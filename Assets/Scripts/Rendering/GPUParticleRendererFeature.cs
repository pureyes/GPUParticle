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
        
        [Header("渲染时机")]
        [Tooltip("选择粒子在URP管线的哪个阶段渲染")]
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        
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
        // 只有Game视图和Scene视图都渲染
        var cameraType = renderingData.cameraData.cameraType;
        if (cameraType == CameraType.Game || cameraType == CameraType.SceneView)
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
