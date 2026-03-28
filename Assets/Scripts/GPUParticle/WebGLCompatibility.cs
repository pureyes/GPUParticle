using UnityEngine;

/// <summary>
/// WebGL 兼容性检查 - 检查浏览器是否支持 WebGL 2.0 和 Compute Shader
/// </summary>
public class WebGLCompatibility : MonoBehaviour
{
    [SerializeField] private bool showDebugInfo = true;
    
    private void Start()
    {
        CheckCompatibility();
    }

    private void CheckCompatibility()
    {
        if (showDebugInfo)
        {
            Debug.Log($"Platform: {Application.platform}");
            Debug.Log($"Unity Version: {Application.unityVersion}");
            Debug.Log($"Graphics Device: {SystemInfo.graphicsDeviceName}");
            Debug.Log($"Graphics API: {SystemInfo.graphicsDeviceType}");
            Debug.Log($"Compute Shader Support: {SystemInfo.supportsComputeShaders}");
            Debug.Log($"Max Compute Buffer Size: {SystemInfo.maxComputeBufferInputsVertex}");
        }

        // 检查 Compute Shader 支持
        if (!SystemInfo.supportsComputeShaders)
        {
            Debug.LogError("Compute Shaders are not supported on this platform!");
            
            #if UNITY_WEBGL && !UNITY_EDITOR
            // 在 WebGL 构建中显示警告
            Debug.LogError("WebGL 2.0 with Compute Shader support is required. Please use a modern browser.");
            #endif
        }
        else
        {
            Debug.Log("Compute Shaders are supported!");
        }
    }

    private void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // 简单的调试信息显示
        GUILayout.BeginArea(new Rect(10, 10, 300, 100));
        GUILayout.BeginVertical("box");
        
        GUILayout.Label($"FPS: {1f / Time.deltaTime:F1}");
        GUILayout.Label($"Particles: {FindObjectOfType<GPUParticleSystem>()?.GetParticleCount() ?? 0}");
        GUILayout.Label($"GPU: {SystemInfo.graphicsDeviceName}");
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
