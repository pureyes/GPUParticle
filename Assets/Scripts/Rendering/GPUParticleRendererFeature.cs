using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// GPU粒子系统的URP Renderer Feature
/// 将粒子渲染集成到URP管线中，支持可配置的渲染时机
/// </summary>
public class GPUParticleRendererFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        [Header("粒子系统配置")]
        public ComputeShader particleCompute;
        public Material particleMaterial;
        public int particleCount = 100000;
        
        [Header("发射参数")]
        public float emitRadius = 10f;
        public float emitSpeed = 5f;
        public float particleLife = 3f;
        
        [Header("物理参数")]
        public float gravity = 9.8f;
        public float damping = 0.1f;
        public Vector3 windForce = Vector3.zero;
        
        [Header("鼠标交互")]
        public bool enableMouseInteraction = true;
        public float interactionRadius = 3f;
        public float interactionStrength = 50f;
        
        [Header("渲染时机")]
        [Tooltip("选择粒子在URP管线的哪个阶段渲染")]
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        
        [Header("Scene View")]
        public bool enableSceneViewPreview = true;
        
        [Header("调试")]
        public bool showDebugInfo = false;
    }
    
    public Settings settings = new Settings();
    private GPUParticleRenderPass _particlePass;
    
    public override void Create()
    {
        _particlePass = new GPUParticleRenderPass(settings);
    }
    
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var cameraType = renderingData.cameraData.cameraType; 
                // 加这行诊断
        Debug.Log($"[{GetType().Name}] CameraType={cameraType}, enableSceneView={settings.enableSceneViewPreview}");
    
        bool shouldRender = cameraType == CameraType.Game || 
                            (cameraType == CameraType.SceneView && settings.enableSceneViewPreview);
        if (shouldRender)
        {
            renderer.EnqueuePass(_particlePass);
        }
    }
    
    protected override void Dispose(bool disposing)
    {
        _particlePass?.Dispose();
        base.Dispose(disposing);
    }
}
