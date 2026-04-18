// using UnityEngine;

// /// <summary>
// /// GPU粒子系统 - CommandBuffer 版本（内置管线）
// /// 可以精确控制渲染时机
// /// </summary>
// public class GPUParticleSystem_CB : MonoBehaviour
// {
//     [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
//     public struct Particle
//     {
//         public Vector3 position;
//         public float life;
//         public Vector3 velocity;
//         public float maxLife;
//     }

//     [Header("Particle Settings")]
//     [SerializeField] private int particleCount = 100000;
//     [SerializeField] private float emitRadius = 10f;
//     [SerializeField] private float emitSpeed = 5f;
//     [SerializeField] private float particleLife = 3f;
//     [SerializeField] private float particleSize = 0.1f;

//     [Header("References")]
//     [SerializeField] private ComputeShader particleCompute;
//     [SerializeField] private Material particleMaterial;

//     [Header("Rendering")]
//     [Tooltip("渲染时机")]
//     [SerializeField] private CameraEvent renderEvent = CameraEvent.BeforeForwardAlpha;

//     [Header("Mouse Interaction")]
//     [SerializeField] private float interactionRadius = 3f;
//     [SerializeField] private float interactionStrength = 50f;
//     [SerializeField] private bool enableMouseInteraction = true;

//     private ComputeBuffer particleBuffer;
//     private ComputeBuffer indirectArgsBuffer;
//     private CommandBuffer commandBuffer;
//     private int threadGroups;
//     private int updateKernel;
//     private Camera cachedCamera;

//     // Property IDs
//     private static readonly int ParticleBufferID = Shader.PropertyToID("_ParticleBuffer");
//     private static readonly int DeltaTimeID = Shader.PropertyToID("_DeltaTime");
//     private static readonly int TimeID = Shader.PropertyToID("_Time");
//     private static readonly int EmitRadiusID = Shader.PropertyToID("_EmitRadius");
//     private static readonly int EmitSpeedID = Shader.PropertyToID("_EmitSpeed");
//     private static readonly int ParticleLifeID = Shader.PropertyToID("_ParticleLife");
//     private static readonly int MousePositionID = Shader.PropertyToID("_MousePosition");
//     private static readonly int MouseRadiusID = Shader.PropertyToID("_MouseRadius");
//     private static readonly int MouseStrengthID = Shader.PropertyToID("_MouseStrength");
//     private static readonly int MouseActiveID = Shader.PropertyToID("_MouseActive");
//     private static readonly int ParticleCountID = Shader.PropertyToID("_ParticleCount");
//     private static readonly int ParticleSizeID = Shader.PropertyToID("_ParticleSize");

//     private void OnEnable()
//     {
//         cachedCamera = Camera.main;
//         if (cachedCamera == null)
//         {
//             Debug.LogError("No main camera found!");
//             enabled = false;
//             return;
//         }

//         InitializeParticles();
//         CreateCommandBuffer();
        
//         // 关键：将 CommandBuffer 添加到相机的指定渲染阶段
//         cachedCamera.AddCommandBuffer(renderEvent, commandBuffer);
//     }

//     private void OnDisable()
//     {
//         if (cachedCamera != null && commandBuffer != null)
//         {
//             cachedCamera.RemoveCommandBuffer(renderEvent, commandBuffer);
//         }
        
//         ReleaseBuffers();
        
//         if (commandBuffer != null)
//         {
//             commandBuffer.Release();
//             commandBuffer = null;
//         }
//     }

//     private void InitializeParticles()
//     {
//         if (particleCompute == null)
//         {
//             Debug.LogError("Compute Shader is not assigned!");
//             return;
//         }

//         updateKernel = particleCompute.FindKernel("UpdateParticles");
//         if (updateKernel < 0)
//         {
//             Debug.LogError("Failed to find kernel 'UpdateParticles'");
//             return;
//         }

//         // Create particle buffer
//         Particle[] particles = new Particle[particleCount];
//         for (int i = 0; i < particleCount; i++)
//         {
//             particles[i] = new Particle
//             {
//                 position = Random.insideUnitSphere * emitRadius,
//                 velocity = Random.insideUnitSphere * emitSpeed,
//                 life = Random.Range(0f, particleLife),
//                 maxLife = particleLife
//             };
//         }

//         int stride = System.Runtime.InteropServices.Marshal.SizeOf<Particle>();
//         particleBuffer = new ComputeBuffer(particleCount, stride);
//         particleBuffer.SetData(particles);

//         // Indirect args buffer
//         uint[] args = new uint[] { 6, (uint)particleCount, 0, 0, 0 };
//         indirectArgsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
//         indirectArgsBuffer.SetData(args);

//         threadGroups = Mathf.CeilToInt(particleCount / 256f);
//     }

//     private void CreateCommandBuffer()
//     {
//         commandBuffer = new CommandBuffer();
//         commandBuffer.name = "GPUParticleSystem_CB";
//     }

//     private void Update()
//     {
//         if (particleCompute == null || particleBuffer == null)
//             return;

//         // 1. 更新 Compute Shader 参数
//         particleCompute.SetFloat(DeltaTimeID, Time.deltaTime);
//         particleCompute.SetFloat(TimeID, Time.time);
//         particleCompute.SetFloat(EmitRadiusID, emitRadius);
//         particleCompute.SetFloat(EmitSpeedID, emitSpeed);
//         particleCompute.SetFloat(ParticleLifeID, particleLife);
//         particleCompute.SetInt(ParticleCountID, particleCount);

//         // 鼠标交互
//         if (enableMouseInteraction)
//         {
//             HandleMouseInteraction();
//         }
//         else
//         {
//             particleCompute.SetInt(MouseActiveID, 0);
//         }

//         particleCompute.SetBuffer(updateKernel, ParticleBufferID, particleBuffer);

//         // 2. Dispatch Compute Shader（更新粒子位置）
//         // 注意：这里只是设置参数，真正的 Dispatch 要在 CommandBuffer 里做
//         // 或者像下面这样直接 Dispatch（计算在 GPU，不依赖渲染时机）
//         particleCompute.Dispatch(updateKernel, threadGroups, 1, 1);

//         // 3. 构建 CommandBuffer
//         BuildCommandBuffer();
//     }

//     private void HandleMouseInteraction()
//     {
//         Vector3 mouseWorldPos = Vector3.zero;
//         int mouseActive = 0;

//         if (Input.GetMouseButton(0) && cachedCamera != null)
//         {
//             Ray ray = cachedCamera.ScreenPointToRay(Input.mousePosition);
//             Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            
//             if (groundPlane.Raycast(ray, out float enter))
//             {
//                 mouseWorldPos = ray.GetPoint(enter);
//                 mouseActive = 1;
//             }
//         }

//         particleCompute.SetVector(MousePositionID, mouseWorldPos);
//         particleCompute.SetFloat(MouseRadiusID, interactionRadius);
//         particleCompute.SetFloat(MouseStrengthID, interactionStrength);
//         particleCompute.SetInt(MouseActiveID, mouseActive);
//     }

//     private void BuildCommandBuffer()
//     {
//         // 清除上一次的命令
//         commandBuffer.Clear();

//         // 设置材质参数
//         commandBuffer.SetGlobalFloat(ParticleSizeID, particleSize);
//         commandBuffer.SetGlobalBuffer(ParticleBufferID, particleBuffer);

//         // 绘制命令（会被记录到 CommandBuffer，在 renderEvent 时机执行）
//         commandBuffer.DrawProceduralIndirect(
//             Matrix4x4.identity,
//             particleMaterial,
//             0,
//             MeshTopology.Triangles,
//             indirectArgsBuffer
//         );
//     }

//     private void ReleaseBuffers()
//     {
//         if (particleBuffer != null)
//         {
//             particleBuffer.Release();
//             particleBuffer = null;
//         }

//         if (indirectArgsBuffer != null)
//         {
//             indirectArgsBuffer.Release();
//             indirectArgsBuffer = null;
//         }
//     }
// }
