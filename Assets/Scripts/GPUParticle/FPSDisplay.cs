using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// FPS 显示器 - 在屏幕上显示当前帧率和粒子数量
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private Text fpsText;
    
    [Header("Settings")]
    [SerializeField] private float updateInterval = 0.5f;

    private float accumTime = 0f;
    private int frames = 0;
    private float currentFps = 0f;

    private void Update()
    {
        // 累加时间和帧数
        accumTime += Time.deltaTime;
        frames++;

        // 每间隔 updateInterval 秒更新一次显示
        if (accumTime >= updateInterval)
        {
            currentFps = frames / accumTime;
            
            if (fpsText != null)
            {
                fpsText.text = $"FPS: {currentFps:F1}";
                
                // 根据FPS改变颜色
                if (currentFps >= 55f)
                    fpsText.color = Color.green;
                else if (currentFps >= 30f)
                    fpsText.color = Color.yellow;
                else
                    fpsText.color = Color.red;
            }

            // 重置累加器
            accumTime = 0f;
            frames = 0;
        }
    }
}
