using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// GPU粒子系统的渲染Pass
/// 负责在URP管线的指定阶段渲染粒子
/// </summary>
public class GPUParticleRenderPass : ScriptableRenderPass
{
    private GPUParticleRendererFeature.Settings _settings;
    private ComputeBuffer _particleBuffer;
    private ComputeBuffer _indirectArgsBuffer;
    private int _threadGroups;
    private bool _initialized;
    
    // Compute Shader相关
    private int _updateKernel;
    private ComputeShader _particleCompute;
    
    // 属性ID缓存
    private static readonly int ParticleBufferID = Shader.PropertyToID("_ParticleBuffer");
    private static readonly int DeltaTimeID = Shader.PropertyToID("_DeltaTime");
    private static readonly int TimeID = Shader.PropertyToID("_Time");
    private static readonly int ParticleCountID = Shader.PropertyToID("_ParticleCount");
    private static readonly int EmitRadiusID = Shader.PropertyToID("_EmitRadius");
    private static readonly int EmitSpeedID = Shader.PropertyToID("_EmitSpeed");
    private static readonly int ParticleLifeID = Shader.PropertyToID("_ParticleLife");
    private static readonly int MousePositionID = Shader.PropertyToID("_MousePosition");
    private static readonly int MouseRadiusID = Shader.PropertyToID("_MouseRadius");
    private static readonly int MouseStrengthID = Shader.PropertyToID("_MouseStrength");
    private static readonly int MouseActiveID = Shader.PropertyToID("_MouseActive");
    
    public GPUParticleRenderPass(GPUParticleRendererFeature.Settings settings)
    {
        _settings = settings;
        
        // 设置渲染时机（从配置中读取）
        renderPassEvent = _settings.renderPassEvent;
        
        // 初始化粒子系统
        InitializeParticles();
    }
    
    private void InitializeParticles()
    {
        if (_settings.particleCompute == null || _settings.particleMaterial == null)
        {
            Debug.LogWarning("[GPUParticleRenderPass] ComputeShader或Material未设置");
            return;
        }
        
        _particleCompute = _settings.particleCompute;
        _updateKernel = _particleCompute.FindKernel("UpdateParticles");
        
        if (_updateKernel < 0)
        {
            Debug.LogError("[GPUParticleRenderPass] 找不到Kernel 'UpdateParticles'");
            return;
        }
        
        // 创建粒子缓冲区
        int particleStride = System.Runtime.InteropServices.Marshal.SizeOf<ParticleData>();
        _particleBuffer = new ComputeBuffer(_settings.particleCount, particleStride, ComputeBufferType.Default);
        
        // 初始化粒子数据
        ParticleData[] particles = new ParticleData[_settings.particleCount];
        for (int i = 0; i < _settings.particleCount; i++)
        {
            particles[i] = new ParticleData
            {
                position = Random.insideUnitSphere * 10f,
                life = Random.Range(0f, 5f),
                velocity = Random.insideUnitSphere * 5f,
                maxLife = 5f
            };
        }
        _particleBuffer.SetData(particles);
        
        // 创建间接绘制参数缓冲区
        uint[] args = new uint[5];
        args[0] = 6; // 每个实例的顶点数（Quad = 6个顶点）
        args[1] = (uint)_settings.particleCount; // 实例数
        args[2] = 0; // 起始顶点
        args[3] = 0; // 起始实例
        args[4] = 0; // 保留
        
        _indirectArgsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        _indirectArgsBuffer.SetData(args);
        
        _threadGroups = Mathf.CeilToInt(_settings.particleCount / 256f);
        _initialized = true;
        
        if (_settings.showDebugInfo)
        {
            Debug.Log($"[GPUParticleRenderPass] 初始化完成: {_settings.particleCount} 粒子, 渲染时机: {_settings.renderPassEvent}");
        }
    }
#if UNITY_EDITOR
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        renderPassEvent = _settings.renderPassEvent;
        base.OnCameraSetup(cmd, ref renderingData);
    }
#endif

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (!_initialized || _particleBuffer == null)
            return;
        
        // 1. 更新粒子（Compute Shader）
        UpdateParticles();
        
        // 2. 渲染粒子
        RenderParticles(context, renderingData);
    }
    
    private void UpdateParticles()
    {
        _particleCompute.SetFloat(DeltaTimeID, Time.deltaTime);
        _particleCompute.SetFloat(TimeID, Time.time);
        _particleCompute.SetInt(ParticleCountID, _settings.particleCount);
        
        // 发射参数
        _particleCompute.SetFloat(EmitRadiusID, _settings.emitRadius);
        _particleCompute.SetFloat(EmitSpeedID, _settings.emitSpeed);
        _particleCompute.SetFloat(ParticleLifeID, _settings.particleLife);
        
        // 鼠标交互
        if (_settings.enableMouseInteraction && Camera.main != null)
        {
            HandleMouseInteraction();
        }
        else
        {
            _particleCompute.SetInt(MouseActiveID, 0);
        }
        
        _particleCompute.SetBuffer(_updateKernel, ParticleBufferID, _particleBuffer);
        
        _particleCompute.Dispatch(_updateKernel, _threadGroups, 1, 1);
    }
    
    private void HandleMouseInteraction()
    {
        Vector3 mouseWorldPos = Vector3.zero;
        int mouseActive = 0;
        
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            
            if (groundPlane.Raycast(ray, out float enter))
            {
                mouseWorldPos = ray.GetPoint(enter);
                mouseActive = 1;
            }
        }
        
        _particleCompute.SetVector(MousePositionID, mouseWorldPos);
        _particleCompute.SetFloat(MouseRadiusID, _settings.interactionRadius);
        _particleCompute.SetFloat(MouseStrengthID, _settings.interactionStrength);
        _particleCompute.SetInt(MouseActiveID, mouseActive);
    }
    
    private void RenderParticles(ScriptableRenderContext context, RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get("GPUParticleRender");
        
        // 设置材质参数
        _settings.particleMaterial.SetBuffer(ParticleBufferID, _particleBuffer);
        
        // 绘制粒子
        cmd.DrawProceduralIndirect(
            Matrix4x4.identity, 
            _settings.particleMaterial, 
            0, 
            MeshTopology.Triangles, 
            _indirectArgsBuffer
        );
        
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
    
    public void Dispose()
    {
        _particleBuffer?.Release();
        _particleBuffer = null;
        
        _indirectArgsBuffer?.Release();
        _indirectArgsBuffer = null;
        
        _initialized = false;
    }
    
    // 粒子数据结构（必须与Compute Shader一致）
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    private struct ParticleData
    {
        public Vector3 position;
        public float life;
        public Vector3 velocity;
        public float maxLife;
    }
}
