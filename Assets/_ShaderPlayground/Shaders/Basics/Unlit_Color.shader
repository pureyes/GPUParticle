// Shader "Unlit/Unlit_Color"
// {
//     Properties
//     {
//         // _MainTex：变量名代码中引用
//         // Texture：Inspector显示的标签名
//         // 2D：属性类型=2D纹理
//         // white：默认值=白色纹理
//         _MainTex ("Texture", 2D) = "white"{ }
//     }
//     SubShader
//     {
//         Tags
//         {
//             "RenderType" = "Opaque"
//         }
//         LOD 100

//         Pass
//         {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #pragma multi_compile_fog //为雾效生成多个着色器变体
//             //multi_compile_fog 会让Unity生成2个变体
//             //一个支持雾效(FOG_LINEAR/FOG_EXP/FOG_EXP2定义时)
//             //一个不支持雾效

//             #include "UnityCG.cginc"

//             //顶点着色器输入（从Mesh读取）
//             struct appdata
//             {
//                 float4 vertex : POSITION; //顶点位置（模型空间）
//                 float2 uv : TEXCOORD0;  //第一套UV坐标
//             };

//             //顶点着色器输出/片元着色器输入（经过光栅化插值）
//             struct v2f
//             {
//                 float2 uv : TEXCOORD2;  //UV传给片元着色器
//                 UNITY_FOG_COORDS(1)   //雾效插值数据，存在TEXCOORD1
//                 float4 vertex : SV_POSITION;    //裁剪空间位置（必须）
//                 //SV_POSITION = System Value Position, GPU光栅化必须得语义
//             };

//             //纹理采样器（对应Properties里面的_Maintex）
//             sampler2D _MainTex;
//             //纹理的Scale(x,y)和Translate(x,y)
//             //由Unity自动设置，当你在Inspector调Tiling/Offset时
//             float4 _MainTex_ST;

//             v2f vert(appdata v)
//             {
//                 v2f o;
//                 //MVP 矩阵变换：模型->世界->观察->裁剪
//                 o.vertex = UnityObjectToClipPos(v.vertex);
//                 //uv * _MainTex_ST.xy + _MainText_ST.zw
//                 o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//                 //计算雾效因子，存入o.fogCoord
//                 UNITY_TRANSFER_FOG(o,o.vertex);
//                 return o;
//             }

//             //fixed4 = 低精度4D向量(RGBA)在移动端比float4快
//             fixed4 frag(v2f i):SV_Target //SV_Target = 输出到帧缓冲的颜色
//             {
//                 //采样纹理颜色
//                 fixed4 col = tex2D(_MainTex, i.uv);
//                 //如果有误，混合雾颜色
//                 UNITY_APPLY_FOG(i.fogCoord,col);
//                 return col;
//             }

//             ENDCG
//         }
//     }
// }

//虽然Built-in的Shader在URP里通常也能编译(Unity会做兼容)但建议写URP格式更规范
Shader "Unlit/Unlit_Color"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white"{ }
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        LOD 100

        Pass
        {
            HLSLPROGRAM //这个有变化
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog //为雾效生成多个着色器变体

            //核心库有变化
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            //URP 用 后缀缩写 明确表示空间：
            //OS = Object Space
            //WS = World Space
            //VS = View Space
            //CS = Clip Space
            //NDC = Normalized Device Coordinates
            //顶点着色器输入（从Mesh读取）
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            //顶点着色器输出/片元着色器输入（经过光栅化插值）
            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float fogCoord : TEXCOORD1;
                float4 positionCS : SV_POSITION;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;

            Varyings vert(Attributes v)
            {
                Varyings o;
                //变换函数有变化
                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.fogCoord = ComputeFogFactor(o.positionCS.z);
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                //纹理采样和雾效有变化
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                col.rgb = MixFog(col.rgb, i.fogCoord);
                return col;
            }

            ENDHLSL
        }
    }
}
