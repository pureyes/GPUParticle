# URP Lit.shader 源码阅读笔记

> **版本**: URP 14.0.12 (Unity 2022.3 LTS 对应版本)  
> **目标**: 先建立地图，不抠细节。看懂骨架，再逐层深入。

---

## 一、Shader 入口结构图

```
Lit.shader  (入口声明)
│
├── SubShader
│   ├── Tags { "RenderPipeline" = "UniversalPipeline" }
│   │
│   ├── Pass: ForwardLit          ← 真正前向渲染（唯一做光照的 Pass）
│   │   LightMode = "UniversalForward"
│   │   #include "LitInput.hlsl"
│   │   #include "LitForwardPass.hlsl"
│   │
│   ├── Pass: ShadowCaster        ← 投射阴影（只写深度，不写颜色）
│   │   LightMode = "ShadowCaster"
│   │
│   ├── Pass: GBuffer             ← Deferred / 延迟渲染路径用
│   │   LightMode = "UniversalGBuffer"
│   │
│   ├── Pass: DepthOnly           ← 深度预渲染（SSAO / 场景深度）
│   │   LightMode = "DepthOnly"
│   │
│   ├── Pass: DepthNormals        ← 法线深度贴图（用于后处理）
│   │   LightMode = "DepthNormals"
│   │
│   ├── Pass: Meta                ← 烘焙光照贴图专用（不参与实时渲染）
│   │   LightMode = "Meta"
│   │
│   └── Pass: Universal2D         ← 2D 渲染器使用
│       LightMode = "Universal2D"
│
└── FallBack "Hidden/Universal Render Pipeline/FallbackError"
```

**一句话总结**:  
如果你只想改**实时光照效果** → 只看 `ForwardLit` Pass。  
其他 Pass 都是为了特定功能（阴影、深度、烘焙）服务的。

---

## 二、Forward Pass 入口函数

**文件**: `LitForwardPass.hlsl`

### 2.1 两个入口函数

```hlsl
// ========== Vertex Shader ==========
Varyings LitPassVertex(Attributes input)
```

**做了什么**:
- 坐标变换: `OS → WS → CS`（`GetVertexPositionInputs`）
- 法线/切线变换到世界空间（`GetVertexNormalInputs`）
- 计算顶点光照 (`VertexLighting`)
- 计算雾效因子 (`ComputeFogFactor`)
- 输出光照贴图 UV、球谐光照 (SH)
- 可选: 插值阴影坐标 (`shadowCoord`)

---

```hlsl
// ========== Fragment Shader ==========
void LitPassFragment(Varyings input, out half4 outColor : SV_Target0)
```

**核心流程**（5 步）:

```
1. 视差映射 (Parallax) — 如果开启了 _PARALLAXMAP，先扰动 UV
         ↓
2. 初始化表面数据 — InitializeStandardLitSurfaceData(uv, surfaceData)
         ↓
3. 初始化输入数据 — InitializeInputData(input, surfaceData.normalTS, inputData)
         ↓
4. 应用 Decal (如果开启了 _DBUFFER)
         ↓
5. PBR 光照计算 — half4 color = UniversalFragmentPBR(inputData, surfaceData)
         ↓
   混合雾效 — color.rgb = MixFog(color.rgb, inputData.fogCoord)
   处理 Alpha — color.a = OutputAlpha(color.a, ...)
```

**重点**: `LitPassFragment` 的代码很短，但把核心逻辑都**委托**给了几个函数。  
这几个函数就是理解 URP PBR 的钥匙。

---

## 三、SurfaceData 是什么

**文件**: `ShaderLibrary/SurfaceData.hlsl`

```hlsl
struct SurfaceData
{
    half3 albedo;              // 基础颜色（贴图 × _BaseColor）
    half3 specular;            // 高光颜色（Specular 工作流下有效）
    half  metallic;            // 金属度（Metallic 工作流下有效）
    half  smoothness;          // 光滑度
    half3 normalTS;            // 切线空间法线
    half3 emission;            // 自发光
    half  occlusion;           // AO 遮蔽
    half  alpha;               // 透明度
    half  clearCoatMask;       // 清漆层遮罩
    half  clearCoatSmoothness; // 清漆层光滑度
};
```

**本质**:  
`SurfaceData` 是**材质属性的大集合**。它回答了"这个像素表面长什么样"。  
所有贴图采样、颜色计算，最终都是为了填满这个结构体。

**从哪来**:  
`InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)`  
定义在 `LitInput.hlsl` 第 249 行。

**关键代码片段**:
```hlsl
half4 albedoAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
outSurfaceData.albedo = albedoAlpha.rgb * _BaseColor.rgb;
outSurfaceData.normalTS = SampleNormal(uv, TEXTURE2D_ARGS(_BumpMap, sampler_BumpMap), _BumpScale);
outSurfaceData.emission = SampleEmission(uv, _EmissionColor.rgb, TEXTURE2D_ARGS(_EmissionMap, sampler_EmissionMap));
```

---

## 四、InputData 是什么

**文件**: `ShaderLibrary/Input.hlsl`

```hlsl
struct InputData
{
    float3  positionWS;              // 世界空间位置
    float4  positionCS;              // 裁剪空间位置
    float3  normalWS;                // 世界空间法线（已归一化）
    half3   viewDirectionWS;         // 世界空间视线方向（归一化）
    float4  shadowCoord;             // 阴影坐标（用于采样 Shadow Map）
    half    fogCoord;                // 雾效坐标
    half3   vertexLighting;          // 逐顶点光照（如果开启了 _ADDITIONAL_LIGHTS_VERTEX）
    half3   bakedGI;                 // 烘焙全局光照（Lightmap + SH）
    float2  normalizedScreenSpaceUV; // 屏幕空间归一化 UV
    half4   shadowMask;              // Shadow Mask（烘焙阴影混合）
    half3x3 tangentToWorld;          // 切线空间到世界空间的变换矩阵

    // ... DEBUG_DISPLAY 相关的额外字段（略）
};
```

**本质**:  
`InputData` 是**光照计算需要的"环境信息"**。它回答了"光从哪来、眼睛在哪、阴影怎么样"。

**与 SurfaceData 的区别**:

| | SurfaceData | InputData |
|---|---|---|
| **描述** | 表面本身长什么样 | 光照环境+几何信息 |
| **来源** | 贴图采样、材质属性 | 顶点插值、坐标变换、光照贴图采样 |
| **类比** | 皮肤的肤色、纹理 | 太阳光方向、阴影、雾 |

**从哪来**:  
`InitializeInputData(Varyings input, half3 normalTS, out InputData inputData)`  
定义在 `LitForwardPass.hlsl` 第 68 行。

---

## 五、UniversalFragmentPBR 是什么

**文件**: `ShaderLibrary/Lighting.hlsl`

```hlsl
half4 UniversalFragmentPBR(InputData inputData, SurfaceData surfaceData)
```

**本质**:  
这是 **URP PBR 光照计算的"总入口"**。  
给定一个像素的位置、法线、视线方向（`InputData`）和表面属性（`SurfaceData`），  
它计算出最终颜色。

**内部核心流程**:

```
1. InitializeBRDFData(surfaceData, brdfData)
   // 根据 Metallic/Specular 工作流，初始化 BRDF 参数
   // 输出: diffuseColor, specularColor, roughness, grazingTerm...
         ↓
2. CreateAmbientOcclusionFactor(inputData, surfaceData)
   // 获取 AO 因子（SSAO / 贴图 AO）
         ↓
3. GetMainLight(inputData, shadowMask, aoFactor)
   // 获取主光源（方向光）+ 计算主光阴影
         ↓
4. MixRealtimeAndBakedGI(mainLight, normalWS, bakedGI)
   // 混合实时光照和烘焙 GI
         ↓
5. 计算主光照贡献
         ↓
6. 遍历额外光源 (Additional Lights)
   // Forward+ / 传统 Forward 遍历
         ↓
7. 计算环境反射 (Reflection Probes / 天空盒)
         ↓
8. 清漆层 (Clear Coat) 额外光照
         ↓
9. 汇总所有光照 + 自发光 + 间接光
         ↓
   return finalColor
```

**关键理解**:  
`UniversalFragmentPBR` 是一个**黑箱式的调度器**。它不关心你的贴图怎么采样，只关心:  
- "表面属性是什么"（`SurfaceData`）  
- "环境信息是什么"（`InputData`）

---

## 六、文件依赖关系图

```
Lit.shader
│
├── LitInput.hlsl                          ← 材质属性、贴图采样、SurfaceData 初始化
│   ├── Core.hlsl                          ← URP 核心工具函数
│   ├── CommonMaterial.hlsl                ← 通用材质函数
│   ├── SurfaceInput.hlsl                  ← 基础 Surface 输入（SampleAlbedoAlpha 等）
│   ├── ParallaxMapping.hlsl               ← 视差映射
│   └── DBuffer.hlsl                       ← Decal 系统
│
├── LitForwardPass.hlsl                    ← Forward Pass 顶点/片元函数
│   ├── Lighting.hlsl                      ← PBR 光照总入口（UniversalFragmentPBR）
│   │   ├── BRDF.hlsl                      ← BRDF 模型（Disney/GGX 等）
│   │   ├── GlobalIllumination.hlsl        ← GI / 环境光 / 反射探针
│   │   ├── Shadows.hlsl                   ← 阴影计算
│   │   ├── RealtimeLights.hlsl            ← 实时光源遍历
│   │   └── ...
│   └── LODCrossFade.hlsl (条件)           ← LOD 交叉淡化
│
├── LitGBufferPass.hlsl                    ← GBuffer Pass（延迟渲染用）
├── ShadowCasterPass.hlsl                  ← 阴影投射 Pass
├── DepthOnlyPass.hlsl                     ← 深度 Only Pass
├── LitDepthNormalsPass.hlsl               ← 法线深度 Pass
└── LitMetaPass.hlsl                       ← 光照贴图烘焙 Pass
```

---

## 七、修改一个效果应该从哪里入手？

### 场景 1: 想改基础颜色/贴图采样逻辑
→ 改 `LitInput.hlsl` 中的 `InitializeStandardLitSurfaceData()`  
→ 或者改 `SurfaceInput.hlsl`（如果是通用逻辑）

### 场景 2: 想改法线计算（比如加自己的法线混合算法）
→ 改 `LitInput.hlsl` 中的 `InitializeStandardLitSurfaceData()`  
→ `outSurfaceData.normalTS` 就是最终传入光照的法线

### 场景 3: 想改光照模型（比如自定义 BRDF）
→ 改 `ShaderLibrary/Lighting.hlsl` 中的 `UniversalFragmentPBR()`  
→ 或者改 `ShaderLibrary/BRDF.hlsl` 中的 `InitializeBRDFData()` / `BRDFSpecular()`

### 场景 4: 想加一个新的 Shader Feature / Keyword
→ 1. 在 `Lit.shader` 的 Properties 里加属性  
→ 2. 在 `Lit.shader` 的 ForwardLit Pass 里加 `#pragma shader_feature_local _MYFEATURE`  
→ 3. 在 `LitInput.hlsl` 里根据 keyword 修改 SurfaceData  
→ 4. 在 `LitForwardPass.hlsl` 里根据 keyword 做额外处理

### 场景 5: 想改阴影接收行为
→ `LitForwardPass.hlsl` 中的 `InitializeInputData()` 设置了 `shadowCoord`  
→ 真正的阴影衰减计算在 `Lighting.hlsl` -> `GetMainLight()` 里

---

## 八、速查表

| 我想知道... | 看哪个文件 |
|---|---|
| Shader 有哪些 Pass？ | `Lit.shader` |
| Forward 顶点/片元入口在哪？ | `LitForwardPass.hlsl` |
| SurfaceData 定义 | `ShaderLibrary/SurfaceData.hlsl` |
| InputData 定义 | `ShaderLibrary/Input.hlsl` |
| PBR 光照计算总入口 | `ShaderLibrary/Lighting.hlsl` → `UniversalFragmentPBR()` |
| BRDF 初始化 | `ShaderLibrary/BRDF.hlsl` → `InitializeBRDFData()` |
| 贴图采样/材质属性 | `LitInput.hlsl` → `InitializeStandardLitSurfaceData()` |
| 阴影计算 | `ShaderLibrary/Shadows.hlsl` |
| 全局光照/GI | `ShaderLibrary/GlobalIllumination.hlsl` |
| 雾效 | `ShaderLibrary/Core.hlsl` → `MixFog()` |

---

## 九、当前理解进度

- [ ] Lit.shader 入口结构和 Pass 分工
- [ ] Forward Pass 的顶点/片元函数骨架
- [ ] SurfaceData 是什么、包含什么字段
- [ ] InputData 是什么、包含什么字段
- [ ] UniversalFragmentPBR 的定位和职责
- [ ] BRDF 内部数学公式（Disney/GGX）
- [ ] 阴影映射的具体算法（PCF/Soft Shadow）
- [ ] Forward+ 额外光源遍历机制
- [ ] 反射探针的混合与 Box Projection
- [ ] Decal 系统的详细流程

> **下一步**: 深入 `Lighting.hlsl`，把 `UniversalFragmentPBR` 的内部调用链逐层展开。
