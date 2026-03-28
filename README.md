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
