技术TA（偏技术）90天学习计划
适用对象：有OpenGL底层基础，做过Compute Shader，Unity经验<1年，Shader偏弱 目标：获得技术TA岗位offer 核心策略：把底层原理翻译成Unity工程化能力

📋 个人背景与短板分析
已有优势（不需要再花时间）
 OpenGL图形管线（TinyRenderer、Games101、LearnOpenGL）
 Compute Shader基础（GPU粒子系统Compute部分已懂）
 数学基础（线性代数、变换、投影）
 C++引擎层经验（cocos2dx多年）
 Cocos Creator经验（2年）
当前短板（重点突破）
 Unity URP工程化：Renderer Feature、Render Pass、工具链封装
 Shader能力：HLSL语法、URP Shader Library、能改官方Shader
 工具链思维：Editor工具、Gizmos、美术工作流支持
 代表作：能展示技术+工程能力的完整项目
🎯 三阶段学习计划
阶段一：工程化突围（第1-30天）
目标：把GPU粒子系统做成可复用的生产力工具

Week 1-2: URP Renderer Feature集成
任务清单：

 Day 1-2: 创建Renderer Feature基础框架

 创建GPUParticleRendererFeature.cs
 创建GPUParticleRenderPass.cs
 支持可配置的Render Pass Event（渲染时机）
 替换原有的OnRenderObject()渲染方式
 Day 3-4: 测试不同渲染时机的效果

 在场景中添加不透明物体（Cube）
 在场景中添加透明物体（Plane）
 测试AfterRenderingOpaques（粒子被透明物体遮挡）
 测试AfterRenderingTransparents（粒子在最前面）
 测试BeforeRenderingPostProcessing（受Bloom影响）
 理解渲染顺序对视觉效果的影响
 Day 5-7: 完善Renderer Feature功能

 支持Scene View预览
 支持多相机正确渲染
 支持相机剔除（Frustum Culling）
 添加调试信息输出
产出物：

Renderer Feature代码（GitHub提交）
一张对比图：不同Render Pass Event的渲染效果差异
技术博客：《URP Renderer Feature实战：把Compute Shader粒子集成到管线》
检查标准：

 //粒子在不改变原有MonoBehaviour的情况下，通过Renderer Feature渲染
 可以在Inspector中切换渲染时机并实时看到效果
 Scene View和Game View都正确显示
Week 3-4: Editor工具链开发
任务清单：

 Day 8-10: 自定义Inspector面板

 创建GPUParticleSystemEditor.cs
 基础参数：粒子数量、生命周期、发射半径、速度
 物理参数：重力、阻尼、风力
 交互参数：鼠标交互半径、力度
 实时预览按钮（重置粒子、暂停/继续）
 Day 11-12: Gizmos可视化

 发射器范围可视化（Wire Sphere）
 鼠标交互范围可视化（当鼠标按下时）
 粒子数量统计显示
 选中时显示粒子分布预览
 Day 13-14: Preset系统

 支持保存当前配置为Preset文件
 支持加载Preset
 内置几个默认Preset（爆炸、下雪、魔法等）
 Day 15: 设置向导工具

 创建GPUParticleSetupWizard.cs
 一键查找URP Renderer Data
 一键添加Renderer Feature
 快速创建材质按钮
产出物：

完整的Editor工具代码
使用文档（README）
视频演示：如何在5分钟内配置好粒子系统
检查标准：

 美术同学可以在不看代码的情况下调整粒子效果
 可以保存和分享配置
 有可视化辅助理解发射器范围
阶段二：Shader专项突破（第31-60天）
目标：补齐Shader弱项，达到能改官方Lit Shader的水平

Week 5-6: URP Shader基础迁移
任务清单：

 Day 16-18: OpenGL→HLSL迁移对照

 读HLSL for GLSL Developers文档
 对比 vertex shader 语法差异
 对比 fragment/pixel shader 语法差异
 理解Unity的ShaderLab结构
 Day 19-21: 手写基础Shader

 最简Unlit Shader（纯色）
 添加纹理采样
 添加基础光照（Blinn-Phong）
 添加Normal Map
 添加Roughness/Metallic（简易PBR）
 Day 22-23: 读懂URP官方Shader

 精读Lit.shader
 精读LitForwardPass.hlsl
 理解UniversalFragmentPBR()函数
 理解Shader Keywords系统
 Day 24-25: 修改官方Shader

 给URP Lit添加自定义功能（如边缘发光）
 理解多Pass Shader的写法
 理解Shader Variant和编译优化
产出物：

一套手写的递进式Shader（Unlit→Lit→PBR）
一篇博客：《从OpenGL到HLSL：程序员的URP Shader迁移指南》
能解释清楚URP Shader的架构设计
检查标准：

 能不看文档写出一个带光照的Shader
 能定位并修改URP官方Shader的特定功能
 理解Shader性能优化的基本概念
Week 7-8: 风格化Shader实战
任务清单：

 Day 26-28: 风格化水体Shader

 深度图采样实现深浅颜色混合
 法线扰动实现波浪效果
 边缘泡沫效果（基于深度差）
 简单反射（用反射探针或平面反射）
 顶点动画实现波浪起伏
 Day 29-30: 用Renderer Feature做平面反射

 创建PlanarReflectionRendererFeature
 实现反射相机的设置
 渲染反射纹理
 在水体Shader中采样反射纹理
 优化：只在需要时渲染反射
 Day 31-32: 其他风格化效果（选做1-2个）

 卡通渲染（Toon Shading）+ 描边
 全息投影效果（Hologram）
 护盾/能量场效果
 溶解（Dissolve）效果
产出物：

一个完整的风格化水体Shader（GitHub）
配套的技术博客（重点写深度图和反射的实现）
视频演示：水体效果的参数调节
检查标准：

 水体效果参数可调，实时预览
 反射正确显示场景物体
 性能可控（在目标平台跑满60fps）
阶段三：代表作《程序化神庙》（第61-90天）
目标：整合前两阶段成果，做出能展示技术+工程能力的完整项目

Week 9-10: 项目架构设计
任务清单：

 Day 33-34: 确定项目范围和技术栈

 核心机制：每次运行生成不同风格的神庙
 技术栈确认：
地形：Heightmap + 顶点着色器位移
建筑：Shape Grammar（C#）+ GPU Instancing
氛围：自定义URP Post-processing
粒子：复用阶段一的粒子系统
 明确不做：Tessellation（性价比低）、复杂物理
 Day 35-36: 搭建项目基础

 GitHub仓库搭建
 URP管线配置
 基础场景搭建（相机、光照、后处理）
 第一人称控制器（用Unity内置或简单实现）
 Day 37-38: Shape Grammar基础

 实现基础的建筑生成规则
 支持柱子、墙壁、屋顶的模块化生成
 实现随机参数控制建筑风格（希腊/玛雅）
 GPU Instancing渲染大量建筑部件
产出物：

可运行的基础场景
能生成简单建筑结构
GitHub仓库初始化
Week 11-12: 核心功能实现
任务清单：

 Day 39-41: 程序化地形

 用噪声生成Heightmap
 顶点着色器实现地形细节
 地形材质：根据坡度混合草地/石头/雪地
 神庙位置自动选择（平坦区域）
 Day 42-44: 完善建筑生成

 神庙主体结构生成（基座、柱子、屋顶）
 支持两种风格参数：古希腊（多立克/爱奥尼）vs 玛雅（金字塔）
 添加细节：台阶、装饰、破损效果
 生成碰撞体（用于玩家行走）
 Day 45-46: 氛围和光照

 动态时间系统（昼夜循环）
 体积光效果（使用阶段二写的Shader）
 雾效和大气散射
 火把/光源的粒子效果（复用阶段一的粒子系统）
 Day 47-49: 交互和重建功能

 按空格键触发重建
 重建时的过渡动画（旧建筑消散+新建筑生成）
 简单的行走和跳跃（Character Controller）
 相机后处理（Bloom、Tone Mapping）
产出物：

完整的程序化神庙生成系统
可交互的Demo（探索+重建）
基础音效和BGM
Week 13: polish和发布
任务清单：

 Day 50-52: 性能优化

 GPU Instancing检查
 LOD系统（如果建筑部件多）
 内存和Draw Call分析
 目标：中端显卡60fps
 Day 53-55: 美术 polish

 调整光照和氛围
 添加粒子效果（火把、灰尘、光晕）
 UI设计（开始菜单、重建提示）
 截图和视频录制
 Day 56-58: 文档和宣传

 README详细说明（技术栈、运行方法、截图）
 技术博客：《程序化神庙：从Shape Grammar到URP渲染》
 B站/YouTube视频（5-8分钟，展示生成过程和可调节参数）
 可下载的Windows/Mac构建版本
 Day 59-60: 简历整合

 整理作品集页面（可以用itch.io或个人网站）
 更新简历，突出技术TA相关技能
 准备面试话术（能讲清楚每个技术决策）
产出物：

可玩的完整Demo（GitHub Release）
技术展示视频
详细的技术博客
更新的简历
📚 学习资源清单
URP Renderer Feature/Render Pass
Catlike Coding - Custom SRP（看URP部分）
Unity官方文档：ScriptableRendererFeature
参考项目：UniversalRenderingExamples（你已下载）
Shader（OpenGL→HLSL迁移）
HLSL for GLSL Developers（快速对照表）
Unity URP源码：Packages/com.unity.render-pipelines.universal/Shaders/Lit.shader
书籍：《Unity Shader入门精要》（冯乐乐）
程序化生成
论文/文章：Shape Grammar在建筑生成中的应用
参考项目：Manifold Garden（William Chyr的博客）
算法：Perlin Noise、Simplex Noise、L-system基础
技术TA参考
William Chyr（Manifold Garden的TA，偏管线）
Graphics Programming Weekly
✅ 里程碑检查点
检查点1：第30天（阶段一结束）
 GPU粒子系统有完整的Renderer Feature
 有可视化Inspector和Gizmos
 写了一篇技术博客
 GitHub仓库有完整的README
检查点2：第60天（阶段二结束）
 能手写URP Shader
 完成了风格化水体（带反射）
 能解释URP官方Shader的架构
 写了Shader迁移相关的技术博客
检查点3：第90天（阶段三结束）
 《程序化神庙》Demo可玩
 有技术展示视频
 简历更新完毕
 开始投递简历或接到面试
📝 每周复盘模板
每周结束时，记录以下内容：

## Week X 复盘（日期：XXXX/XX/XX）

### 本周完成的任务
- [ ] 任务1
- [ ] 任务2
...

### 遇到的问题
- 问题描述：
- 解决方案：
- 是否记录到博客：是/否

### 学习时间统计
- 工作日：X小时/天
- 周末：X小时/天
- 总计：X小时

### 下周计划调整
- 原计划：
- 调整原因：
- 新计划：

### 收获与感悟
（记录技术突破、心态变化等）
🎯 成功标准
90天后，你应该能：

技术能力：

能独立开发URP Renderer Feature
能手写中等复杂度的HLSL Shader
能为美术创建可用的Editor工具
作品集：

2个GitHub仓库（GPU粒子系统 + 程序化神庙）
3篇以上技术博客
1个可下载游玩的Demo
1个技术展示视频
面试准备：

能清楚讲解GPU粒子系统的架构
能解释URP渲染管线
能讨论性能优化思路
能展示解决技术问题的过程
最后提醒：

这个计划的前提是每天能投入2-3小时，周末更多
如果遇到技术难点卡住，不要死磕超过3天，及时求助或调整计划
博客要边做边写，不要等做完再写
GitHub提交要频繁，展示开发过程  这个是KIMI AI给的建议，我已经做到第一阶段的工具来了，那第二阶段还有什么需要补充的？
[图片]
claude-sonnet-4.6
第二阶段补充建议
先评估你现在的真实状态
在补充之前，问你几个关键问题，帮你判断第二阶段哪里需要加强：
- 你现在能不看文档写出一个带法线贴图的 Unlit Shader 吗？
- 你看过 URP 的 Lit.shader 源码吗？能大概看懂吗？
- 你对 ShaderLab 的 Properties / SubShader / Pass 结构熟悉吗？

---
原计划的几个明显漏洞
漏洞1：Week 5-6 的 Shader 学习顺序偏陡
原计划从 HLSL 迁移直接跳到"读懂 Lit.shader"，跨度太大。
补充这个过渡层：
补充：Day 18.5（插入在 Day 18 和 19 之间）

理解 ShaderLab 的工程结构（不是语法，是组织方式）：
- Properties 块的作用和 MaterialPropertyDrawer
- SubShader Tags：RenderType / Queue / RenderPipeline
- Pass Tags：LightMode（这个非常重要，URP 靠它识别 Pass）
- Fallback 和 UsePass 的用途

重点理解 LightMode Tag：
- UniversalForward       → 正常渲染
- ShadowCaster          → 投影
- DepthOnly             → 深度预通道
- Meta                  → Lightmap 烘焙
- Universal2D           → 2D 渲染
为什么重要： 很多 TA 面试会直接问"你的 Shader 为什么不投影"，答案就藏在 LightMode 里。

---
漏洞2：没有 Material Property Drawer 的内容
原计划的 Editor 工具是 C# 写的，但 Shader 侧的材质面板美化完全没提。
补充到 Day 22-23（读懂官方 Shader 的同时）：
补充任务：MaterialPropertyDrawer 实战

- [Toggle] 开关关键字
- [Enum] 下拉选择混合模式
- [HDR] HDR 颜色拾取
- [NoScaleOffset] 隐藏 Tiling/Offset
- 自定义 ShaderGUI（继承 ShaderGUI 类）
  → 实现"折叠分组"效果
  → 实现"根据关键字显示/隐藏属性"
产出物补充： 给你 Week 5-6 写的那套 Shader 配一个好看的自定义材质面板，这个细节在作品集里视觉冲击力很强。

---
漏洞3：Shader Keyword 和 Variant 没有实战练习
原计划只说"理解 Shader Keywords 系统"，但没有动手任务。
补充到 Day 23-24：
补充任务：Shader Keyword 实战

目标：给你写的 PBR Shader 加一个可选功能开关

- 用 #pragma shader_feature 定义关键字
  #pragma shader_feature _NORMALMAP
  #pragma shader_feature _EMISSION

- 在 C# 脚本里用 Material.EnableKeyword() 控制
- 理解 shader_feature vs multi_compile 的区别
  → shader_feature：没用到的 variant 会被剔除（适合材质级别）
  → multi_compile：所有组合都编译（适合全局功能）

- 用 Shader Variant Collection 收集用到的 Variant
- 在 Build Settings 里看 Variant 数量变化
为什么重要： Variant 爆炸是移动端 TA 最常遇到的问题，面试必考。

---
漏洞4：深度图的使用没有单独练习就直接进了水体
水体 Shader 里用到深度图，但原计划没有铺垫。建议在做水体之前加一个小练习：
补充到 Day 25.5（水体之前）：
补充任务：深度图独立练习（1天）

- 理解 Unity 深度图的采样方式
  float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv);
  float linearDepth = LinearEyeDepth(depth, _ZBufferParams);

- 写一个"深度可视化 Shader"
  → 把深度值映射成颜色显示出来
  → 在 Scene 里验证远近关系是否正确

- 理解为什么需要在 URP Asset 里开启 Depth Texture
  → Camera → Depth Texture 选项
  → 或者在 Renderer Feature 里 ConfigureInput(ScriptableRenderPassInput.Depth)

- 理解 Depth Buffer 精度问题（Reversed-Z）
  → Unity 在不同平台 Z 值方向不同
  → 用 UNITY_REVERSED_Z 宏处理
这1天的投入，会让水体 Shader 写起来顺畅很多。

---
漏洞5：平面反射的实现有坑，原计划没提醒
原计划 Day 29-30 做平面反射，但有几个坑很容易踩：
补充注意事项和替代方案：
平面反射的几个坑（提前知道可以少踩）：

坑1：反射相机的 cullingMask 和 clearFlags 要单独设置
坑2：反射矩阵的计算（绕水面法线翻转 View Matrix）
     Matrix4x4.Scale(new Vector3(1, -1, 1)) 不够，要用正确的反射矩阵
坑3：开启 GL.invertCulling = true 否则背面剔除方向反了
坑4：反射相机不能继承主相机的 Renderer Feature（会递归渲染）

如果平面反射卡住超过1天，切换到这个替代方案：
→ 用 Reflection Probe（实时模式）模拟反射
→ 或者用 SSR（屏幕空间反射）的简化版本
→ 这两个替代方案在面试中解释起来反而更有深度
  （"我评估了平面反射的性价比，在移动端选择了...方案"）

---
漏洞6：没有性能分析的实战
整个第二阶段缺少对 Shader 性能的量化分析，这是 TA 岗位很看重的能力。
补充到 Week 7-8 结束时（大约 Day 32）：
补充任务：Shader 性能分析实战（1-2天）

工具使用：
- Unity Frame Debugger：逐 Pass 查看渲染调用
- RenderDoc：抓帧分析（比 Frame Debugger 更底层）
- Unity Profiler：GPU 时间线

需要会看的指标：
- SetPass Calls（不是 Draw Calls，这个才是 CPU 瓶颈）
- 顶点数 / 面数
- 纹理内存占用
- Shader 编译时间

实战任务：
- 用 Frame Debugger 分析你的水体 Shader
  → 找出渲染它用了几个 Pass
  → 反射纹理的渲染时机是什么时候
- 对比开启/关闭反射前后的 Draw Call 变化
- 写到博客里（截图 + 数据）
这个能力在简历上写"有 GPU 性能分析经验"，面试时能展示截图，非常加分。

---
整理后的第二阶段补充任务清单
Week 5（Day 16-21）补充：
  ✦ Day 18.5：ShaderLab 工程结构 + LightMode Tag 专项

Week 6（Day 22-25）补充：
  ✦ Day 22-23 同期：MaterialPropertyDrawer + 自定义 ShaderGUI
  ✦ Day 23-24 同期：Shader Keyword 实战（shader_feature vs multi_compile）
  ✦ Day 25.5（新增）：深度图独立练习

Week 7（Day 26-30）补充：
  ✦ Day 29-30 注意：平面反射的坑和替代方案

Week 8（Day 31-32）补充：
  ✦ Day 32（新增）：Frame Debugger + RenderDoc 性能分析实战

---
一个优先级建议
如果时间不够，按这个顺序取舍：
暂时无法在飞书文档外展示此内容

---
最后一个提醒
第二阶段最容易发生的问题是**“看懂了但没写出来”**。
建议每个知识点都遵循这个流程：
读文档/源码（30分钟）
    ↓
手写一遍（不复制粘贴）
    ↓
改掉一个参数看效果变化
    ↓
用 Frame Debugger 验证结果
    ↓
用自己的话写到笔记/博客里
只有走完这个循环，知识才真正是你的。


恭喜你顺利完成阶段一！把GPU粒子系统落地成带工具链的完整工程，这已经是标准的“技术TA”产出了。你的执行力非常强！
基于你拥有优质的底层基础（OpenGL/C++），而且目标是拿下偏技术TA的Offer，原计划的“阶段二：Shader专项突破”非常棒，但为了应对更高级别的硬核面试和实际工业界需求，我建议在阶段二的原计划基础上，补充以下5个核心技术点（也是面试的高频考区）：
1. 工业级Shader的性能与兼容性标准（强烈建议加入 Week 5-6）
你在OpenGL手打出来的Shader在Unity里大概率能跑，但不一定符合Unity的合批规范。面试官非常看重你写的Shader是否“对性能友好”。
- 补充：SRP Batching 兼容
  - 要点： 学习如何在HLSL中使用 CBUFFER_START(UnityPerMaterial) 和 CBUFFER_END 包裹材质属性。懂得为什么不规范的包裹会导致SRP Batching失效。
  - 产出证明： 用Frame Debugger看你写的Shader是否成功触发了SRP Batching。
- 补充：GPU Instancing 支持
  - 要点： 引入 #pragma multi_compile_instancing，使用 UNITY_SETUP_INSTANCE_ID。这为你第三阶段（神庙大量建筑生成）打下必须的基础。
- 补充：跨平台差异处理（OpenGL转Unity避坑）
  - 要点： 理解Unity底层的跨平台宏。比如 UNITY_REVERSED_Z（DirectX和OpenGL的反转Z差异），以及开启抗锯齿后纹理倒置问题（_MainTex_TexelSize.y < 0）。这是你从底层转引擎必须跨越的认知。
2. Shader Graph 工作流集成（强烈建议加入 Week 6 或 7）
现代技术TA不能只写代码。美术通常是用连拖带拽的 Shader Graph (SG)，TA 的职责往往是给他们造“高级节点”。
- 补充：Custom Function 节点开发
  - 要点： 把你手写的复杂 HLSL 算法（比如你阶段一写的某种粒子光照计算，或者复杂的噪声算法）封装成 .hlsl 文件，然后通过 SG 的 Custom Function 节点暴露给美术用。
- 产出标准： 不要只提供一个纯代码的 .shader 文件，同时提供一个基于 SG 的版本，证明你懂得兼容“美术工作流”。
3. URP 光照与阴影底层机制（对标高级TA面试题）
你之前的 OpenGL 经验可能光照是自己算 Dot(N,L)，但在 URP 里你需要调用它的系统。
- 补充：GI（全局光照）与 SH（球谐光照）
  - 要点： 查看 URP 源码是如何处理环境光的（SampleSH）。面试必问：Unity场景里的烘焙光照贴图和光照探针，在你的自定义Shader里是如何接收的？
- 补充：URP 阴影系统
  - 要点： 搞懂怎么在自定义 Shader 采样 URP 的 Shadowmap。理解 Main Light Shadow 和 Additional Light Shadows 的级联（Cascade）和软阴影（Soft Shadow）机制。
4. 深度与颜色缓冲的高阶应用（丰富 Week 7-8 水体效果）
原计划里的水体 Shader 写了“深度混合”和“平面反射”，这点很好。但可以利用 URP 特性做得更“工业化”。
- 补充：Camera Opaque Texture (GrabPass 的替代品)
  - 要点： URP 没有旧管线的 GrabPass。学习开启管线的 Opaque Texture，在 Shader 中采样它来实现水体的**焦散（Caustics）**或者水面下扭曲（Heat Distortion / Refraction），这比单纯的平面反射更具表现力。
- 补充：Compute Shader 与 材质通信
  - 结合你阶段一的优势：使用 DrawMeshInstancedIndirect，也就是你在 Compute Shader 计算出来的只读 Buffer（StructuredBuffer），直接在你的水体或特效片元着色器里读取（_StructuredBuffer[unity_InstanceID]），这是技术TA秀肌肉的绝佳点。
5. Custom Shader GUI（定制材质面板）
原计划在阶段一写了 C# 的 Editor 工具（针对粒子系统）。对于 Shader，你也应该定制它的面板。
- 补充：编写 ShaderGUI 编辑器类
  - 要点： 默认的材质球面板把所有参数一字排开，非常丑。用 C# 继承 ShaderGUI，把材质参数做成可折叠的 Tab（比如：“基础属性”、“水面波浪”、“反射设置”），加入枚举下拉框（比如切换不同质量的水体），以及属性间的联动隐藏（比如勾选“开启泡沫”，下面才显示泡沫颜色调节）。
  - 意义： 这是商业项目中TA最常干的活，极其讨好面试官（体现你的业务同理心）。

---
给你的阶段二时间线微调建议：
你可以把这些补充内容无缝融进原有的 Week 5 - Week 8 中：
- Week 5 (打基建时): 在写 Unlit/Lit 迁移时，顺手就把 SRP Batching / Instancing 宏 / 跨平台宏 加上。跑一下 Frame Debugger。
- Week 6 (改官方Shader时): 重点看源码里的光照探针(SH)和阴影采样部分。顺手写一个 Custom Function 给 Shader Graph 做测试。
- Week 7-8 (做水体时): 引入 Camera Opaque Texture 做折射。水体写完后，花1-2天用 C# 写一个漂亮的 ShaderGUI 面板。
总结
你现在的进度非常惊人！底层图形学（OpenGL的管线思维）是你最大的护城河。阶段二的核心目标就是在你的“底层基建”外面，严丝合缝地套上“Unity URP的工业级规范外壳”（合批、跨平台、工具化、美术友好）。
把这些补充进去，即使是对口大厂（如腾讯、网易、米哈游）的技术TA岗，你的知识面也绝对站得住脚。加油！遇到 Shader 具体语法的坑可以随时交流。


有，而且我建议你把第二阶段从“会写 Shader”升级成“会在 URP 里定位问题、改官方实现、做可交付效果”。
你第一阶段既然已经把工具链做起来了，第二阶段就不要只停留在“写几个 Shader demo”，而要补上 技术TA真正有辨识度的能力：调试、性能、兼容、工作流、可维护性。
我先给结论：
第二阶段建议补充的重点
除了原计划里的：
- HLSL/ShaderLab 迁移
- 手写基础 Shader
- 阅读并修改 URP Lit
- 做一个风格化水体
还应该额外补这 6 类内容：
1. Shader 调试与问题定位能力
2. URP 常用数据源与管线节点理解
3. Shader 变体与性能治理
4. 材质/效果工作流设计能力
5. 跨平台与精度意识
6. 作品展示方式升级：从“效果图”变成“技术案例”

---
一、最应该补的是：Shader 调试能力
很多人会写一点效果，但技术TA面试更看重的是：
- 为什么这个效果在 SceneView 正常、GameView 不正常？
- 为什么 Android 上发灰、闪烁、精度不够？
- 为什么开了 Bloom 后边缘过曝？
- 为什么换个 Renderer Feature 后深度纹理失效？
- 为什么某个 keyword 一开，包体和编译时间暴涨？
所以第二阶段建议你专门加一个小模块：
补充任务A：Shader 调试专项（建议 4~6 天）
要掌握的工具
- Frame Debugger
  - 看 draw call 顺序
  - 看当前 pass 输入输出
  - 看是否真的执行了某个 pass
- RenderDoc
  - 抓帧
  - 看纹理、RT、深度、法线
  - 看 shader 输入、常量、采样结果
- Unity Profiler / Memory Profiler
  - 看材质实例、GC、纹理内存
- URP Debug Display（如果项目版本支持）
  - 看 overdraw、lighting、material debug
你要做的练习
不是学工具本身，而是做几个“定位问题”的案例：
1. 深度图取样错误排查
  - 为什么水体泡沫边缘不对？
  - 是深度纹理没开？坐标空间错？线性化错？
2. 法线空间错误排查
  - 为什么 Normal Map 打光方向怪？
  - TBN 构建错了？切线没导入？法线空间不一致？
3. 透明物体排序问题
  - 为什么水体/护盾穿插异常？
  - 是 Queue、ZWrite、Blend、Render Pass Event 的问题？
4. 后处理前后渲染差异
  - 为什么某个效果不受 Bloom？
  - 为什么换到 Before/After PostProcessing 结果不同？
产出物建议
写一篇很有价值的博客：
- 《URP Shader 调试实战：用 Frame Debugger 和 RenderDoc 定位 4 类常见问题》
这个比“我做了个炫酷 Shader”更像技术TA。

---
二、补上 URP 常用图形数据源理解
你现在的计划里已经有水体和反射，但还可以更系统一点。
技术TA常做的是：告诉美术“这个效果能不能做，成本是多少，需要依赖什么缓冲”。
所以建议你把 URP 里常见可用数据源单独梳理一遍。
补充任务B：URP 常见缓冲/数据源专题（建议 5~7 天）
至少要明确这些东西：
1. Depth Texture
你已经会在水体里用，但还要补：
- 深度图何时可用
- 如何从屏幕 UV 采样
- 如何线性化深度
- 透视相机和正交相机差异
- 深度差可做什么
  - 泡沫
  - 交界线
  - 软粒子
  - 深浅颜色
2. Opaque Texture
- 什么时候需要开启
- 折射/热扭曲/玻璃常见依赖
- 成本与限制
3. Normal / Additional Lights / Shadow 数据
- URP Lit 中光照数据从哪里来
- 主光、附加光、阴影采样的基本流程
- 阴影衰减如何参与最终结果
4. Camera Color Target / RTHandle
如果你做 Renderer Feature，建议补：
- 临时 RT 的申请和释放
- Blit 的基本路径
- 全屏效果的输入输出链路
5. 屏幕空间坐标与空间变换
必须非常熟：
- Object / World / View / Clip / NDC / Screen
- 法线在不同空间下怎么用
- 什么时候在世界空间做效果，什么时候在视空间做更稳
推荐做两个小练习
1. 软粒子 Soft Particle
  - 直接复用你的粒子系统，给粒子加深度淡出
  - 这非常像技术TA会做的“效果质量升级”
2. 折射/热空气扭曲
  - 用 Opaque Texture 实现一个简单扭曲效果
  - 可以做成护盾或者热浪
产出物
- 一个“Shader常用数据源演示场景”
- README 里列出：
  - 深度图案例
  - Opaque Texture案例
  - Soft Particle案例
  - Distortion案例
这会让你的第二阶段更完整，不只是“水体一个点状突破”。

---
三、必须补：Shader 性能与变体治理
这是很多人第二阶段最容易缺的部分，但对技术TA特别重要。
你不一定要做到图形程序员那种极致优化，但至少要知道：
- 哪里会导致 shader variant 爆炸
- 什么时候该用 keyword，什么时候不用
- half / float 的选择
- 透明、过度采样、分支、贴图采样的代价
- 移动端和 PC 的差异
补充任务C：Shader 性能治理（建议 4~5 天）
重点掌握
1. Shader Keywords / Variants
要能解释：
- #pragma multi_compile
- #pragma shader_feature
- local keyword / global keyword
- 为什么 keyword 会导致编译量暴增
- 如何控制组合数量
你至少要做一个实验：
- 写一个 Shader，加入几个 feature 开关
- 统计/观察不同 keyword 组合对变体数量的影响
- 总结“哪些功能适合 keyword，哪些适合材质参数分支”
2. 精度与采样成本
- half 和 float 的区别
- 颜色、法线、中间变量哪些可以 half
- 顶点阶段和片元阶段计算权衡
- 多贴图采样成本
- 移动端尤其注意
3. 透明物体性能
- Overdraw
- ZWrite Off 的影响
- 半透明水体/护盾/特效为什么贵
- 如何做 cheap 版降级
4. Instancing / SRP Batcher 认知
你不一定这阶段全精通，但至少要会：
- Shader 如何支持 GPU Instancing
- URP/SRP Batcher 的基本要求
- 为什么一些写法会破坏批处理
产出物
一篇博客非常加分：
- 《URP Shader 性能避坑：Keywords、Overdraw 与精度选择》
面试官会很喜欢这个方向，因为它像“懂落地的人”。

---
四、补“技术TA视角”的材质工作流设计
你第一阶段已经有工具思维了，第二阶段就不要只做 Shader 文件，还要做 材质参数组织。
技术TA经常不是亲自把效果做到最极致，而是：
- 提供一个稳定的主 Shader
- 给美术暴露合适参数
- 隐藏危险参数
- 提供默认值和预设
- 保证风格统一
补充任务D：做一个“可交付材质模板”
建议你对水体 Shader 不只做效果，还做以下内容：
1. Inspector 参数分组
例如：
- Surface
- Wave
- Foam
- Reflection
- Color
- Debug
2. 参数命名规范
比如：
- _ShallowColor
- _DeepColor
- _FoamThreshold
- _WaveSpeed
- _ReflectionStrength
不要杂乱无章，这是工程能力的一部分。
3. 做参数联动与保护
例如：
- 波浪强度过大时给 warning
- 某功能关闭时相关参数灰掉
- 没有深度图/反射纹理时提示信息
4. Preset / 示例球
你第一阶段已经做了 Preset 系统，这里可以复用：
- 湖水
- 海水
- 魔法水面
- 毒液池
5. 做一个 Debug 可视化模式
比如切换显示：
- 深度差
- 泡沫遮罩
- 反射强度
- 法线扰动
这个特别像技术TA做法，面试展示也很好用。

---
五、补跨平台和兼容意识
如果你目标是技术TA，这部分建议至少具备“知道有哪些坑”。
补充任务E：兼容性检查（建议 3~4 天）
建议你至少验证这些
- DX11 / Vulkan / Metal 中至少一种额外后端的表现
- PC + 移动平台思维差异
  - 精度
  - 贴图带宽
  - overdraw
  - keyword/variant
- Gamma / Linear
  - 颜色为什么不一致
- 法线贴图导入设置
  - 为什么效果发怪
- 深度纹理在不同平台的差异认知
  - 反转Z、坐标系细节至少知道概念
你不一定要真机全测，但至少可以做：
- 一个“兼容性说明文档”
- 写明当前项目验证环境
- 写明已知限制
这会让你的项目显得更专业。

---
六、补一个“非水体”的第二个效果案例
我很建议你第二阶段不要只有水体一个项目。
因为只有水体时，别人会觉得你是“跟教程做了一个效果”；如果再补一个短平快案例，就更像你掌握了方法。
补充任务F：再做一个小而完整的效果
优先推荐以下 3 个：
方案1：Soft Particle + Distortion 护盾
原因：
- 和你第一阶段 GPU粒子系统能联动
- 展示深度图、透明、屏幕采样、参数工作流
- 很像游戏里真实会用的 TA 效果
可包含：
- Fresnel 边缘高亮
- 噪声流动
- 命中时局部脉冲
- 屏幕扭曲
- 深度淡出
方案2：Toon + Outline
原因：
- 能展示你对光照模型替换的理解
- 可以对比 URP Lit 改造思路
- 面试时非常好讲“风格化渲染”
方案3：Dissolve + Burn 边缘
原因：
- 工作量小
- 很适合补齐 keyword / mask / ramp / emission 这些基础
- 容易做成可复用主材质
如果让我选：
- 主项目做水体
- 副项目做护盾/能量场
最适合你现在的技术路径。

---
七、建议你重排第二阶段结构
如果你已经完成第一阶段工具链，第二阶段我建议改成下面这个版本，会更扎实：

---
第二阶段重构版（第31-60天）
模块1：HLSL/URP 基础迁移（5~7天）
目标：
- 能从 GLSL 视角快速进入 HLSL + ShaderLab + URP
任务：
- HLSL vs GLSL 语法对照
- ShaderLab 结构
- URP Shader Library 常用函数
- 空间变换、纹理采样、光照输入
产出：
- 最简 Unlit / Texture / Normal / BlinnPhong / 简化PBR 五个小 Shader

---
模块2：读懂 URP Lit（5~6天）
目标：
- 不只是会抄，而是知道改哪里
任务：
- 阅读 Lit.shader
- 阅读 LitForwardPass.hlsl
- 理解 SurfaceData / InputData
- 理解 UniversalFragmentPBR
- 理解关键词和多 Pass 结构
产出：
- 一份自己的 URP Lit 架构笔记
- 一个“给 Lit 加边缘发光/Fresnel”的改造版本

---
模块3：Shader 调试与数据源（6~8天）
目标：
- 能定位效果问题
任务：
- Frame Debugger
- RenderDoc
- Depth Texture
- Opaque Texture
- Soft Particle
- Distortion
- Debug View
产出：
- 深度/屏幕采样演示场景
- 博客：《URP Shader 调试实战》

---
模块4：主案例 - 风格化水体（8~10天）
目标：
- 做一个完整、可调、可展示的效果
任务：
- 深浅色混合
- 法线扰动
- 泡沫
- 顶点波浪
- 平面反射 Renderer Feature
- 参数面板整理
- Debug 显示模式
产出：
- 水体 Shader
- Planar Reflection Renderer Feature
- 视频展示

---
模块5：副案例 - 护盾/能量场 或 Toon（5~6天）
目标：
- 证明你不是只能做水体
任务：
- Fresnel
- Noise
- Distortion
- Dissolve 或描边
- 参数预设
产出：
- 第二个小案例
- 简短技术总结

---
模块6：性能与变体治理（3~4天）
目标：
- 形成 TA 视角
任务：
- keyword vs shader_feature
- half vs float
- overdraw 观察
- SRP Batcher / Instancing 基础认知
- 移动端注意项整理
产出：
- 一页性能 checklist
- 博客：《URP Shader 性能避坑》

---
八、第二阶段的“检查标准”也该升级
原来的检查标准偏“会做”，我建议改成更像面试标准：
补充后的检查标准
到第60天，你最好达到：
Shader能力
- 能独立写出 Unlit、基础光照、法线、Fresnel、溶解、屏幕扭曲类 Shader
- 能阅读 URP Lit 的核心结构并定位修改点
- 能解释 SurfaceData / InputData / BRDF 这类基本概念
URP理解
- 知道深度图、Opaque Texture、反射纹理在什么阶段生成和使用
- 能解释 Renderer Feature 如何为 Shader 提供额外输入
- 能说清楚一个效果依赖哪些渲染资源
调试能力
- 能用 Frame Debugger 看渲染顺序
- 能用 RenderDoc 定位一次采样/深度问题
- 遇到“效果不对”时，知道先查什么
工程化能力
- Shader 参数组织合理
- 有 Preset / Debug 模式 / 使用文档
- 效果能被别人复用，不是一次性 demo
性能意识
- 知道透明 overdraw 的风险
- 知道 variant 爆炸的原因
- 知道移动端需要注意精度和采样成本

---
九、如果时间有限，优先级怎么排
如果你第二阶段时间很紧，不可能全补，我建议优先级如下：
必补
1. URP Lit 阅读与修改
2. Depth / Opaque Texture 理解
3. Frame Debugger + RenderDoc
4. Shader 变体和基础性能意识
5. 水体做成工程化可交付效果
次优先
6. 第二个小效果案例
7. 跨平台兼容性验证
8. 更系统的材质 Inspector 美化

---
十、最适合你现在的补充方案
结合你的背景，我会建议你第二阶段额外补成下面这组“最值钱组合”：
必做组合
- URP Lit 源码阅读 + 改一个功能
- Soft Particle
- Distortion 护盾
- RenderDoc 抓帧分析一次
- Shader Variant 基础治理
- 水体效果做成可复用材质模板
这组内容和你第一阶段的 GPU 粒子工具链会形成很强的联动，简历上也好写：
- 做过 Renderer Feature
- 做过 可复用 Editor 工具
- 会 URP Shader 开发与官方 Shader 改造
- 具备 图形调试和性能分析能力
- 能把技术能力转成 美术可用工作流
这就很像技术TA了。

---
如果你愿意，我下一步可以直接帮你把第二阶段整理成一个**“按天拆解的增强版计划表（31-60天）”**，包括：
- 每天做什么
- 每天产出什么
- 每周博客写什么
- 每个模块的验收标准
如果需要，我可以继续直接给你一版可执行日程表。
[图片]
就是这个对话有3个模型，可以把这三个模型全部总结下，再给我出一份详细的计划表吗？

三个模型建议综合总结
先梳理三个模型各自的贡献

---
模型一（KIMI）：整体框架设计者
贡献： 提供了完整的90天三阶段骨架
核心逻辑： 把你的OpenGL底层能力"翻译"成Unity工程化能力
优点： 结构清晰、里程碑明确、产出物导向
缺点： 部分内容跨度太大、有几个技术细节没有铺垫、Shader部分偏浅

---
模型二（本对话AI）：细节补充者
贡献： 发现并填补了KIMI计划的6个漏洞
核心补充：
- ShaderLab工程结构 + LightMode Tag
- MaterialPropertyDrawer + 自定义ShaderGUI
- Shader Keyword实战（Variant管理）
- 深度图独立练习（水体前置知识）
- 平面反射的坑和替代方案
- Frame Debugger + RenderDoc性能分析实战

---
模型三（你的背景分析）：约束条件提供者
贡献： 定义了边界条件
核心信息：
- 已有：OpenGL管线、Compute Shader、数学基础、C++/Cocos经验
- 短板：Unity URP工程化、HLSL Shader、Editor工具链、代表作
- 目标：技术TA岗位offer

---
三个模型融合后的核心策略
KIMI的框架（骨架）
    + 本对话AI的补充（血肉）
    + 你的背景约束（起点校准）
    = 一份真正可执行的计划

---

---
综合90天详细计划表

---
总览
暂时无法在飞书文档外展示此内容
每日投入预期： 工作日2-3小时，周末4-5小时

---

---
阶段一：URP工程化突围（Day 1-30）
Week 1-2：URP Renderer Feature集成

---
Day 1-2：Renderer Feature基础框架
学习目标：
理解ScriptableRendererFeature和ScriptableRenderPass的职责分工
任务清单：
□ 创建 GPUParticleRendererFeature.cs
  - 继承 ScriptableRendererFeature
  - 实现 Create() 和 AddRenderPasses()
  - 暴露 RenderPassEvent 枚举到 Inspector

□ 创建 GPUParticleRenderPass.cs
  - 继承 ScriptableRenderPass
  - 实现 Execute() 方法
  - 把原有 OnRenderObject() 里的渲染逻辑迁移进来

□ 在 URP Renderer Asset 里挂载测试
检查标准：
- 粒子在不修改 MonoBehaviour 的情况下通过 Feature 渲染
- Inspector 里能看到 RenderPassEvent 下拉框
参考资料：
- Unity官方文档：ScriptableRendererFeature
- UniversalRenderingExamples 仓库

---
Day 3-4：测试不同渲染时机
学习目标：
理解渲染顺序对视觉效果的实际影响，建立直觉
任务清单：
□ 场景搭建
  - 添加不透明物体（Cube）
  - 添加透明物体（Plane，用透明材质）
  - 确保粒子会和这两类物体产生遮挡关系

□ 逐个测试以下 RenderPassEvent
  - AfterRenderingOpaques    → 粒子被透明物体遮挡
  - AfterRenderingTransparents → 粒子渲染在最前
  - BeforeRenderingPostProcessing → 受Bloom影响
  - AfterRendering           → 完全在后处理之后

□ 截图记录每种情况的视觉差异
产出物：
一张对比图（4种渲染时机的效果对比）
检查标准：
- 能说清楚每种时机适合什么样的粒子效果
- 截图对比差异肉眼可见

---
Day 5-7：完善Renderer Feature功能
任务清单：
□ Scene View 支持
  - 重写 OnCameraPreCull() 处理 Scene 相机
  - 验证 Scene View 和 Game View 都正确显示

□ 多相机支持
  - 测试场景中有两个相机时的渲染结果
  - 确保每个相机独立触发 Feature

□ 相机视锥剔除
  - 在 Execute() 里加入简单的 Frustum Culling
  - 只渲染在相机视野内的粒子

□ 调试信息
  - 在 Inspector 显示当前粒子数量
  - 显示当前使用的 RenderPassEvent
产出物：
- Renderer Feature完整代码（GitHub提交）
- 技术博客：《URP Renderer Feature实战》（500字以上，有代码片段）

---
Week 3-4：Editor工具链开发

---
Day 8-10：自定义Inspector面板
学习目标：
掌握 PropertyDrawer 和 Editor 的写法，理解美术友好的工具设计思路
任务清单：
□ 创建 GPUParticleSystemEditor.cs
  - 继承 UnityEditor.Editor
  - 使用 EditorGUILayout 排版

□ 实现参数分组显示
  基础参数组：
    - 粒子数量（IntSlider，限制范围）
    - 生命周期（FloatField）
    - 发射半径（Slider）
    - 初速度范围（MinMaxSlider）
  
  物理参数组：
    - 重力系数
    - 阻尼系数
    - 风力方向和强度
  
  交互参数组：
    - 鼠标交互半径
    - 交互力度

□ 实现实时控制按钮
  - [重置粒子] 按钮
  - [暂停/继续] 切换按钮
  - 播放状态指示（运行中/已暂停）
检查标准：
- 参数修改实时生效，不需要重新运行
- 按钮有正确的视觉反馈

---
Day 11-12：Gizmos可视化
任务清单：
□ 在 OnDrawGizmos() 和 OnDrawGizmosSelected() 里实现

□ 常驻显示（OnDrawGizmos）
  - 发射器中心点（小球体）
  - 发射范围（WireSphere，半透明）

□ 选中时显示（OnDrawGizmosSelected）
  - 发射范围高亮
  - 粒子数量统计文字（Handles.Label）
  - 风力方向箭头（Handles.ArrowHandleCap）

□ 交互时显示
  - 鼠标按下时：在鼠标位置绘制交互范围圈
  - 用不同颜色区分不同状态

---
Day 13-14：Preset系统
任务清单：
□ 创建 GPUParticlePreset.cs（ScriptableObject）
  - 存储所有可配置参数
  - 支持从当前配置创建

□ 在 Inspector 面板添加 Preset 操作
  - [保存为Preset] 按钮 → 弹出文件保存对话框
  - [加载Preset] ObjectField → 一键应用

□ 内置默认 Preset（创建对应 .asset 文件）
  - 爆炸效果（高速、短生命、向外扩散）
  - 下雪效果（低速、向下、宽范围）
  - 魔法粒子（旋转、发光、长生命）

---
Day 15：一键安装向导
任务清单：
□ 创建 GPUParticleSetupWizard.cs
  - 继承 EditorWindow
  - 菜单入口：Tools / GPU Particle / Setup Wizard

□ 向导功能
  Step 1：检测项目是否使用 URP（检测 UniversalRenderPipelineAsset）
  Step 2：自动找到项目中的 Renderer Data 资产
  Step 3：一键添加 GPUParticleRendererFeature
  Step 4：快速创建默认材质

□ 状态显示
  - 每个步骤显示 ✓ 或 ✗ 状态图标
  - 点击修复按钮自动处理问题
阶段一最终产出物：
GitHub仓库：
  ├── Assets/
  │   ├── GPUParticle/
  │   │   ├── Runtime/
  │   │   │   ├── GPUParticleSystem.cs
  │   │   │   ├── GPUParticleRendererFeature.cs
  │   │   │   └── GPUParticleRenderPass.cs
  │   │   ├── Editor/
  │   │   │   ├── GPUParticleSystemEditor.cs
  │   │   │   └── GPUParticleSetupWizard.cs
  │   │   └── Presets/
  │   │       ├── Explosion.asset
  │   │       ├── Snow.asset
  │   │       └── Magic.asset
  └── README.md（含截图和使用说明）

技术博客：
  - 《URP Renderer Feature实战》
  - 《如何为美术设计好用的粒子工具》

视频演示：
  - 5分钟配置演示

---

---
阶段二：Shader专项突破（Day 31-60）
Week 5-6：URP Shader基础建立

---
Day 31-32：HLSL语法速通
学习目标：
利用已有GLSL/OpenGL经验，快速建立HLSL语感
任务清单：
□ 对照表速读（重点记差异，不是重新学）
  数据类型对比：
    GLSL vec2/vec3/vec4  →  HLSL float2/float3/float4
    GLSL mat4            →  HLSL float4x4
    GLSL sampler2D       →  HLSL Texture2D + SamplerState

  内置函数对比：
    GLSL mix()     →  HLSL lerp()
    GLSL fract()   →  HLSL frac()
    GLSL mod()     →  HLSL fmod()
    坐标系：GLSL UV左下角原点，DirectX/HLSL左上角原点

□ 手写一个最简 HLSL Shader（纯色输出）
  - 理解 ShaderLab 包裹 HLSL 的结构
  - 理解 HLSLPROGRAM / ENDHLSL 块
  - 能让它在 URP 场景里正确显示

□ 理解 URP 的 include 体系
  - #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
  - 常用宏：TransformObjectToHClip()、TEXTURE2D()、SAMPLER()
参考资料：
- HLSL for GLSL Developers（文档速查）
- 《Unity Shader入门精要》第一章

---
⭐ Day 33（补充）：ShaderLab工程结构 + LightMode Tag
这是本对话AI补充的内容，KIMI原计划没有，必须做
学习目标：
理解 URP 如何通过 Tag 识别 Pass 用途，这是很多 Shader Bug 的根源
任务清单：
□ 理解 SubShader Tags
  "RenderType" = "Opaque"          → 决定渲染队列和替换着色器
  "RenderPipeline" = "UniversalPipeline" → 告诉URP这个Shader属于它
  "Queue" = "Geometry"             → 渲染顺序

□ 重点掌握 Pass Tags 的 LightMode
  "LightMode" = "UniversalForward"   → 正常渲染（必须有）
  "LightMode" = "ShadowCaster"       → 让物体投射阴影
  "LightMode" = "DepthOnly"          → 深度预通道
  "LightMode" = "Meta"               → Lightmap烘焙

□ 实验验证（动手测试）
  实验1：把 ShadowCaster Pass 注释掉 → 物体不再投影
  实验2：把 LightMode 改错 → 观察物体消失
  实验3：添加 ShadowCaster Pass → 物体恢复投影

□ 理解 Fallback 的作用
  Fallback "Universal Render Pipeline/Lit"
  → 找不到对应 Pass 时使用 Fallback 里的 Pass
  → ShadowCaster 通常靠 Fallback 继承
检查标准：
- 能回答：为什么我的自定义Shader不投影？
- 能回答：LightMode和RenderType有什么区别？

---
Day 34-36：手写递进式Shader
任务清单：
□ Shader 1：纯色 Unlit
  - 最基础框架，理解 vertex + fragment 结构

□ Shader 2：纹理采样
  - Properties 添加 _MainTex
  - TEXTURE2D + SAMPLER 声明
  - SAMPLE_TEXTURE2D() 采样

□ Shader 3：基础光照（Blinn-Phong）
  - 获取主光源方向（GetMainLight()）
  - 计算漫反射（saturate(dot(N, L))）
  - 计算高光（Blinn-Phong公式）
  - 添加环境光

□ Shader 4：Normal Map
  - 添加法线贴图属性
  - 在切线空间计算法线
  - UnpackNormal() 使用
  - TBN矩阵变换

□ Shader 5：简易PBR
  - 添加 Roughness / Metallic 参数
  - 使用 URP ShaderLibrary 的 BRDF 函数
  - 理解 UniversalFragmentPBR() 的输入结构
产出物：
5个递进的 Shader 文件（全部提交GitHub，每个有注释）

---
Day 37-38：读懂 URP Lit.shader 源码
任务清单：
□ 找到源码位置
  Packages/com.unity.render-pipelines.universal/Shaders/Lit.shader
  Packages/.../ShaderLibrary/Lighting.hlsl

□ 按这个顺序读
  Step 1：先看 Lit.shader 的整体结构（Properties、SubShader、Pass数量）
  Step 2：找到 UniversalForward Pass
  Step 3：找到 #include "LitForwardPass.hlsl"
  Step 4：在 LitForwardPass.hlsl 里找 LitPassFragment()
  Step 5：追踪 UniversalFragmentPBR() 的定义

□ 做笔记：画出调用链
  LitPassFragment()
    → InitializeStandardLitSurfaceData()  // 填充材质参数
    → InitializeInputData()               // 填充渲染输入
    → UniversalFragmentPBR()              // 计算光照
      → GlobalIllumination()              // 全局光照
      → LightingPhysicallyBased()         // 直接光照（循环）
检查标准：
- 能画出 Lit.shader 的调用链图（哪怕是草图）
- 能解释 InputData 和 SurfaceData 各自存什么

---
⭐ Day 39（补充）：MaterialPropertyDrawer + 自定义ShaderGUI
本对话AI补充，配合Day 34-36的Shader使用
任务清单：
□ 常用 MaterialPropertyDrawer 属性标签
  [Toggle(_NORMALMAP)] → 开关关键字，生成 toggle UI
  [Enum(Off,0,Front,1,Back,2)] → 下拉选择
  [HDR] → HDR颜色拾取器
  [NoScaleOffset] → 隐藏Tiling/Offset
  [Header(Surface Options)] → 分组标题
  [Space] → 空行分隔

□ 给你的 PBR Shader 加上这些标签
  - 用 [Header] 把参数分成几组
  - 用 [Toggle] 控制 Normal Map 开关
  - 用 [HDR] 标记自发光颜色

□ 进阶：写一个简单的 ShaderGUI
  - 继承 ShaderGUI 类
  - 实现折叠面板效果（FoldoutHeader）
  - 实现根据关键字显示/隐藏属性
    如：NormalMap勾选时才显示法线贴图槽

□ 效果验证
  - 在Project里选中Material，检查Inspector是否好看
  - 和默认的MaterialPropertyDrawer对比截图

---
⭐ Day 40（补充）：Shader Keyword 和 Variant 实战
本对话AI补充，防止Variant爆炸问题
任务清单：
□ 理解两种关键字声明方式
  #pragma shader_feature _NORMALMAP
  → 没用到的变体会被剔除（适合材质级别的开关）
  
  #pragma multi_compile _FOG_OFF _FOG_ON
  → 所有组合都编译（适合全局功能）

□ 在 Shader 里用关键字控制代码
  #ifdef _NORMALMAP
      // 采样法线贴图
  #else
      // 使用顶点法线
  #endif

□ 在 C# 里控制关键字
  material.EnableKeyword("_NORMALMAP");
  material.DisableKeyword("_NORMALMAP");

□ 查看 Variant 数量
  - 打开 Build Settings → 点击 Shader Stripping
  - 或者在 Edit → Project Settings → Graphics → Shader Stripping
  - 用 Shader Variant Collection 收集实际用到的变体

□ 实验：故意制造 Variant 爆炸
  - 定义 4 个 multi_compile 关键字（每个2个值）
  - 计算理论变体数：2^4 = 16个
  - 和 shader_feature 对比（未使用的会被剔除）
检查标准：
- 能解释 shader_feature 和 multi_compile 的区别
- 能在 Build 前检查和控制 Variant 数量

---
Day 41-42：修改官方Shader
任务清单：
□ 给 URP Lit 添加边缘发光（Rim Light）
  - 复制 Lit.shader 和相关 .hlsl 文件
  - 在 Properties 里添加 _RimColor 和 _RimPower
  - 在 LitPassFragment 里添加 Rim 计算
  - float rim = 1.0 - saturate(dot(viewDir, normalWS));
  - color += _RimColor * pow(rim, _RimPower);

□ 理解修改官方 Shader 的工作流
  - 为什么不直接改 Packages 里的源码
  - 复制到 Assets 目录后如何保持后续可维护
  - 如何追踪 Unity 版本升级后的差异

□ 测试多 Pass 写法
  - 给你的 Rim Shader 加一个 ShadowCaster Pass
  - 验证物体能正确投影

---
Week 7-8：风格化Shader实战

---
⭐ Day 43（补充）：深度图独立练习
本对话AI补充，水体的前置必修课
任务清单：
□ 开启深度纹理
  - URP Asset → Depth Texture 勾选
  - 或者在 RenderPass 里：ConfigureInput(ScriptableRenderPassInput.Depth)

□ 写一个深度可视化 Shader
  - 采样方式：
    float rawDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, 
                     sampler_CameraDepthTexture, uv);
    float linearDepth = LinearEyeDepth(rawDepth, _ZBufferParams);
  
  - 把 linearDepth 映射到 0-1 范围显示
  - 近处红色，远处蓝色（便于肉眼验证）

□ 理解深度值的坐标空间
  - rawDepth：NDC空间，受平台影响（Reversed-Z问题）
  - LinearEyeDepth：转为相机空间线性深度（单位：米）
  - Linear01Depth：归一化到0-1的线性深度

□ 理解 Reversed-Z
  - DirectX（PC/Console）：远处 depth=0，近处 depth=1
  - OpenGL（部分平台）：近处 depth=0，远处 depth=1
  - Unity 用 UNITY_REVERSED_Z 宏自动处理
  - 用 UNITY_NEAR_CLIP_VALUE 处理平台差异

□ 实验验证
  - 在场景里放几个不同距离的物体
  - 用深度可视化 Shader 的材质贴到 Quad 上
  - 确认深度值和实际距离关系正确
检查标准：
- 能看懂深度图的颜色分布含义
- 知道为什么直接用 rawDepth 会有平台问题

---
Day 44-47：风格化水体Shader
任务清单：
□ 基础框架搭建
  - 创建一个平铺的 Plane 作为水面
  - 创建 Water.shader
  - 开启透明混合（Queue = Transparent，ZWrite Off）

□ 深浅颜色混合（基于深度差）
  - 采样 _CameraDepthTexture
  - 计算水面与水底的深度差：
    float waterDepth = LinearEyeDepth(...) - positionSS.w;
  - 用 lerp() 混合浅水色和深水色
  - 添加可调参数：_ShallowColor、_DeepColor、_DepthFalloff

□ 法线扰动实现波浪
  - 准备两张法线贴图（不同大小/方向）
  - 用时间偏移 UV：uv1 += _Time.y * _WaveSpeed1
  - 混合两张法线：lerp(normal1, normal2, 0.5)
  - 用法线扰动采样反射/折射的 UV

□ 边缘泡沫效果
  - 基于深度差，浅处（深度差小）显示泡沫
  - float foam = 1.0 - saturate(waterDepth / _FoamRange);
  - 用泡沫噪声纹理增加细节
  - 泡沫颜色叠加到最终颜色

□ 顶点动画（波浪起伏）
  - 在顶点着色器里偏移 Y 坐标
  - 用 sin() + cos() 组合模拟水波
  - 振幅和频率参数化

---
Day 48-50：平面反射（带替代方案）
任务清单：
□ 方案一：完整平面反射（如果没遇到大坑就做这个）

  Step 1：创建 PlanarReflectionRendererFeature
    - 在 Execute() 里设置反射相机
    - 计算反射矩阵（以水面为镜面）
    
  Step 2：反射矩阵计算
    Matrix4x4 reflectionMatrix = CalculateReflectionMatrix(水面法线, 水面高度);
    reflectionCamera.worldToCameraMatrix = reflectionMatrix * mainCamera.worldToCameraMatrix;
  
  Step 3：注意事项
    - GL.invertCulling = true（渲染前）/ false（渲染后）
    - 反射相机的 cullingMask 排除水面层
    - reflectionCamera.cullingMask &= ~(1 << waterLayer)
  
  Step 4：在水体Shader里采样反射纹理
    float4 reflectionColor = SAMPLE_TEXTURE2D(_ReflectionTex, ...);

□ 方案二：替代方案（卡住超过1天就切换）
  选项A：使用实时 Reflection Probe
    - 放置 Reflection Probe 在水面上方
    - 设置为 Realtime 模式
    - 在 Shader 里用 unity_SpecCube0 采样
  
  选项B：伪反射（Fake Reflection）
    - 把天空盒纹理做上下翻转
    - 用法线扰动 UV，模拟动态反射
    - 性能最好，移动端常用

□ 面试话术准备
  "我评估了三种反射方案：
   平面反射效果最好但额外Draw Call翻倍；
   Reflection Probe适合静态场景；
   伪反射性能最优适合移动端。
   我根据目标平台选择了..."

---
Day 51-53：风格化Shader选做（选1-2个）
选项A：卡通渲染（推荐，技术TA面试常考）
□ 色阶化光照（Toon Shading）
  - 把连续光照值量化：
    float toon = floor(diffuse * _Steps) / _Steps;
  - 或者用 Ramp Texture 映射光照

□ 描边（Outline）
  - Pass 1：正常渲染
  - Pass 2（描边Pass）：
    沿法线方向膨胀顶点
    positionOS.xyz += normalOS * _OutlineWidth;
    只渲染背面（Cull Front）
    输出纯黑色

□ 高光硬化（卡通高光）
  - step() 函数量化高光
  float spec = step(0.5, pow(specular, _Glossiness));
选项B：溶解效果（次推荐，粒子配合效果好）
□ 基础溶解
  - 噪声纹理采样
  - 用 clip() 函数裁切
    clip(noise - _DissolveAmount);

□ 边缘发光
  - 在溶解边缘附近添加颜色
    float edge = saturate(noise - _DissolveAmount) / _EdgeWidth;
    color += _EdgeColor * (1 - edge);

---
⭐ Day 54-55（补充）：性能分析实战
本对话AI补充，体现工程意识
任务清单：
□ Frame Debugger 实战
  - Window → Analysis → Frame Debugger → Enable
  - 找到水体的渲染 Draw Call
  - 记录：Draw Call 数量、顶点数、面数
  - 找到反射纹理的渲染时机
  - 截图保存（用于博客和面试展示）

□ RenderDoc 抓帧（可选，有余力做）
  - 下载并安装 RenderDoc
  - 在 Unity 里接入 RenderDoc 插件
  - 抓一帧，分析 Pipeline State
  - 查看 Pixel History（选中一个像素，看它被哪些Pass修改过）

□ Profiler GPU 时间线
  - Window → Analysis → Profiler → GPU Usage
  - 找到耗时最多的 Pass
  - 对比开启/关闭反射的 GPU 时间差异

□ 量化记录（这些数据写到博客里）
  场景配置：粒子N个 + 水体 + 反射
  Draw Calls：X次
  SetPass Calls：X次（这个才是CPU瓶颈）
  GPU时间：Xms（60fps要求<16ms）
  关闭反射后GPU时间：Xms（说明反射开销是Xms）
检查标准：
- 能用数据说明你的优化决策
- 简历上可以写"有Frame Debugger和RenderDoc使用经验"

---
Day 56-58：Shader体系整理
任务清单：
□ 整理 GitHub 仓库
  Shaders/
  ├── Basic/
  │   ├── 01_Unlit_Color.shader
  │   ├── 02_Unlit_Texture.shader
  │   ├── 03_BlinnPhong.shader
  │   ├── 04_NormalMap.shader
  │   └── 05_SimplePBR.shader
  ├── URP/
  │   ├── CustomLit_RimLight.shader（改官方的）
  │   └── Water.shader
  └── Stylized/
      └── Toon.shader（或Dissolve.shader）

□ 写技术博客：《从OpenGL到HLSL：URP Shader迁移指南》
  必须包含：
  - 语法对比表格（GLSL vs HLSL）
  - LightMode Tag 说明（本对话AI补充的重点）
  - Variant 管理（本对话AI补充的重点）
  - 深度图使用（本对话AI补充的重点）
  - 附带 Frame Debugger 截图

---
Day 59-60：阶段二复盘和阶段三准备
任务清单：
□ 复盘检查
  - [ ] 能不看文档写出带法线贴图的 Lit Shader
  - [ ] 能解释 LightMode Tag 的作用
  - [ ] 能解释 shader_feature vs multi_compile
  - [ ] 能用 Frame Debugger 分析一帧
  - [ ] GitHub 上有完整的 Shader 系列
  - [ ] 写了至少2篇技术博客

□ 阶段三技术预研
  - 快速浏览 Shape Grammar 相关文章（30分钟）
  - 看一个程序化生成建筑的参考Demo
  - 确认 Perlin Noise 的实现方式
  - 列出阶段三的技术风险点

---

---
阶段三：代表作《程序化神庙》（Day 61-90）
Week 9-10：架构搭建

---
Day 61-62：项目规划和技术栈确认
任务清单：
□ 明确做什么（Scope定义）
  核心玩法：
    - 玩家进入场景，看到一座程序化生成的神庙
    - 按 Space 键触发重建：旧建筑消散，新建筑生成
    - 玩家可以走动和探索

  技术栈：
    - 地形：Heightmap + 顶点偏移
    - 建筑：Shape Grammar（C#）+ GPU Instancing
    - 渲染：阶段二写的风格化Shader
    - 粒子：复用阶段一的粒子系统（火把烟雾）
    - 后处理：Bloom + Tone Mapping + Vignette

□ 明确不做
  - 不做物理破坏（太复杂）
  - 不做网络
  - 不做复杂 AI
  - Tessellation 性价比低，不做

□ 创建 GitHub 仓库
  - 初始化 Unity 项目（URP模板）
  - 配置 .gitignore（忽略 Library/ 等）
  - 写初始 README（说明项目目标和技术栈）
  - 第一次提交

---
Day 63-64：基础场景搭建
任务清单：
□ URP 管线配置
  - 创建 URP Asset（Medium Quality 模板）
  - 开启 Depth Texture（水体/粒子需要）
  - 开启 Opaque Texture（如需要折射）
  - 挂载阶段一的 GPUParticleRendererFeature

□ 光照设置
  - 主方向光（太阳）
  - 设置 Shadow Distance（建议 100m）
  - 环境光（Skybox 或 Gradient）

□ 基础场景
  - 空旷的地形占位（先用 Plane 代替）
  - 主相机配置
  - 第一人称控制器（用 Character Controller 组件）
    简单实现：WASD移动 + 鼠标视角 + 空格跳跃

□ 后处理配置
  - 添加 Global Volume
  - Bloom（Threshold:1, Intensity:0.5）
  - Tone Mapping（ACES 模式）
  - Vignette（轻微）

---
Day 65-67：Shape Grammar基础
任务清单：
□ 理解 Shape Grammar 核心思想
  规则示例：
    神庙 → 基座 + 柱子列 + 屋顶
    柱子列 → [柱子] * N（N由宽度决定）
    基座 → 多层台阶
    屋顶 → 三角山花 + 屋顶板

□ 实现建筑部件数据结构
  [System.Serializable]
  public class TempleModule {
      public Mesh mesh;
      public Vector3 position;
      public Quaternion rotation;
      public Vector3 scale;
      public Material material;
  }

□ 实现基础生成器
  public class TempleGenerator : MonoBehaviour {
      public int columnCount = 6;    // 柱子数量
      public float columnSpacing = 2f;
      public float templeWidth = 12f;
      
      void Generate() {
          // 1. 生成基座
          // 2. 生成柱子（两排）
          // 3. 生成屋顶
      }
  }

□ 用 GPU Instancing 渲染部件
  - 把同类型的部件（如柱子）合批渲染
  - 使用 Graphics.DrawMeshInstanced()
  - 对比普通 GameObject 的 Draw Call 数量差异

---
Day 68-70：程序化地形
任务清单：
□ 用 C# 生成 Heightmap
  - 创建 ProceduralTerrain.cs
  - 用 Mathf.PerlinNoise() 生成高度值
  - 多层叠加（Octave Noise）增加细节：
    float height = 0;
    for (int i = 0; i < octaves; i++) {
        height += Mathf.PerlinNoise(x * freq, y * freq) * amplitude;
        freq *= 2; amplitude *= 0.5f;
    }
  - 把 Heightmap 应用到 Terrain 或自定义 Mesh

□ 地形材质（基于坡度混合）
  - 在顶点着色器里根据 worldNormal.y 判断坡度
  - 平坦处：草地纹理
  - 较陡处：石头纹理
  - 顶部：雪地纹理（可选）
  - 用 lerp() 平滑混合

□ 神庙选址算法
  - 遍历地形，找一块足够平坦的区域
  - 计算区域内高度差，小于阈值则选中
  - 在选中区域把地形压平（避免建筑悬空）

---
Week 11-12：核心功能实现

---
Day 71-74：完整建筑生成
任务清单：
□ 神庙风格系统
  [System.Serializable]
  public class TempleStyle {
      public enum StyleType { Greek, Mayan }
      public StyleType style;
      public float columnHeight = 5f;
      public int columnCount = 6;
      public Color primaryColor;
      public float weatheringAmount;  // 破损程度
  }

□ 希腊风格（多立克/爱奥尼柱）
  - 柱身：圆柱体（程序化生成圆柱 Mesh）
  - 柱头：方形顶板
  - 额枋（Entablature）
  - 三角山花（Pediment）

□ 玛雅风格（金字塔）
  - 层叠的台阶结构
  - 越往上越小（缩放参数）
  - 顶部祭坛

□ 破损/风化效果
  - 用 Dissolve 思路：随机裁剪掉部分顶点
  - 或者在材质上叠加裂缝纹理
  - weatheringAmount 参数控制破损程度

□ 生成碰撞体
  - 简化碰撞体（不需要完全匹配视觉Mesh）
  - 用 BoxCollider 近似台阶和柱子

---
Day 75-77：氛围和动态光照
任务清单：
□ 昼夜循环系统
  public class TimeOfDay : MonoBehaviour {
      [Range(0, 24)] public float timeOfDay = 12f;
      public Light sunLight;
      
      void Update() {
          float angle = (timeOfDay / 24f) * 360f - 90f;
          sunLight.transform.rotation = Quaternion.Euler(angle, 0, 30f);
          // 根据时间调整光照颜色（日出橙色、正午白色、黄昏红色）
      }
  }

□ 体积光/光柱效果
  - 用阶段二写的 Shader 技术
  - 在神庙入口添加光柱效果（可以用 Particle + 自定义Shader）
  - 或者用 URP 的 Decal 投影光影

□ 火把粒子效果
  - 复用阶段一的 GPU 粒子系统
  - 配置为火焰 Preset
  - 在神庙关键位置放置火把
  - 火把对应的点光源（动态光照）

□ 环境粒子
  - 漂浮的灰尘粒子（低速、随机游走）
  - 神庙周围的叶片（可选）

---
Day 78-80：重建系统和交互
任务清单：
□ 重建触发逻辑
  void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
          StartCoroutine(RebuildSequence());
      }
  }

□ 重建过渡动画（Coroutine）
  IEnumerator RebuildSequence() {
      // Phase 1：旧建筑消散（1.5秒）
      yield return StartCoroutine(DissolveOldTemple());
      
      // Phase 2：生成新神庙（随机新风格）
      RandomizeStyle();
      Generate();
      
      // Phase 3：新建筑出现动画（1.5秒）
      yield return StartCoroutine(AppearNewTemple());
  }

□ 消散效果实现
  - 给建筑材质添加 _DissolveAmount 参数
  - 用 DOTween 或 Coroutine 动画化这个参数
  - 消散完成后销毁/隐藏 GameObject

□ 出现动画
  - 逆向的消散效果
  - 或者从地面升起的动画（Y 轴位移）

□ UI 提示
  - 屏幕下方显示"按 Space 重建"提示
  - 重建过程中显示进度（简单文字即可）

---
Week 13：优化、Polish和发布

---
Day 81-83：性能优化
任务清单：
□ Draw Call 优化检查
  - 打开 Frame Debugger 统计 Draw Call
  - 确认 GPU Instancing 正确生效
    （同类型建筑部件应该合批）
  - 目标：整个场景 Draw Call < 100

□ LOD 系统（如果建筑部件超过50种）
  - 对柱子等重复部件设置 LOD Group
  - LOD0：正常细节（近距离）
  - LOD1：简化版（中距离）
  - LOD2：极简版（远距离）

□ 内存检查
  - Window → Analysis → Memory Profiler
  - 检查纹理内存占用（是否有不必要的超大纹理）
  - 检查 Mesh 数量（确认 GPU Instancing 没有创建大量重复Mesh）

□ 性能目标验证
  - 中端显卡（GTX 1060 或同级别）
  - 目标：1080p 60fps
  - 用 Profiler 的 GPU Usage 模块确认

---
Day 84-86：美术Polish
任务清单：
□ 光照调整
  - 调整阴影质感（Shadow Distance、Cascade）
  - 大气散射感（雾效：Fog设置）
  - 找几个好看的时间段截图

□ 粒子效果完善
  - 火把火焰：调整颜色、大小、速度
  - 光晕效果（用 Lens Flare 组件）
  - 神庙周围的漂浮粒子效果

□ UI 完善
  - 简洁的开始/标题界面
  - 操作说明（WASD移动、Space重建、ESC退出）
  - 字体选择（推荐古典风格字体）

□ 截图和录制
  - 选5-6个好角度截图（用作 README 和作品集）
  - 录制游戏视频（OBS或Unity Recorder）
  - 录制技术展示视频（展示参数调节和重建过程）

---
Day 87-89：文档和宣传
任务清单：
□ GitHub README 更新
  # 程序化神庙（Procedural Temple）
  
  ## 技术特性
  - Shape Grammar 建筑生成
  - GPU Instancing 优化渲染
  - URP Renderer Feature 粒子系统
  - 风格化水体 Shader（如果场景有水）
  - 自定义 Editor 工具
  
  ## 技术栈
  Unity 2022.x + URP 14.x
  
  ## 截图
  [多张截图]
  
  ## 如何运行
  [详细步骤]
  
  ## 技术博客
  [链接]

□ 技术博客：《程序化神庙完整复盘》
  必须包含的章节：
  - Shape Grammar 设计思路
  - GPU Instancing 的 Draw Call 对比数据
  - 消散效果的实现方法
  - 性能优化过程（有数据）
  - 遇到的问题和解决方案

□ 视频制作（B站）
  时长：5-8分钟
  脚本结构：
    00:00 - 00:30  效果展示（先勾引观众）
    00:30 - 02:00  技术架构讲解
    02:00 - 04:00  核心功能演示（重建动画、参数调节）
    04:00 - 06:00  技术细节深挖（选1-2个讲清楚）
    06:00 - 07:00  总结和展望

□ 可下载构建
  - Windows Build（64位）
  - 上传到 itch.io 或 GitHub Release

---
Day 90：简历整合和投递准备
任务清单：
□ 简历更新
  技术技能部分：
  - Unity URP：Renderer Feature、自定义Render Pass、后处理
  - Shader：HLSL、URP Shader Library、风格化渲染
  - Editor工具：自定义Inspector、Gizmos、EditorWindow
  - 图形编程：Compute Shader、GPU Instancing
  - 性能优化：Frame Debugger、RenderDoc、Profiler

  项目经历：
  项目1：GPU粒子系统工具
    - 基于Compute Shader + URP Renderer Feature
    - 包含完整Editor工具链（可视化Inspector、Preset系统）
    - 链接：[GitHub] [博客]

  项目2：程序化神庙
    - Shape Grammar建筑生成 + 风格化渲染
    - GPU Instancing优化，Draw Call < 100
    - 链接：[GitHub] [视频] [可玩Demo]

□ 作品集页面
  推荐：itch.io 项目页面 或 个人网站
  包含：截图、视频嵌入、技术说明、下载链接

□ 面试话术准备
  问题1："介绍一下你的GPU粒子系统"
  → 讲架构：Compute Shader计算 → Renderer Feature渲染 → Editor工具封装
  → 讲难点：渲染时机的选择，为什么用Feature而不是OnRenderObject
  → 讲数据：支持多少粒子，帧率多少

  问题2："你了解URP的渲染管线吗"
  → 画出：CPU提交 → Culling → Opaque → Skybox → Transparent → PostProcess
  → 说出各个LightMode Tag对应的时机

  问题3："你是怎么调试Shader问题的"
  → Frame Debugger：找到渲染时机和Pass
  → RenderDoc：像素级调试
  → 输出中间值：用颜色可视化法线、深度等中间结果

□ 开始投递
  目标公司类型：
  - 中大型游戏公司的TA岗（技术美术/Shader工程师）
  - 引擎/工具组的技术开发
  - 外包/中间件公司的图形工程师

---

---
总览时间轴
Week 1  (Day 1-7)   Renderer Feature 集成
Week 2  (Day 8-15)  Editor 工具链
Week 3  (Day 16-22) HLSL迁移 + ShaderLab结构[补充]
Week 4  (Day 23-30) 递进Shader + Keyword实战[补充]
Week 5  (Day 31-37) 读Lit源码 + 修改官方Shader + ShaderGUI[补充]
Week 6  (Day 38-44) 深度图练习[补充] + 水体Shader
Week 7  (Day 45-51) 平面反射 + 风格化效果选做
Week 8  (Day 52-60) 性能分析[补充] + 整理 + 阶段三预研
Week 9  (Day 61-67) 神庙项目架构 + Shape Grammar基础
Week 10 (Day 68-74) 地形生成 + 完整建筑生成
Week 11 (Day 75-80) 氛围光照 + 粒子 + 重建系统
Week 12 (Day 81-86) 性能优化 + 美术Polish
Week 13 (Day 87-90) 文档 + 视频 + 简历 + 投递

---
三个检查点
Day 30 检查：
  □ Renderer Feature 正常工作（可截图证明）
  □ Editor 工具美术可用
  □ 博客至少1篇
  □ GitHub 有 README 和截图

Day 60 检查：
  □ 能手写带法线贴图的 Shader
  □ 能解释 LightMode Tag（本对话AI补充的重点）
  □ 用 Frame Debugger 分析过水体的渲染
  □ GitHub 有完整 Shader 系列
  □ 博客至少2篇

Day 90 检查：
  □ 神庙 Demo 可玩并可下载
  □ 技术展示视频发布
  □ 简历更新完毕
  □ 已开始投递或收到面试邀请


理解了！你的意思是将 原计划（Kimi的底层搬运思路）、我补充的建议（工业级规范与工作流），以及 高级技术TA的实际开发标准（隐形的第三重视角） 进行全面整合，为你量身定制一份**“终极版全栈技术TA突围计划表”**。
既然你已经完成了第一阶段（粒子系统与工具链），这是一个巨大的优势！接下来的计划表将把重心完全放在第二部分（Shader深度工业化）和第三部分（项目架构与性能统筹），并且每一天都有明确的“防坑指南”和“面试考点”。
以下是为你重新编制的**《技术TA（重技术）90天终极突围计划》**（从第二阶段：第31天开始）：

---
🎯 终极版技术TA突围计划（阶段二 & 阶段三）
核心指导思想：底层原理 ➡️ 引擎特性 ➡️ 美术工作流 ➡️ 性能压榨
🛡️ 阶段二：Shader工业化与高级特性（第31-60天）
目标告别玩具级代码，写出能合批、跨平台、美术易用且能进大厂项目的工业级Shader。
Week 5：HLSL换血与URP性能规范（底层机制日）
任务：将OpenGL思维装进Unity URP的性能框架中。
- Day 31-32：跨平台架构与宏定义
  - 对照学习 OpenGL 转 HLSL 语法。
  - TA重点：学习并测试Unity跨平台宏：UNITY_REVERSED_Z（处理DX和GL深度反转问题）、_MainTex_TexelSize.y < 0（处理抗锯齿倒置问题）。
- Day 33-34：打通动态合批（SRP Batcher）
  - 手写最简单的 Unlit/Blinn-Phong Shader。
  - TA重点：使用 CBUFFER_START(UnityPerMaterial) / CBUFFER_END 规范包裹材质变量。
  - 检验：打开 Frame Debugger，确保材质球成功触发了 “SRP Batcher”（面试极高频考点）。
- Day 35-37：GPU Instancing 彻底点亮
  - 在代码中加入 #pragma multi_compile_instancing 和 UNITY_SETUP_INSTANCE_ID。
  - 结合 Compute Shader 写一个 DrawMeshInstancedIndirect 的Demo，把阶段一算的粒子位置直接塞给HLSL渲染（不走CPU）。此技法将为第三阶段造神庙打下基石。
Week 6：光影解剖与美术工作流（实战协同日）
任务：扒掉URP官方Shader的外衣，并学给美术造工具。
- Day 38-40：解剖 URP Lit LitForwardPass
  - 精读 Lit.shader，重点看光照计算模块。
  - TA重点：理解 URP 如何获取环境光（SampleSH 球谐光照），如何采样级联阴影（Main Light Shadows 和 Additional Light Shadows）。
- Day 41-42：Shader Graph 工业化封装
  - 在HLSL中写一个复杂的自定义节点算法（如：高质量噪声或各向异性高光）。
  - TA重点：将其封装为 Custom Function 节点，暴露给 Shader Graph，使得不懂代码的美术可以连线使用。
- Day 43-44：多Pass与Shader变体（Variants）控制
  - 学习 multi_compile 和 shader_feature 的区别。
  - 给 URP Lit 添加一个“受击边缘泛光”效果的变体，使用关键字来开启和关闭。
Week 7：风格化水体与深度/颜色缓冲（特效突破日）
任务：利用URP专属特性，做出现代感的水体效果。
- Day 45-47：深度图（Depth）与颜色图（Opaque Texture）
  - TA重点：摒弃旧版 GrabPass，开启URP的 Opaque Texture。
  - 采样深度图（_CameraDepthTexture）计算深度差，实现水岸边缘泡沫。
  - 采样颜色图（_CameraOpaqueTexture）加上法线扰动，实现水下景物的折射扭曲（Refraction）。
- Day 48-49：平面反射（Planar Reflection）
  - 重温阶段一：写一个 Renderer Feature，开启一个带斜椎体裁剪（Oblique Projection）的倒影相机，生成实时水面反射RT。
- Day 50-51：顶点动画与焦散（Caustics）
  - 用 Gerstner Wave 算法写顶点位移动画。
  - 追加单张图采样或Voronoi噪声实现水底焦散效果。
Week 8：编辑器面板定制与总结（“表面工程”日）
任务：把技术包装得极其专业，让面试官眼前一亮。
- Day 52-54：Custom Shader GUI 面板开发
  - 用 C# 继承 ShaderGUI，为你上周写的水体创建一个优雅的 Inspector 面板。
  - TA重点：加入 Foldout 折叠菜单（如【基础属性】、【折射控制】）、枚举下拉框（高质/低质水切换）、互斥开关（禁用折射时隐藏对应的参数）。
- Day 55-57：阶段性整合与录屏
  - 产出：水体效果 + ShaderSG连线版 + 完美的面板GUI演示视频。
- Day 58-60：技术反刍库与博客撰写
  - 产出博客：《URP管线下的高级渲染：从SRP合批兼容到自定义Shader GUI》。

---
🏰 阶段三：代表作《程序化神庙》（第61-90天）
任务：整合所有技能栈（GPU计算、C#架构、高级材质、管线Feature），打造终极面试敲门砖。
Week 9 & Week 10：程序化生成与海量渲染（Day 61-74）
- 地形与基础建筑生成
  - C# 编写 Shape Grammar（形状文法）生成神庙的柱子、墙壁、阶梯。
  - Compute Shader + 顶点着色器根据噪声图生成高低起伏的地形。
- 性能极客挑战
  - 生成 10万+ 块神庙砖块！
  - TA硬核要求：绝对不用 GameObject 生成砖块。全盘使用 Week 5 掌握的 GPU Instancing / DrawMeshInstanced 技术，将所有砖块状态存储在 Compute Buffer 中，确保 10万块建筑同屏 Draw Call 控制在个位数。
Week 11：光影、氛围与特效集成（Day 75-81）
- 环境氛围与后处理
  - 给神庙加上基于高度的大气雾（利用深度重建世界坐标的方式在后处理中实现）。
  - 接入天空盒随时间变化的动态光照（测试你的Shader是否正确接收了SH和阴影变幻）。
- 接入第一阶段粒子系统
  - 在神庙中散布动态火把/魔法法阵，复用你第一阶段写的带 Renderer Feature 的 GPU 粒子。保证特效与透贴/水体的渲染顺序正确（深度测试无穿帮）。
Week 12：终极性能压榨（Day 82-86）
- 内存与渲染分析（极其重要的面试资本）
  - 打开 Unity Profiler 和 Frame Debugger。
  - 优化项 1：砍掉无用的 Shader 变体，控制编译打包速度。
  - 优化项 2：为远处的建筑开启简单的 LOD 系统，降低顶点负担。
  - 优化项 3：验证 Overdraw，用 Scene 视图优化粒子系统造成的透明过度绘制。
Week 13：打磨、开源与投递准备（Day 87-90）
- Day 87-88：README与视频包装
  - 在 GitHub 放上极其干练的 README：明确挂出【性能数据指标】（如：GTX1060 下 10万建筑同屏稳定过 120fps）。
  - 录制解说视频：不仅演示神庙，更要演示编辑器工具面板的操作过程。
- Day 89-90：精准简历修改
  - 修改简历说辞（比如不要写“了解Shader”，写“熟练开发符合URP SRP Batcher规范的高级材质系统及Shader GUI工具链”）。

---
💡 TA高频面试模拟题（在这60天学习中时刻针对性思考）
为了让你直击Offer，在每一个学习模块中，问自己能不能流畅回答以下问题：
1. [底层 vs 引擎]：OpenGL里的 gl_Position 和 URP里的 TransformObjectToHClip 中间发生了什么矩阵运算？
2. [性能优化]：SRP Batcher 合并渲染的原理是什么？它和 GPU Instancing 以及 Dynamic Batching 到底有什么冲突和区别？
3. [管线深度]：在URP中，如果我想在后处理之前拿到一张带有半透明物体的深度图，用 Renderer Feature 该怎么挂载和实现？
4. [工作流]：如果美术反映你给的Shader在加了点光源后不生效（全黑），你怎么排查？（答案提示：宏定义支持、BasePass与AdditionalLightPass分离）
这样调整后的计划，完全脱离了“新手模仿”的阶段，直接拔高到**“为了项目能落地”、“为了性能不崩盘”、“为了美术能干活”**的三维工业标准，祝你在这个计划的指引下顺利拿下那份Offer！有问题随时聊。
[图片]
gpt-5.4
可以。
不过你这条消息里没有贴出“另外两个模型”的具体回复内容，所以我现在只能完整总结你已经贴出来的这一个方案，并基于它给你做一份更细、更适合你当前进度的第二阶段详细计划表。
如果你愿意，你可以把另外两个模型的建议也贴过来，我再帮你做一版：
- 三模型共同点总结
- 差异对比
- 取长补短后的最终版90天计划
- 按周/按天执行表
- 风险预案和面试导向优化
在你还没贴另外两个模型内容前，我先做两件事：
1. 把目前这个方案完整提炼总结
2. 结合你“第一阶段已经做到工具链”的现状，给你一份第二阶段详细执行表

---
一、先总结目前这个模型给你的核心建议
这个方案整体质量其实是不错的，主线非常清晰：
1. 总目标定位是对的
目标不是“成为纯图形程序员”，而是：
把底层图形基础，翻译成 Unity/URP 工程化能力，拿技术TA offer
这是这个方案最正确的地方。
因为你的背景已经说明了：
你已经有的强项
- OpenGL 图形管线基础扎实
- 做过 Compute Shader
- 数学不弱
- 有 C++ 引擎经验
- 有 cocos 系开发经验
你真正欠缺的
- Unity URP 工程化经验
- Shader 落地能力
- 美术可用的工具化思维
- 能代表你“技术TA价值”的项目作品
也就是说，你现在不是缺图形学理论，而是缺：
- Unity里怎么接管管线
- Shader怎么真正服务项目
- 怎么给美术做工具
- 怎么把技术点包装成作品和简历
所以这个方案的主线“底层原理 → Unity工程化 → 作品集”非常合理。

---
2. 它把90天分成了3个阶段
阶段一：工程化突围（1-30天）
核心目标：
把 GPU 粒子做成 可复用的生产力工具
重点在：
- Renderer Feature / Render Pass
- Editor 工具
- Gizmos
- Preset
- Setup Wizard
本质上在训练你：
- 不只是“把功能做出来”
- 而是“做成别人能用的系统”
你现在说你第一阶段工具已经做出来了，说明你已经跨过了一个非常关键的门槛。
这部分对技术TA非常重要，因为它体现的是：
- 管线接入能力
- Unity编辑器扩展能力
- 可交付意识
- 为美术服务的能力

---
3. 阶段二：Shader专项突破（31-60天）
原方案的目标是：
补齐 Shader 弱项，达到“能改官方 Lit Shader”的水平
原始任务分两块：
第一块：基础迁移
- OpenGL → HLSL 对照
- ShaderLab 结构
- 手写基础 Shader
- 阅读 URP Lit
- 改官方 Shader
第二块：风格化 Shader 实战
- 水体 Shader
- 平面反射 Renderer Feature
- 选做其他风格化效果
这一阶段的意义是：
- 把你原来的 OpenGL/GLSL 认知迁移到 Unity/URP
- 补上 Shader“语法 + 架构 + 工程化”的短板
- 做一个成型的可展示视觉效果

---
4. 阶段三：代表作《程序化神庙》（61-90天）
目标是：
整合前两阶段成果，做一个“技术+工程能力”兼具的完整项目
项目里会整合：
- 程序化建筑生成
- GPU Instancing
- 地形
- 粒子
- Shader
- 后处理
- 氛围
- 交互
这个阶段不是纯炫技，而是为了形成：
- 一个能玩的 Demo
- 一个能讲清楚技术决策的完整项目
- 一个能挂在简历和作品集上的代表作
这非常符合技术TA岗位的面试逻辑。

---
二、这个模型方案的优点和不足
优点
优点1：方向非常对
它没有让你继续深挖 OpenGL 或图形学公式，而是把重心放到：
- URP
- Shader
- 工具链
- Demo
- 作品集
这对找技术TA工作更现实。
优点2：有工程化思维
不是只让你“做效果”，而是要求：
- 可配置
- 可视化
- 多相机
- SceneView支持
- Preset
- README
- 视频演示
这些都很像真实工作内容。
优点3：强调作品产出
每个阶段都有：
- GitHub代码
- 博客
- 视频
- 对比图
- Demo
这一点非常重要，因为技术TA求职不看“你懂多少”，更看“你展示出来多少”。

---
不足
这个方案也有几个明显不足，尤其是对第二阶段来说。
不足1：Shader 计划偏“会做”，不够“会排查”
它教你做 Shader，但对下面这些强调不够：
- Frame Debugger
- RenderDoc
- 深度图问题定位
- 透明排序问题
- 变体和编译问题
- 平台兼容问题
而这些恰恰是技术TA很需要的能力。
不足2：性能意识不够独立
原方案里性能优化更多放在第三阶段的项目收尾，
但实际上第二阶段就应该开始建立：
- overdraw 意识
- keyword/variant 意识
- half/float 精度意识
- SRP Batcher / Instancing 认知
不足3：第二阶段案例略单薄
只有一个“风格化水体”会显得像单点教程产出。
如果你能补一个：
- 护盾/能量场
或
- Soft Particle / Distortion
那会更像你真的掌握了方法。
不足4：缺少“材质工作流设计”
技术TA不只是写 Shader，还要会：
- 怎么给参数分组
- 哪些参数该暴露
- 哪些参数该锁住
- 怎么做 preset
- 怎么做 debug view
这部分应该补上。

---
三、如果把目前这一个模型压缩成一句话
可以概括成：
先用 GPU 粒子系统证明你的 Unity 工程化能力，再用 URP Shader 和风格化水体证明你的渲染落地能力，最后用“程序化神庙”整合成一个完整的技术TA代表作。
这是它的核心思想。

---
四、基于这个方案，给你一个“综合优化版”的第二阶段目标
既然你第一阶段已经做出来了，那第二阶段不要只定成：
会写 Shader
而应该升级成：
会在 URP 中开发、调试、优化、封装 Shader，并做出一个可交付的风格化效果系统。
也就是说，第二阶段要覆盖 5 件事：
1. 写 Shader
2. 读懂 URP 官方 Shader
3. 定位和调试问题
4. 考虑性能和变体
5. 把效果做成可给他人使用的材质/工具

---
五、给你一份详细的第二阶段计划表
下面这份计划默认你已经完成第一阶段，而且你每天能投入：
- 工作日：2~3 小时
- 周末：4~6 小时
总时长约 30 天。
我会按 周目标 + 每日任务 + 每周产出 + 验收标准 来写。

---
第二阶段详细计划表（第31-60天）

---
Week 1：HLSL/ShaderLab/URP基础迁移
目标
把你原本的 GLSL/OpenGL 经验，迁移到 Unity 的 ShaderLab + HLSL + URP 语境里。

---
Day 1：语法迁移总览
任务：
- 阅读 HLSL for GLSL Developers
- 列一个自己的对照表：
  - vec2/3/4 ↔ float2/3/4
  - mix ↔ lerp
  - fract ↔ frac
  - mod ↔ fmod
  - mat3/mat4 ↔ float3x3/float4x4
- 理解 Unity 中：
  - vertex / fragment 写法
  - Attributes / Varyings
  - TEXTURE2D / SAMPLER
产出：
- 一页《GLSL → HLSL 对照笔记》

---
Day 2：ShaderLab 结构
任务：
- 学习 ShaderLab 的：
  - Shader
  - Properties
  - SubShader
  - Pass
  - Tags
  - HLSLPROGRAM
- 理解：
  - RenderType
  - Queue
  - ZWrite
  - Blend
  - Cull
练习：
- 写一个最简 Unlit 纯色 Shader
产出：
- Unlit_Color.shader

---
Day 3：纹理采样与UV
任务：
- 纹理属性定义
- UV 传递
- 贴图采样
- Tiling/Offset
- 主纹理颜色乘法
练习：
- 写一个贴图 Unlit Shader
- 支持 _BaseMap 和 _BaseColor
产出：
- Unlit_Texture.shader

---
Day 4：空间变换
任务：
- 梳理 Object / World / View / Clip / NDC / Screen
- 学习 URP 常用变换函数：
  - TransformObjectToWorld
  - TransformObjectToHClip
  - TransformObjectToWorldNormal
- 写出世界空间法线可视化 Shader
产出：
- Debug_WorldNormal.shader

---
Day 5：基础光照
任务：
- 理解主光源方向获取
- 学习 Lambert / Blinn-Phong
- 在 URP 中获取主光数据
练习：
- 写一个基础光照 Shader
- 支持漫反射 + 高光
产出：
- Lit_BlinnPhong.shader

---
Day 6：Normal Map
任务：
- 理解切线空间法线
- 学习 normal map 解码
- 搭建 TBN 或使用 URP 工具函数
练习：
- 给前一天 Shader 加 Normal Map
产出：
- Lit_NormalMap.shader

---
Day 7：周复盘
任务：
- 整理一份本周笔记
- 录一个 1~2 分钟小视频，展示：
  - 纯色
  - 纹理
  - 法线
  - 光照
- 写周报博客草稿
本周验收标准：
- 你能不用抄教程，手写一个最基础的带纹理光照 Shader
- 能说清楚 ShaderLab 的结构
- 能解释几个基础空间的区别

---
Week 2：读懂 URP Lit，开始“改官方Shader”
目标
从“会写简单 Shader”升级到“能读官方实现、知道去哪里改”。

---
Day 8：阅读 Lit.shader 入口结构
任务：
- 打开 URP 包中的 Lit.shader
- 先不深究细节，只做结构拆解：
  - Properties
  - Pass 列表
  - Forward pass
  - ShadowCaster
  - DepthOnly
  - Meta
- 画一张结构图
产出：
- 《URP Lit 结构图》

---
Day 9：读 LitForwardPass.hlsl
任务：
- 找到 Forward Pass 主入口
- 梳理：
  - Attributes
  - Varyings
  - SurfaceData
  - InputData
  - Fragment 主流程
产出：
- 《URP Lit Forward Pass 笔记》

---
Day 10：理解 SurfaceData / InputData
任务：
- 重点理解这些数据分别装了什么：
  - albedo
  - normalTS
  - metallic
  - smoothness
  - emission
  - alpha
  - worldPos
  - viewDir
  - shadowCoord
练习：
- 在笔记里写清：
  - “材质数据”与“几何/光照输入”的区别

---
Day 11：理解 UniversalFragmentPBR
任务：
- 阅读 UniversalFragmentPBR() 附近逻辑
- 先不追最底层 BRDF 推导，只要搞懂：
  - 输入是什么
  - 输出是什么
  - 主光/附加光/环境光如何参与
产出：
- 一页《URP PBR 主流程理解》

---
Day 12：关键词与变体
任务：
- 看官方 Shader 里的 #pragma
- 理解：
  - multi_compile
  - shader_feature
  - _NORMALMAP
  - _MAIN_LIGHT_SHADOWS
  - _ADDITIONAL_LIGHTS
产出：
- 《URP Shader Keywords 基础笔记》

---
Day 13：第一次改官方 Shader
任务：
- 给 Lit 加一个简单自定义功能：
  - 推荐：Fresnel 边缘发光
  - 或 Rim Light
- 尽量不乱改结构，保持原有功能可用
产出：
- CustomLit_Rim.shader

---
Day 14：周复盘与总结
任务：
- 录一个对比视频：
  - 原版 Lit
  - 你改造后的 Lit
- 写博客草稿：
  - 《如何阅读 URP Lit Shader》
本周验收标准：
- 你能说出 Lit 的 Forward Pass 在做什么
- 你知道自定义功能应该插在哪
- 你能改出一个小功能而不把整个 Shader 改坏

---
Week 3：深度图、屏幕采样、调试能力
目标
补齐技术TA很关键的一块：会排查，会验证，会解释“为什么这样做”。

---
Day 15：Depth Texture 原理与采样
任务：
- 学习 URP 深度纹理的生成条件
- 实现一个深度可视化 Shader
- 理解线性深度转换
产出：
- Debug_Depth.shader

---
Day 16：用深度做交界效果
任务：
- 做一个简单“交界高亮/边缘泡沫” Demo
- 比如物体接触地面时出现边缘亮带
产出：
- 一个小场景 Demo

---
Day 17：Opaque Texture 与屏幕采样
任务：
- 开启 Opaque Texture
- 做一个简单屏幕扭曲效果
- 学习屏幕UV与采样方法
产出：
- Screen_Distortion.shader

---
Day 18：Soft Particle
任务：
- 把你第一阶段的粒子系统和深度图结合
- 做粒子与场景几何体交界时的软融合
产出：
- GPU 粒子 Soft Particle 版本

---
Day 19：Frame Debugger 实战
任务：
- 用 Frame Debugger 看：
  - 粒子 draw call
  - 水体 draw call
  - distortion draw call
- 记录每个效果处于什么渲染阶段
产出：
- 截图笔记一份

---
Day 20：RenderDoc 抓帧
任务：
- 抓一帧
- 查看：
  - 深度纹理
  - 场景颜色 RT
  - 某个 Shader 的输入纹理
  - 某个 pass 的输出结果
产出：
- 《RenderDoc 抓帧记录》

---
Day 21：周复盘
任务：
- 写博客：
  - 《URP Shader 调试入门：Depth、OpaqueTexture、Frame Debugger、RenderDoc》
- 整理调试 checklist
本周验收标准：
- 你知道深度图/OpaqueTexture 什么时候可用
- 你能解释 Soft Particle 为什么有效
- 你能用调试工具验证效果链路

---
Week 4：主案例——风格化水体 Shader
目标
做出一个像样的、参数可调、能拿来展示的完整效果。

---
Day 22：水体底色与深浅混合
任务：
- 基于深度做浅水/深水颜色过渡
- 支持基础颜色参数
产出：
- 水体基础版

---
Day 23：法线扰动与流动
任务：
- 双法线流动
- 控制波浪速度、强度、方向
产出：
- 法线扰动版水体

---
Day 24：泡沫
任务：
- 基于深度差做边缘泡沫
- 加噪声增强自然感
产出：
- 泡沫版水体

---
Day 25：顶点波浪
任务：
- 在顶点阶段做波浪起伏
- 控制频率、振幅、速度
产出：
- 顶点动画版水体

---
Day 26：Fresnel 与高光细节
任务：
- 加边缘亮
- 加简单镜面反射感
- 控制风格化强度
产出：
- 水体外观增强版

---
Day 27：参数整理与 Debug 模式
任务：
- 参数分组：
  - Color
  - Wave
  - Foam
  - Reflection
  - Debug
- 增加调试开关：
  - 显示深度差
  - 显示泡沫mask
  - 显示法线扰动
产出：
- 工程化版本水体材质

---
Day 28：周复盘
任务：
- 录视频展示参数调节过程
- 截图做前后对比图
- 写博客草稿
本周验收标准：
- 水体已经不是“技术实验”，而是“可展示效果”
- 参数能调，逻辑清晰
- 你能讲清楚每个模块在干什么

---
Week 5：平面反射 Renderer Feature + 副案例
目标
把水体补完整，同时再做一个第二案例，证明你不是只能做水。

---
Day 29：平面反射基础
任务：
- 梳理平面反射思路：
  - 反射相机
  - 反射矩阵
  - 裁剪平面
  - 渲染到 RT
产出：
- 设计笔记

---
Day 30：实现 Planar Reflection Renderer Feature
任务：
- 创建 PlanarReflectionRendererFeature
- 创建 pass
- 输出反射纹理
产出：
- 平面反射基础代码

---
Day 31：水体接入反射纹理
任务：
- 在水体中采样反射 RT
- 加入反射强度、扰动
产出：
- 带反射的完整水体

---
Day 32：优化反射
任务：
- 只在需要时渲染
- 控制分辨率
- 考虑层过滤
产出：
- 反射优化版

---
Day 33：副案例选题与基础搭建
建议你选：
- 护盾/能量场
任务：
- 做基础 Fresnel
- 半透明
- 流动噪声
产出：
- Shield_Base.shader

---
Day 34：副案例增强
任务：
- 增加 distortion
- 增加 dissolve 或 hit pulse
- 增加参数控制
产出：
- 完整护盾效果

---
Day 35：周复盘
任务：
- 录 2 段视频：
  - 水体
  - 护盾
- 写博客草稿：
  - 《URP 风格化水体与平面反射实践》
本周验收标准：
- 水体效果完整
- 平面反射真正跑起来
- 至少有第二个独立可展示 Shader 案例

---
Week 6：性能、变体、整理发布
目标
把第二阶段从“做完了”变成“像作品”。

---
Day 36：Shader 变体治理
任务：
- 复盘你项目中的 keywords
- 区分：
  - 哪些适合 shader_feature
  - 哪些适合材质参数
- 控制变体数量
产出：
- 变体清单

---
Day 37：精度与成本检查
任务：
- 检查 Shader 中哪些可用 half
- 检查纹理采样次数
- 检查透明 overdraw 风险
产出：
- 性能检查笔记

---
Day 38：SRP Batcher / Instancing 基础认知
任务：
- 学习 SRP Batcher 条件
- 检查材质/Shader 是否友好
- 理解 Instancing 的基本支持方式
产出：
- 一页总结

---
Day 39：项目整理
任务：
- 清理命名
- 整理文件夹结构
- 补 README
- 补示例场景
建议目录：
- Shaders
- Materials
- Textures
- RendererFeatures
- DemoScenes
- Docs

---
Day 40：文档与演示
任务：
- README
- GIF / 截图
- 参数说明
- 使用方式
- 已知限制

---
Day 41：博客正式发布
建议至少发 2 篇：
1. 《从 OpenGL 到 URP：Shader 迁移笔记》
2. 《URP 风格化水体、反射与调试实践》

---
Day 42：阶段总结
任务：
- 回顾第二阶段全部成果
- 列出第三阶段可复用内容：
  - 水体
  - 反射
  - 护盾/能量场
  - Soft Particle
  - 调试和性能经验
本周验收标准：
- 第二阶段代码可展示、可阅读、可运行
- 有博客、有视频、有 README
- 你能像面试答题一样讲清楚技术决策

---
六、第二阶段最终应有的产出清单
到第60天结束，你最好有这些：
代码
- 一组基础 Shader
  - Unlit
  - Texture
  - BlinnPhong
  - NormalMap
  - 简化PBR / Rim
- 一个改造过的 URP Lit Shader
- 一个完整水体 Shader
- 一个 PlanarReflection Renderer Feature
- 一个副案例 Shader（护盾/能量场/Toon 任选）
文档
- GLSL → HLSL 迁移笔记
- URP Lit 架构笔记
- Shader 调试笔记
- 性能与变体笔记
- README
展示
- 水体展示视频
- 副案例展示视频
- 对比截图
- 1~2 篇技术博客

---
七、第二阶段面试话术你也要提前准备
你做完后，至少要能讲清这几个问题：
1. 为什么要读 URP Lit 源码？
答题方向：
- 为了理解官方材质流程
- 便于在不重写整套光照的前提下做功能扩展
- 降低维护成本
2. 为什么水体需要深度图？
答题方向：
- 做深浅水颜色变化
- 做边缘泡沫
- 做交界融合
- 增强空间感
3. 为什么要做平面反射，而不是只做简单 cubemap？
答题方向：
- 平面反射对近景和接触场景物体更真实
- 但成本更高，所以需要按需渲染和降分辨率优化
4. 你怎么排查 Shader 效果不对？
答题方向：
- 先确认资源是否生成
- 再确认 Render Pass 顺序
- 再看 Frame Debugger
- 必要时用 RenderDoc 查看中间 RT 和采样输入
5. 你如何控制 Shader 性能？
答题方向：
- 控制纹理采样次数
- 谨慎使用透明和屏幕采样
- 管理 keyword 和变体数量
- 合理使用 half 精度
- 做可降级版本

---
八、如果你后面把另外两个模型贴出来，我可以怎么帮你
你把另外两个模型的内容贴出来后，我可以继续帮你做这三件事：
1. 三模型横向对比
我会帮你拆成：
- 共同建议
- 互相冲突的地方
- 哪个建议更适合技术TA求职
- 哪个建议更适合你当前背景
2. 输出一版“融合后的最优方案”
比如会整理成：
- 必做
- 可选
- 删掉的低性价比项
- 风险预案
- 时间不足时的压缩版
3. 给你最终版详细计划表
可以细到：
- 每天做什么
- 每天看什么资料
- 每天提交什么 Git
- 每周博客写什么
- 每周验收什么
- 哪些东西能直接写进简历

---
九、我先给你的直接建议
如果你现在就要继续推进，不等另外两个模型内容，
那你第二阶段最优先补这 5 件事：
1. 读 URP Lit 并改一个功能
2. 学会 Depth Texture / Opaque Texture
3. 做 Soft Particle
4. 做一个完整水体 + 平面反射
5. 补一个副案例：护盾/能量场
再加上：
6. 用 Frame Debugger 和 RenderDoc 做一次调试记录
7. 整理 Shader 性能和 variant 认知
这套组合最像技术TA。

---
如果你愿意，我下一条可以直接继续帮你输出一份：
《第二阶段每日执行清单（Day 1 ~ Day 30）》
我会写成真正可以打卡的格式，比如：
- 今天看什么
- 今天做什么
- 今天提交什么
- 今天记录什么
- 今天完成的判断标准
如果你要，我就直接继续给你“可复制到 Notion/飞书的打卡版计划表”。
