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
[图片]
就是这个对话有3个模型，可以把这三个模型全部总结下，再给我出一份详细的计划表吗？
[图片]
claude-sonnet-4.6
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
