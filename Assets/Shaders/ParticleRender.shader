Shader "Custom/ParticleRender"
{
    Properties
    {
        _MainTex ("Particle Texture", 2D) = "white" {}
        _ParticleSize ("Particle Size", Range(0.01, 1.0)) = 0.05
        _ParticleColor ("Particle Color", Color) = (0.2, 0.6, 1.0, 1.0)
        _GlowPower ("Glow Power", Range(0.1, 5.0)) = 2.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
        LOD 100

        // 加法混合，用于发光效果
        Pass
        {
            Name "FORWARD"
            
            Blend One One
            ZWrite Off
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"

            struct Particle
            {
                float3 position;
                float life;
                float3 velocity;
                float maxLife;
            };

            StructuredBuffer<Particle> _ParticleBuffer;
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ParticleSize;
            float4 _ParticleColor;
            float _GlowPower;

            struct appdata
            {
                uint vertexID : SV_VertexID;
                uint instanceID : SV_InstanceID;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float fade : TEXCOORD1;
            };

            // Quad vertices (6 vertices for 2 triangles)
            static const float2 quadVertices[6] = 
            {
                float2(-0.5, -0.5), // 0 - bottom left
                float2( 0.5, -0.5), // 1 - bottom right
                float2(-0.5,  0.5), // 2 - top left
                float2(-0.5,  0.5), // 2 - top left
                float2( 0.5, -0.5), // 1 - bottom right
                float2( 0.5,  0.5)  // 3 - top right
            };

            static const float2 quadUVs[6] = 
            {
                float2(0, 0),
                float2(1, 0),
                float2(0, 1),
                float2(0, 1),
                float2(1, 0),
                float2(1, 1)
            };

            v2f vert (appdata v)
            {
                v2f o;

                // 获取当前实例的粒子数据
                Particle p = _ParticleBuffer[v.instanceID];

                // 计算生命值比例
                float lifeRatio = saturate(p.life / p.maxLife);
                
                // 计算速度大小用于颜色变化
                float speed = length(p.velocity);
                
                // 获取 quad 顶点偏移
                float2 quadOffset = quadVertices[v.vertexID];
                float2 uv = quadUVs[v.vertexID];

                // 将粒子位置转换到视图空间
                float3 centerWorld = p.position;
                float4 centerClip = UnityWorldToClipPos(float4(centerWorld, 1.0));
                
                // 计算屏幕空间的粒子大小
                float size = _ParticleSize * lifeRatio;
                
                // 在屏幕空间偏移顶点
                float2 screenOffset = quadOffset * size;
                
                // 考虑屏幕宽高比
                screenOffset.x *= _ScreenParams.y / _ScreenParams.x;
                
                // 应用偏移
                o.vertex = centerClip;
                o.vertex.xy += screenOffset * centerClip.w;
                
                // 传递 UV
                o.uv = uv;

                // 计算颜色 - 基于生命值和速度
                float3 baseColor = _ParticleColor.rgb;
                
                // 速度越快颜色越亮/偏白
                float speedFactor = saturate(speed / 10.0);
                float3 hotColor = lerp(baseColor, float3(1.0, 0.8, 0.4), speedFactor * 0.5);
                
                // 生命值低时淡出
                float alpha = smoothstep(0.0, 0.3, lifeRatio);
                
                o.color = float4(hotColor, alpha);
                o.fade = alpha;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 采样纹理
                fixed4 texCol = tex2D(_MainTex, i.uv);
                
                // 如果没有纹理，使用圆形渐变
                if (length(texCol.rgb) < 0.01)
                {
                    float2 centerUV = i.uv - 0.5;
                    float dist = length(centerUV);
                    float circle = 1.0 - smoothstep(0.3, 0.5, dist);
                    texCol = fixed4(circle, circle, circle, circle);
                }

                // 应用粒子颜色和发光
                fixed4 col = texCol * i.color;
                col.rgb *= _GlowPower;
                
                return col;
            }
            ENDCG
        }
    }
    
    Fallback Off
}
