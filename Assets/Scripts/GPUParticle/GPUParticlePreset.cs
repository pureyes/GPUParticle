using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "GPUParticlePreset", menuName = "GPU Particle/Preset")]
public class GPUParticlePreset : ScriptableObject
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

    public void ApplyTo(GPUParticleSystem system)
    {
        if (system == null) return;
        system.particleCount = particleCount;
        system.particleLife = particleLife;
        system.emitRadius = emitRadius;
        system.emitSpeed = emitSpeed;
        system.gravity = gravity;
        system.damping = damping;
        system.windForce = windForce;
        system.enableMouseInteraction = enableMouseInteraction;
        system.interactionRadius = interactionRadius;
        system.interactionStrength = interactionStrength;
        system.renderPassEvent = renderPassEvent;
    }

    public void CaptureFrom(GPUParticleSystem system)
    {
        if (system == null) return;
        particleCount = system.particleCount;
        particleLife = system.particleLife;
        emitRadius = system.emitRadius;
        emitSpeed = system.emitSpeed;
        gravity = system.gravity;
        damping = system.damping;
        windForce = system.windForce;
        enableMouseInteraction = system.enableMouseInteraction;
        interactionRadius = system.interactionRadius;
        interactionStrength = system.interactionStrength;
        renderPassEvent = system.renderPassEvent;
    }
}
