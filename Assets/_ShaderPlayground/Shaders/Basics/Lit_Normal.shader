// ==================== 切线空间版本（已注释，供对比）====================
// Shader "lit/Basic/lit_Normal"
// {
//     Properties
//     {
//         _MainTex ("Texture", 2D) = "white" { }
//         _BumpTex ("Normal Map", 2D) = "bump" { }
//         _BumpScale ("Bump Scale", Float) = 1.0
//         _Specular ("Specular", Color) = (1,1,1,1)
//         _Gloss ("Gloss", Range(8.0, 256)) = 20
//     }
//     SubShader
//     {
//         Tags
//         {
//             "RenderType" = "Opaque"
//             "RenderPipeline" = "UniversalPipeline"
//         }
//         LOD 100
// 
//         Pass
//         {
//             Name "ForwardLit"
//             Tags { "LightMode" = "UniversalForward" }
// 
//             HLSLPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
// 
//             #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
//             #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
// 
//             struct Attributes
//             {
//                 float4 positionOS : POSITION;
//                 float3 normalOS   : NORMAL;
//                 float4 tangentOS  : TANGENT;
//                 float2 uv         : TEXCOORD0;
//             };
// 
//             struct Varyings
//             {
//                 float4 uv         : TEXCOORD0;       // xy: MainTex, zw: BumpTex
//                 float4 positionCS : SV_POSITION;
//                 float3 lightDirTS : TEXCOORD1;       // 切线空间光照方向
//                 float3 viewDirTS  : TEXCOORD2;       // 切线空间视线方向
//             };
// 
//             TEXTURE2D(_MainTex);
//             TEXTURE2D(_BumpTex);
//             SAMPLER(sampler_MainTex);
//             SAMPLER(sampler_BumpTex);
// 
//             CBUFFER_START(UnityPerMaterial)
//                 float4 _MainTex_ST;
//                 float4 _BumpTex_ST;
//                 float  _BumpScale;
//                 float4 _Specular;
//                 float  _Gloss;
//             CBUFFER_END
// 
//             Varyings vert(Attributes v)
//             {
//                 Varyings o;
//                 o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
// 
//                 // 1. 构建世界空间下的 T、B、N
//                 float3 normalWS    = TransformObjectToWorldNormal(v.normalOS);
//                 float3 tangentWS   = TransformObjectToWorldDir(v.tangentOS.xyz);
//                 float3 bitangentWS = cross(normalWS, tangentWS) * v.tangentOS.w;
// 
//                 // 2. 构建 世界空间 -> 切线空间 的变换矩阵
//                 //    TBN 是正交矩阵，逆矩阵 = 转置矩阵
//                 //    这里把 tangentWS / bitangentWS / normalWS 作为矩阵的 3 个行向量
//                 float3x3 worldToTangent = float3x3(tangentWS, bitangentWS, normalWS);
// 
//                 // 3. 获取世界空间的光照方向和视线方向（先不normalize，放到片元再做）
//                 Light mainLight = GetMainLight();
//                 float3 lightDirWS = mainLight.direction;
// 
//                 float3 positionWS = TransformObjectToWorld(v.positionOS.xyz);
//                 float3 viewDirWS  = GetWorldSpaceNormalizeViewDir(positionWS);
// 
//                 // 4. 将光照方向和视线方向转换到切线空间
//                 o.lightDirTS = mul(worldToTangent, lightDirWS);
//                 o.viewDirTS  = mul(worldToTangent, viewDirWS);
// 
//                 o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
//                 o.uv.zw = TRANSFORM_TEX(v.uv, _BumpTex);
//                 return o;
//             }
// 
//             half4 frag(Varyings i) : SV_Target
//             {
//                 // 1. 收到插值后的方向，重新normalize（插值会破坏单位长度）
//                 float3 lightDirTS = normalize(i.lightDirTS);
//                 float3 viewDirTS  = normalize(i.viewDirTS);
// 
//                 // 2. 采样并解码法线贴图（得到切线空间法线，范围 [-1, 1]）
//                 float4 bumpTex = SAMPLE_TEXTURE2D(_BumpTex, sampler_BumpTex, i.uv.zw);
//                 float3 tangentNormal = UnpackNormalScale(bumpTex, _BumpScale);
//                 tangentNormal = normalize(tangentNormal); // 确保单位向量
// 
//                 // 3. 采样主纹理
//                 half4 albedo = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy);
// 
//                 // 4. 获取主光源
//                 Light mainLight = GetMainLight();
// 
//                 // 5. Lambert 漫反射
//                 float NdotL = saturate(dot(tangentNormal, lightDirTS));
//                 float3 diffuse = albedo.rgb * mainLight.color * NdotL;
// 
//                 // 6. BlinnPhong 高光
//                 float3 halfDir = normalize(lightDirTS + viewDirTS);
//                 float NdotH = saturate(dot(tangentNormal, halfDir));
//                 float3 specular = _Specular.rgb * mainLight.color * pow(NdotH, _Gloss);
// 
//                 // 7. 简单环境光（避免背光面死黑）
//                 float3 ambient = 0.05 * albedo.rgb;
// 
//                 // 8. 合并
//                 float3 finalColor = diffuse + specular + ambient;
//                 return half4(finalColor, albedo.a);
//             }
// 
//             ENDHLSL
//         }
//     }
// }


// ==================== 世界空间版本 ====================
Shader "lit/Basic/lit_Normal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }
        _BumpTex ("Normal Map", 2D) = "bump" { }
        _BumpScale ("Bump Scale", Float) = 1.0
        _Specular ("Specular", Color) = (1,1,1,1)
        _Gloss ("Gloss", Range(8.0, 256)) = 20
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
        }
        LOD 100

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float4 tangentOS  : TANGENT;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 uv          : TEXCOORD0;       // xy: MainTex, zw: BumpTex
                float4 positionCS  : SV_POSITION;
                float3 normalWS    : TEXCOORD1;       // 世界空间法线
                float4 tangentWS   : TEXCOORD2;       // xyz: 世界空间切线, w: handness符号
                float3 positionWS  : TEXCOORD3;       // 世界空间位置（用于算视线方向）
            };

            TEXTURE2D(_MainTex);
            TEXTURE2D(_BumpTex);
            SAMPLER(sampler_MainTex);
            SAMPLER(sampler_BumpTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _BumpTex_ST;
                float  _BumpScale;
                float4 _Specular;
                float  _Gloss;
            CBUFFER_END

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);

                // 1. 把法线和切线转到世界空间，同时保留 handness 符号 (tangentOS.w)
                o.normalWS   = TransformObjectToWorldNormal(v.normalOS);
                o.tangentWS.xyz = TransformObjectToWorldDir(v.tangentOS.xyz);
                o.tangentWS.w   = v.tangentOS.w;

                o.positionWS = TransformObjectToWorld(v.positionOS.xyz);

                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.zw = TRANSFORM_TEX(v.uv, _BumpTex);
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                // 1. 采样并解码法线贴图（得到切线空间法线）
                float4 bumpTex = SAMPLE_TEXTURE2D(_BumpTex, sampler_BumpTex, i.uv.zw);
                float3 tangentNormal = UnpackNormalScale(bumpTex, _BumpScale);

                // 2. 重建世界空间 TBN 矩阵
                //    注意：插值后的 normalWS 和 tangentWS 长度和夹角都会变，
                //    必须重新 normalize，并用 cross 重建正交的 bitangent
                float3 normalWS    = normalize(i.normalWS);
                float3 tangentWS   = normalize(i.tangentWS.xyz);
                float3 bitangentWS = cross(normalWS, tangentWS) * i.tangentWS.w;

                // 构建 切线空间 -> 世界空间 的变换矩阵
                float3x3 tangentToWorld = float3x3(tangentWS, bitangentWS, normalWS);

                // 3. 把法线从切线空间转到世界空间
                float3 normalWS_FromBump = mul(tangentToWorld, tangentNormal);
                normalWS_FromBump = normalize(normalWS_FromBump);

                // 4. 获取世界空间的光照方向和视线方向
                Light mainLight = GetMainLight();
                float3 lightDirWS = normalize(mainLight.direction);

                float3 viewDirWS = normalize(GetWorldSpaceNormalizeViewDir(i.positionWS));

                // 5. Lambert 漫反射（在世界空间计算）
                float NdotL = saturate(dot(normalWS_FromBump, lightDirWS));
                half4 albedo = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy);
                float3 diffuse = albedo.rgb * mainLight.color * NdotL;

                // 6. BlinnPhong 高光（在世界空间计算）
                float3 halfDir = normalize(lightDirWS + viewDirWS);
                float NdotH = saturate(dot(normalWS_FromBump, halfDir));
                float3 specular = _Specular.rgb * mainLight.color * pow(NdotH, _Gloss);

                // 7. 简单环境光
                float3 ambient = 0.05 * albedo.rgb;

                // 8. 合并
                float3 finalColor = diffuse + specular + ambient;
                return half4(finalColor, albedo.a);
            }

            ENDHLSL
        }
    }
}
