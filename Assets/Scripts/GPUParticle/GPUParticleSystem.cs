using UnityEngine;

public class GPUParticleSystem : MonoBehaviour
{
    // Particle data structure - 注意内存对齐，确保和 Compute Shader 一致
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Particle
    {
        public Vector3 position;  // 12 bytes
        public float life;        // 4 bytes  - 总计16字节，对齐
        public Vector3 velocity;  // 12 bytes
        public float maxLife;     // 4 bytes  - 总计16字节，对齐
    } // 总计 32 字节

    [Header("Particle Settings")]
    [SerializeField] private int particleCount = 100000;
    [SerializeField] private float emitRadius = 10f;
    [SerializeField] private float emitSpeed = 5f;
    [SerializeField] private float particleLife = 3f;
    [SerializeField] private float particleSize = 0.1f;

    [Header("References")]
    [SerializeField] private ComputeShader particleCompute;
    [SerializeField] private Material particleMaterial;
    [SerializeField] private Texture2D particleTexture;

    private ComputeBuffer particleBuffer;
    private ComputeBuffer indirectArgsBuffer;
    private int threadGroups;
    private Texture2D defaultParticleTexture;
    private Camera cachedMainCamera;

    // Mouse interaction settings
    [Header("Mouse Interaction")]
    [SerializeField] private float interactionRadius = 3f;
    [SerializeField] private float interactionStrength = 50f;
    [SerializeField] private bool enableMouseInteraction = true;

    // Compute Shader kernel IDs
    private int updateKernel;
    
    // Compute Shader property IDs
    private static readonly int ParticleBufferID = Shader.PropertyToID("_ParticleBuffer");
    private static readonly int DeltaTimeID = Shader.PropertyToID("_DeltaTime");
    private static readonly int EmitRadiusID = Shader.PropertyToID("_EmitRadius");
    private static readonly int EmitSpeedID = Shader.PropertyToID("_EmitSpeed");
    private static readonly int ParticleLifeID = Shader.PropertyToID("_ParticleLife");
    private static readonly int ParticleSizeID = Shader.PropertyToID("_ParticleSize");
    private static readonly int MousePositionID = Shader.PropertyToID("_MousePosition");
    private static readonly int MouseRadiusID = Shader.PropertyToID("_MouseRadius");
    private static readonly int MouseStrengthID = Shader.PropertyToID("_MouseStrength");
    private static readonly int MouseActiveID = Shader.PropertyToID("_MouseActive");
    private static readonly int TimeID = Shader.PropertyToID("_Time");
    private static readonly int ParticleCountID = Shader.PropertyToID("_ParticleCount");

    private void Start()
    {
        cachedMainCamera = Camera.main;
        InitializeParticles();
    }

    private void InitializeParticles()
    {
        if (particleCompute == null)
        {
            Debug.LogError("Particle Compute Shader is not assigned!");
            return;
        }

        // Get kernel index
        updateKernel = particleCompute.FindKernel("UpdateParticles");
        Debug.Log($"FindKernel result: {updateKernel}");
        
        if (updateKernel < 0)
        {
            Debug.LogError("Failed to find kernel 'UpdateParticles'. Compute Shader may have compilation errors.");
            return;
        }

        // Create and initialize particle array
        Particle[] particles = new Particle[particleCount];

        for (int i = 0; i < particleCount; i++)
        {
            particles[i] = new Particle
            {
                position = Random.insideUnitSphere * emitRadius,
                velocity = Random.insideUnitSphere * emitSpeed,
                life = Random.Range(0f, particleLife),
                maxLife = particleLife
            };
        }

        // Create ComputeBuffer for particles - 32 bytes per particle
        int particleStride = System.Runtime.InteropServices.Marshal.SizeOf<Particle>();
        particleBuffer = new ComputeBuffer(particleCount, particleStride, ComputeBufferType.Default);
        particleBuffer.SetData(particles);

        // Create indirect args buffer for DrawProceduralIndirect
        // Each quad has 6 vertices (2 triangles)
        uint[] args = new uint[5];
        args[0] = 6; // vertex count per instance
        args[1] = (uint)particleCount; // instance count
        args[2] = 0; // start vertex location
        args[3] = 0; // start instance location
        args[4] = 0; // reserved
        
        indirectArgsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        indirectArgsBuffer.SetData(args);

        // Calculate thread groups (256 threads per group)
        threadGroups = Mathf.CeilToInt(particleCount / 256f);

        // 生成默认粒子纹理（如果没有分配）
        if (particleTexture == null && defaultParticleTexture == null)
        {
            defaultParticleTexture = ParticleTextureGenerator.CreateDefaultParticleTexture(128);
        }

        Debug.Log($"GPUParticleSystem initialized with {particleCount} particles, thread groups: {threadGroups}");
    }

    private void Update()
    {
        if (particleCompute == null || particleBuffer == null)
            return;

        // Handle mouse interaction
        Vector3 mouseWorldPos = Vector3.zero;
        int mouseActive = 0;
        
        if (enableMouseInteraction)
        {
            HandleMouseInteraction(out mouseWorldPos, out mouseActive);
        }

        // Set compute shader parameters
        particleCompute.SetFloat(DeltaTimeID, Time.deltaTime);
        particleCompute.SetFloat(TimeID, Time.time);
        particleCompute.SetFloat(EmitRadiusID, emitRadius);
        particleCompute.SetFloat(EmitSpeedID, emitSpeed);
        particleCompute.SetFloat(ParticleLifeID, particleLife);

        // Set mouse interaction parameters
        particleCompute.SetVector(MousePositionID, mouseWorldPos);
        particleCompute.SetFloat(MouseRadiusID, interactionRadius);
        particleCompute.SetFloat(MouseStrengthID, interactionStrength);
        particleCompute.SetInt(MouseActiveID, mouseActive);

        // Set particle buffer and count
        particleCompute.SetBuffer(updateKernel, ParticleBufferID, particleBuffer);
        particleCompute.SetInt(ParticleCountID, particleCount);

        // Dispatch compute shader
        particleCompute.Dispatch(updateKernel, threadGroups, 1, 1);
    }

    private void HandleMouseInteraction(out Vector3 worldPos, out int isActive)
    {
        worldPos = Vector3.zero;
        isActive = 0;

        if (Input.GetMouseButton(0) && cachedMainCamera != null) // Left mouse button
        {
            Ray ray = cachedMainCamera.ScreenPointToRay(Input.mousePosition);
            
            // Create a plane at y=0 for ray intersection
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float enter = 0f;
            
            if (groundPlane.Raycast(ray, out enter))
            {
                worldPos = ray.GetPoint(enter);
                isActive = 1;
            }
        }
    }

    private void OnRenderObject()
    {
        if (particleMaterial == null || particleBuffer == null || indirectArgsBuffer == null)
            return;

        // Set material parameters
        particleMaterial.SetFloat(ParticleSizeID, particleSize);
        particleMaterial.SetBuffer(ParticleBufferID, particleBuffer);
        
        // 设置粒子纹理 - 如果没有分配则使用缓存的默认纹理
        particleMaterial.SetTexture("_MainTex", particleTexture != null ? particleTexture : defaultParticleTexture);

        // Draw particles using indirect draw
        // 使用 Quad (6 vertices, 2 triangles) 而不是 Points
        particleMaterial.SetPass(0);
        Graphics.DrawProceduralIndirectNow(MeshTopology.Triangles, indirectArgsBuffer, 0);
    }

    private void OnDestroy()
    {
        ReleaseBuffers();
        if (defaultParticleTexture != null)
        {
            Destroy(defaultParticleTexture);
            defaultParticleTexture = null;
        }
    }

    private void OnDisable()
    {
        ReleaseBuffers();
    }

    private void ReleaseBuffers()
    {
        if (particleBuffer != null)
        {
            particleBuffer.Release();
            particleBuffer = null;
        }

        if (indirectArgsBuffer != null)
        {
            indirectArgsBuffer.Release();
            indirectArgsBuffer = null;
        }
    }

    // 公共方法：重置粒子
    public void ResetParticles()
    {
        ReleaseBuffers();
        InitializeParticles();
    }

    // 公共方法：获取粒子数量
    public int GetParticleCount()
    {
        return particleCount;
    }

    // 公共方法：设置粒子数量（需要重置）
    public void SetParticleCount(int count)
    {
        particleCount = Mathf.Clamp(count, 1000, 500000);
        ResetParticles();
    }
}
