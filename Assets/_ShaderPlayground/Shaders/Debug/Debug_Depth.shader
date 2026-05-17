Shader "Debug/DepthVisualization"
{
    Properties
    {
        // 无属性，纯 Debug
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
            };

            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
                float4 positionNDC  : TEXCOORD0;    // 用于计算屏幕UV
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                VertexPositionInputs posInput = GetVertexPositionInputs(input.positionOS.xyz);
                output.positionCS = posInput.positionCS;
                output.positionNDC = posInput.positionNDC;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // 1. 获取屏幕空间 UV
                // positionNDC 是齐次坐标下的 [0, w] 范围，除以 w 得到 [0, 1]
                float2 screenUV = input.positionNDC.xy / input.positionNDC.w;

                // 2. 采样场景深度（非线性，0~1，近处精度高）
                float rawDepth = SampleSceneDepth(screenUV);

                // 3. 线性化深度（转为实际世界空间距离）
                // LinearEyeDepth: 返回的是相机空间下的 Z 值（距离相机的实际距离）
                // Linear01Depth: 返回的是归一化到 [0,1] 范围的线性深度
                float linearEyeDepth = LinearEyeDepth(rawDepth, _ZBufferParams);
                float linear01Depth = Linear01Depth(rawDepth, _ZBufferParams);

                // ========== 可视化方式 ==========

                // A. 直接看原始深度（非线性，几乎全是白色，因为近处精度被压缩了）
                // return half4(rawDepth.xxx, 1);

                // B. 看 Linear01Depth（线性归一化，远处白、近处黑）
                // return half4(linear01Depth.xxx, 1);

                // C. 看 LinearEyeDepth（实际距离，需要除以一个参考距离来可视化）
                // 这里除以 50 作为参考远裁剪面距离，你可以改成你场景的实际范围
                float visualDepth = saturate(linearEyeDepth / 50.0);
                return half4(visualDepth.xxx, 1);

                // D. 伪彩色（近红远蓝，更有辨识度）
                // float t = saturate(linearEyeDepth / 50.0);
                // half3 color = lerp(half3(1, 0, 0), half3(0, 0, 1), t);
                // return half4(color, 1);
            }
            ENDHLSL
        }
    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}
