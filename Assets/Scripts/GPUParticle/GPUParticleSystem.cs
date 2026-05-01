using UnityEngine;
using UnityEngine.Rendering.Universal;


/// <summary>
/// GPU粒子系统场景组件
/// 作为场景中粒子系统的配置中心，支持Inspector可视化编辑、Gizmos预览和Preset系统
/// </summary>
public class GPUParticleSystem : MonoBehaviour
{
    [Header("基础参数")]
    public int particleCount = 100000;
    public float particleLife = 3f;
    public float emitRadius = 10f;
    public float emitSpeed = 5f;

    [Header("物理参数")]
    public float gravity = 9.8f;
    public float damping = 0.1f;
    public Vector3 windForce = Vector3.zero;

    [Header("交互参数")]
    public bool enableMouseInteraction = true;
    public float interactionRadius = 3f;
    public float interactionStrength = 50f;

    [Header("渲染")]
    public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    public bool enableSceneViewPreview = true;
    public bool showDebugInfo = false;

    [Header("Gizmos")]
    public bool showEmitRadiusGizmo = true;
    public bool showInteractionGizmo = true;
    public bool showParticleDistributionPreview = true;
    public int distributionPreviewCount = 200;
    public Color gizmoColor = new Color(0f, 1f, 1f, 0.5f);
    public Color interactionGizmoColor = new Color(1f, 0.5f, 0f, 0.5f);

    [Header("运行时引用")]
    public GPUParticleRendererFeature targetRendererFeature;

    [System.NonSerialized] public bool isPaused;

    private void OnValidate()
    {
        if (targetRendererFeature == null)
        {
            targetRendererFeature = FindRendererFeature();
        }
        SyncToRendererFeature();
    }

    private void Update()
    {
        SyncToRendererFeature();
    }

    public void SyncToRendererFeature()
    {
        if (targetRendererFeature == null) return;

        var s = targetRendererFeature.settings;
        s.particleCount = particleCount;
        s.particleLife = particleLife;
        s.emitRadius = emitRadius;
        s.emitSpeed = emitSpeed;
        s.gravity = gravity;
        s.damping = damping;
        s.windForce = windForce;
        s.enableMouseInteraction = enableMouseInteraction;
        s.interactionRadius = interactionRadius;
        s.interactionStrength = interactionStrength;
        s.renderPassEvent = renderPassEvent;
        s.enableSceneViewPreview = enableSceneViewPreview;
        s.showDebugInfo = showDebugInfo;
    }

    private GPUParticleRendererFeature FindRendererFeature()
    {
#if UNITY_EDITOR
        string[] guids = UnityEditor.AssetDatabase.FindAssets("t:GPUParticleRendererFeature");
        if (guids.Length > 0)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
            var feature = UnityEditor.AssetDatabase.LoadAssetAtPath<GPUParticleRendererFeature>(path);
            if (feature != null) return feature;
        }
#endif
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        if (showEmitRadiusGizmo)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, emitRadius);
        }

        if (showParticleDistributionPreview && distributionPreviewCount > 0)
        {
            DrawParticleDistributionPreview();
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (showInteractionGizmo && enableMouseInteraction && Event.current != null)
        {
            bool mouseDown = Event.current.type == EventType.MouseDrag ||
                             Event.current.type == EventType.MouseDown;
            if (mouseDown)
            {
                Ray ray = UnityEditor.HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, transform.position);
                if (groundPlane.Raycast(ray, out float enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);
                    Gizmos.color = interactionGizmoColor;
                    Gizmos.DrawWireSphere(hitPoint, interactionRadius);
                }
            }
        }
#endif
    }

    private void DrawParticleDistributionPreview()
    {
        Random.State oldState = Random.state;
        Random.InitState(42);

        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 0.3f);
        int count = Mathf.Min(distributionPreviewCount, particleCount);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * emitRadius;
            Gizmos.DrawSphere(pos, 0.05f);
        }

        Random.state = oldState;
    }

    public void ResetParticles()
    {
        if (targetRendererFeature == null) return;

        var passField = typeof(GPUParticleRendererFeature).GetField("_particlePass",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (passField != null)
        {
            var pass = passField.GetValue(targetRendererFeature) as GPUParticleRenderPass;
            pass?.ResetParticles();
        }
    }

    public void TogglePause()
    {
        if (targetRendererFeature == null) return;

        var passField = typeof(GPUParticleRendererFeature).GetField("_particlePass",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (passField != null)
        {
            var pass = passField.GetValue(targetRendererFeature) as GPUParticleRenderPass;
            if (pass != null)
            {
                isPaused = !isPaused;
                pass.IsPaused = isPaused;
            }
        }
    }
}
