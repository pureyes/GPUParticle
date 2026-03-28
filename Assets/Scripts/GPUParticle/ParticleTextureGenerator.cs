using UnityEngine;

/// <summary>
/// 运行时粒子纹理生成器 - 如果inspector中没有分配纹理，则创建默认的软圆形粒子纹理
/// </summary>
public static class ParticleTextureGenerator
{
    /// <summary>
    /// 创建一个软圆形的粒子纹理
    /// </summary>
    public static Texture2D CreateDefaultParticleTexture(int size = 128)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Bilinear;
        
        Color[] pixels = new Color[size * size];
        Vector2 center = new Vector2(size / 2f, size / 2f);
        float maxDist = size / 2f;
        
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector2 pos = new Vector2(x, y);
                float dist = Vector2.Distance(pos, center);
                
                // 软圆形渐变
                float alpha = 1f - Mathf.SmoothStep(0f, maxDist, dist);
                
                // 添加一些辉光效果
                float glow = Mathf.Exp(-dist * dist / (maxDist * maxDist * 0.5f));
                
                int index = y * size + x;
                pixels[index] = new Color(1f, 1f, 1f, alpha * glow);
            }
        }
        
        texture.SetPixels(pixels);
        texture.Apply();
        
        return texture;
    }
    
    /// <summary>
    /// 创建带有星形光晕的粒子纹理
    /// </summary>
    public static Texture2D CreateStarParticleTexture(int size = 128)
    {
        Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Bilinear;
        
        Color[] pixels = new Color[size * size];
        Vector2 center = new Vector2(size / 2f, size / 2f);
        float maxDist = size / 2f;
        
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector2 pos = new Vector2(x, y);
                Vector2 dir = pos - center;
                float dist = dir.magnitude;
                float angle = Mathf.Atan2(dir.y, dir.x);
                
                // 基础圆形
                float circleAlpha = 1f - Mathf.SmoothStep(0f, maxDist * 0.3f, dist);
                
                // 添加星形光晕
                float starGlow = Mathf.Pow(Mathf.Abs(Mathf.Cos(angle * 4f)), 2f);
                starGlow *= Mathf.Exp(-dist / (maxDist * 0.5f));
                
                float alpha = Mathf.Max(circleAlpha, starGlow * 0.3f);
                alpha *= 1f - Mathf.SmoothStep(0f, maxDist, dist);
                
                int index = y * size + x;
                pixels[index] = new Color(1f, 1f, 1f, Mathf.Clamp01(alpha));
            }
        }
        
        texture.SetPixels(pixels);
        texture.Apply();
        
        return texture;
    }
}
