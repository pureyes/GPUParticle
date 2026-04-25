# GPU Particle System for Unity

基于 Compute Shader 的高性能 GPU 粒子系统，支持 10 万粒子实时渲染和鼠标交互。

![GPU Particle System](Docs/preview.png)

## 功能特点

- 🚀 **10万粒子实时渲染** - GPU 并行计算
- 🖱️ **鼠标交互** - 点击拖动排斥粒子
- 🎮 **相机控制** - 右键旋转，滚轮缩放
- ⚡ **高性能** - DrawProceduralIndirect 渲染
- 🖥️ **桌面平台** - 支持 Windows / macOS / Linux

## 技术栈

- Unity 2021.2+ 
- Compute Shader - 粒子位置更新
- HLSL Shader - GPU 渲染
- C# - 系统管理

## 快速开始

### 在 Unity 中运行

1. 克隆或下载项目
2. 使用 Unity 2021.2 或更高版本打开
3. 选择 `Tools > GPU Particle System > Setup Scene`
4. 点击 Play 运行

### 构建设置

1. 打开 `File > Build Settings`
2. 选择目标平台（Windows / macOS / Linux）
3. 点击 Build

## 操作说明

| 操作 | 功能 |
|------|------|
| 左键 + 拖动 | 排斥粒子 |
| 右键 + 拖动 | 旋转相机 |
| 滚轮 | 缩放 |

## 项目结构

```
Assets/
├── Scripts/GPUParticle/    # C# 脚本
├── Shaders/                # Compute + Render Shader
├── Editor/                 # 编辑器工具
└── Scenes/                 # 场景文件
```

## 性能

在典型设备上的性能表现：

| 设备 | 粒子数 | FPS |
|------|--------|-----|
| 桌面 RTX 3060 | 100,000 | 60+ |
| 笔记本 GTX 1650 | 100,000 | 45-60 |
| MacBook M1 | 50,000 | 55-60 |

## 自定义

### 调整粒子数量
```csharp
// GPUParticleSystem.cs
[SerializeField] private int particleCount = 100000;
```

### 修改交互强度
```csharp
[SerializeField] private float interactionStrength = 30f;
[SerializeField] private float interactionRadius = 5f;
```

## 系统要求

- **操作系统**: Windows 10+, macOS 10.15+, or Linux
- **显卡**: 支持 DirectX 11 / OpenGL 4.5 / Vulkan 的独立显卡
- **Unity**: 2021.2 或更高版本

## 许可证

MIT License - 可自由使用于个人和商业项目


# 技术TA（偏技术）90天学习计划

> 适用对象：有OpenGL底层基础，做过Compute Shader，Unity经验<1年，Shader偏弱
> 目标：获得技术TA岗位offer
> 核心策略：把底层原理翻译成Unity工程化能力

---

## 📋 个人背景与短板分析

### 已有优势（不需要再花时间）
- [x] OpenGL图形管线（TinyRenderer、Games101、LearnOpenGL）
- [x] Compute Shader基础（GPU粒子系统Compute部分已懂）
- [x] 数学基础（线性代数、变换、投影）
- [x] C++引擎层经验（cocos2dx多年）
- [x] Cocos Creator经验（2年）

### 当前短板（重点突破）
- [ ] **Unity URP工程化**：Renderer Feature、Render Pass、工具链封装
- [ ] **Shader能力**：HLSL语法、URP Shader Library、能改官方Shader
- [ ] **工具链思维**：Editor工具、Gizmos、美术工作流支持
- [ ] **代表作**：能展示技术+工程能力的完整项目

---

## 🎯 三阶段学习计划

### 阶段一：工程化突围（第1-30天）
**目标**：把GPU粒子系统做成可复用的生产力工具

#### Week 1-2: URP Renderer Feature集成
**任务清单**：
- [ ] **Day 1-2**: 创建Renderer Feature基础框架
  - [ ] 创建`GPUParticleRendererFeature.cs`
  - [ ] 创建`GPUParticleRenderPass.cs`
  - [ ] 支持可配置的Render Pass Event（渲染时机）
  - [ ] 替换原有的`OnRenderObject()`渲染方式
  
- [ ] **Day 3-4**: 测试不同渲染时机的效果
  - [ ] 在场景中添加不透明物体（Cube）
  - [ ] 在场景中添加透明物体（Plane）
  - [ ] 测试`AfterRenderingOpaques`（粒子被透明物体遮挡）
  - [ ] 测试`AfterRenderingTransparents`（粒子在最前面）
  - [ ] 测试`BeforeRenderingPostProcessing`（受Bloom影响）
  - [ ] 理解渲染顺序对视觉效果的影响
  
- [ ] **Day 5-7**: 完善Renderer Feature功能
  - [ ] 支持Scene View预览
  - [ ] 支持多相机正确渲染
  - [ ] 支持相机剔除（Frustum Culling）
  - [ ] 添加调试信息输出

**产出物**：
- Renderer Feature代码（GitHub提交）
- 一张对比图：不同Render Pass Event的渲染效果差异
- 技术博客：《URP Renderer Feature实战：把Compute Shader粒子集成到管线》

**检查标准**：
- [ ] //粒子在不改变原有MonoBehaviour的情况下，通过Renderer Feature渲染
- [ ] 可以在Inspector中切换渲染时机并实时看到效果
- [ ] Scene View和Game View都正确显示

---

#### Week 3-4: Editor工具链开发
**任务清单**：
- [ ] **Day 8-10**: 自定义Inspector面板
  - [ ] 创建`GPUParticleSystemEditor.cs`
  - [ ] 基础参数：粒子数量、生命周期、发射半径、速度
  - [ ] 物理参数：重力、阻尼、风力
  - [ ] 交互参数：鼠标交互半径、力度
  - [ ] 实时预览按钮（重置粒子、暂停/继续）
  
- [ ] **Day 11-12**: Gizmos可视化
  - [ ] 发射器范围可视化（Wire Sphere）
  - [ ] 鼠标交互范围可视化（当鼠标按下时）
  - [ ] 粒子数量统计显示
  - [ ] 选中时显示粒子分布预览
  
- [ ] **Day 13-14**: Preset系统
  - [ ] 支持保存当前配置为Preset文件
  - [ ] 支持加载Preset
  - [ ] 内置几个默认Preset（爆炸、下雪、魔法等）
  
- [ ] **Day 15**: 设置向导工具
  - [ ] 创建`GPUParticleSetupWizard.cs`
  - [ ] 一键查找URP Renderer Data
  - [ ] 一键添加Renderer Feature
  - [ ] 快速创建材质按钮

**产出物**：
- 完整的Editor工具代码
- 使用文档（README）
- 视频演示：如何在5分钟内配置好粒子系统

**检查标准**：
- [ ] 美术同学可以在不看代码的情况下调整粒子效果
- [ ] 可以保存和分享配置
- [ ] 有可视化辅助理解发射器范围

---

### 阶段二：Shader专项突破（第31-60天）
**目标**：补齐Shader弱项，达到能改官方Lit Shader的水平

#### Week 5-6: URP Shader基础迁移
**任务清单**：
- [ ] **Day 16-18**: OpenGL→HLSL迁移对照
  - [ ] 读`HLSL for GLSL Developers`文档
  - [ ] 对比 vertex shader 语法差异
  - [ ] 对比 fragment/pixel shader 语法差异
  - [ ] 理解Unity的ShaderLab结构
  
- [ ] **Day 19-21**: 手写基础Shader
  - [ ] 最简Unlit Shader（纯色）
  - [ ] 添加纹理采样
  - [ ] 添加基础光照（Blinn-Phong）
  - [ ] 添加Normal Map
  - [ ] 添加Roughness/Metallic（简易PBR）
  
- [ ] **Day 22-23**: 读懂URP官方Shader
  - [ ] 精读`Lit.shader`
  - [ ] 精读`LitForwardPass.hlsl`
  - [ ] 理解`UniversalFragmentPBR()`函数
  - [ ] 理解Shader Keywords系统
  
- [ ] **Day 24-25**: 修改官方Shader
  - [ ] 给URP Lit添加自定义功能（如边缘发光）
  - [ ] 理解多Pass Shader的写法
  - [ ] 理解Shader Variant和编译优化

**产出物**：
- 一套手写的递进式Shader（Unlit→Lit→PBR）
- 一篇博客：《从OpenGL到HLSL：程序员的URP Shader迁移指南》
- 能解释清楚URP Shader的架构设计

**检查标准**：
- [ ] 能不看文档写出一个带光照的Shader
- [ ] 能定位并修改URP官方Shader的特定功能
- [ ] 理解Shader性能优化的基本概念

---

#### Week 7-8: 风格化Shader实战
**任务清单**：
- [ ] **Day 26-28**: 风格化水体Shader
  - [ ] 深度图采样实现深浅颜色混合
  - [ ] 法线扰动实现波浪效果
  - [ ] 边缘泡沫效果（基于深度差）
  - [ ] 简单反射（用反射探针或平面反射）
  - [ ] 顶点动画实现波浪起伏
  
- [ ] **Day 29-30**: 用Renderer Feature做平面反射
  - [ ] 创建`PlanarReflectionRendererFeature`
  - [ ] 实现反射相机的设置
  - [ ] 渲染反射纹理
  - [ ] 在水体Shader中采样反射纹理
  - [ ] 优化：只在需要时渲染反射
  
- [ ] **Day 31-32**: 其他风格化效果（选做1-2个）
  - [ ] 卡通渲染（Toon Shading）+ 描边
  - [ ] 全息投影效果（Hologram）
  - [ ] 护盾/能量场效果
  - [ ] 溶解（Dissolve）效果

**产出物**：
- 一个完整的风格化水体Shader（GitHub）
- 配套的技术博客（重点写深度图和反射的实现）
- 视频演示：水体效果的参数调节

**检查标准**：
- [ ] 水体效果参数可调，实时预览
- [ ] 反射正确显示场景物体
- [ ] 性能可控（在目标平台跑满60fps）

---

### 阶段三：代表作《程序化神庙》（第61-90天）
**目标**：整合前两阶段成果，做出能展示技术+工程能力的完整项目

#### Week 9-10: 项目架构设计
**任务清单**：
- [ ] **Day 33-34**: 确定项目范围和技术栈
  - [ ] 核心机制：每次运行生成不同风格的神庙
  - [ ] 技术栈确认：
    - 地形：Heightmap + 顶点着色器位移
    - 建筑：Shape Grammar（C#）+ GPU Instancing
    - 氛围：自定义URP Post-processing
    - 粒子：复用阶段一的粒子系统
  - [ ] 明确不做：Tessellation（性价比低）、复杂物理
  
- [ ] **Day 35-36**: 搭建项目基础
  - [ ] GitHub仓库搭建
  - [ ] URP管线配置
  - [ ] 基础场景搭建（相机、光照、后处理）
  - [ ] 第一人称控制器（用Unity内置或简单实现）
  
- [ ] **Day 37-38**: Shape Grammar基础
  - [ ] 实现基础的建筑生成规则
  - [ ] 支持柱子、墙壁、屋顶的模块化生成
  - [ ] 实现随机参数控制建筑风格（希腊/玛雅）
  - [ ] GPU Instancing渲染大量建筑部件

**产出物**：
- 可运行的基础场景
- 能生成简单建筑结构
- GitHub仓库初始化

---

#### Week 11-12: 核心功能实现
**任务清单**：
- [ ] **Day 39-41**: 程序化地形
  - [ ] 用噪声生成Heightmap
  - [ ] 顶点着色器实现地形细节
  - [ ] 地形材质：根据坡度混合草地/石头/雪地
  - [ ] 神庙位置自动选择（平坦区域）
  
- [ ] **Day 42-44**: 完善建筑生成
  - [ ] 神庙主体结构生成（基座、柱子、屋顶）
  - [ ] 支持两种风格参数：古希腊（多立克/爱奥尼）vs 玛雅（金字塔）
  - [ ] 添加细节：台阶、装饰、破损效果
  - [ ] 生成碰撞体（用于玩家行走）
  
- [ ] **Day 45-46**: 氛围和光照
  - [ ] 动态时间系统（昼夜循环）
  - [ ] 体积光效果（使用阶段二写的Shader）
  - [ ] 雾效和大气散射
  - [ ] 火把/光源的粒子效果（复用阶段一的粒子系统）
  
- [ ] **Day 47-49**: 交互和重建功能
  - [ ] 按空格键触发重建
  - [ ] 重建时的过渡动画（旧建筑消散+新建筑生成）
  - [ ] 简单的行走和跳跃（Character Controller）
  - [ ] 相机后处理（Bloom、Tone Mapping）

**产出物**：
- 完整的程序化神庙生成系统
- 可交互的Demo（探索+重建）
- 基础音效和BGM

---

#### Week 13:  polish和发布
**任务清单**：
- [ ] **Day 50-52**: 性能优化
  - [ ] GPU Instancing检查
  - [ ] LOD系统（如果建筑部件多）
  - [ ] 内存和Draw Call分析
  - [ ] 目标：中端显卡60fps
  
- [ ] **Day 53-55**: 美术 polish
  - [ ] 调整光照和氛围
  - [ ] 添加粒子效果（火把、灰尘、光晕）
  - [ ] UI设计（开始菜单、重建提示）
  - [ ] 截图和视频录制
  
- [ ] **Day 56-58**: 文档和宣传
  - [ ] README详细说明（技术栈、运行方法、截图）
  - [ ] 技术博客：《程序化神庙：从Shape Grammar到URP渲染》
  - [ ] B站/YouTube视频（5-8分钟，展示生成过程和可调节参数）
  - [ ] 可下载的Windows/Mac构建版本
  
- [ ] **Day 59-60**: 简历整合
  - [ ] 整理作品集页面（可以用itch.io或个人网站）
  - [ ] 更新简历，突出技术TA相关技能
  - [ ] 准备面试话术（能讲清楚每个技术决策）

**产出物**：
- 可玩的完整Demo（GitHub Release）
- 技术展示视频
- 详细的技术博客
- 更新的简历

---

## 📚 学习资源清单

### URP Renderer Feature/Render Pass
- [Catlike Coding - Custom SRP](https://catlikecoding.com/unity/tutorials/custom-srp/)（看URP部分）
- Unity官方文档：[ScriptableRendererFeature](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@12.1/manual/renderer-features/how-to-custom-effect-render-objects.html)
- 参考项目：UniversalRenderingExamples（你已下载）

### Shader（OpenGL→HLSL迁移）
- [HLSL for GLSL Developers](https://microsoft.github.io/DirectX-Specs/d3d/HLSL_for_GLSL_Developers.html)（快速对照表）
- Unity URP源码：`Packages/com.unity.render-pipelines.universal/Shaders/Lit.shader`
- 书籍：《Unity Shader入门精要》（冯乐乐）

### 程序化生成
- 论文/文章：Shape Grammar在建筑生成中的应用
- 参考项目：Manifold Garden（William Chyr的博客）
- 算法：Perlin Noise、Simplex Noise、L-system基础

### 技术TA参考
- [William Chyr](https://www.williamchyr.com/blog)（Manifold Garden的TA，偏管线）
- [Graphics Programming Weekly](https://www.jendryschik.de/books/graphics-programming-weekly/)

---

## ✅ 里程碑检查点

### 检查点1：第30天（阶段一结束）
- [ ] GPU粒子系统有完整的Renderer Feature
- [ ] 有可视化Inspector和Gizmos
- [ ] 写了一篇技术博客
- [ ] GitHub仓库有完整的README

### 检查点2：第60天（阶段二结束）
- [ ] 能手写URP Shader
- [ ] 完成了风格化水体（带反射）
- [ ] 能解释URP官方Shader的架构
- [ ] 写了Shader迁移相关的技术博客

### 检查点3：第90天（阶段三结束）
- [ ] 《程序化神庙》Demo可玩
- [ ] 有技术展示视频
- [ ] 简历更新完毕
- [ ] 开始投递简历或接到面试

---

## 📝 每周复盘模板

每周结束时，记录以下内容：

```markdown
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
```

---

## 🎯 成功标准

90天后，你应该能：
1. **技术能力**：
   - 能独立开发URP Renderer Feature
   - 能手写中等复杂度的HLSL Shader
   - 能为美术创建可用的Editor工具

2. **作品集**：
   - 2个GitHub仓库（GPU粒子系统 + 程序化神庙）
   - 3篇以上技术博客
   - 1个可下载游玩的Demo
   - 1个技术展示视频

3. **面试准备**：
   - 能清楚讲解GPU粒子系统的架构
   - 能解释URP渲染管线
   - 能讨论性能优化思路
   - 能展示解决技术问题的过程

---

**最后提醒**：
- 这个计划的前提是**每天能投入2-3小时**，周末更多
- 如果遇到技术难点卡住，不要死磕超过3天，及时求助或调整计划
- 博客要边做边写，不要等做完再写
- GitHub提交要频繁，展示开发过程

