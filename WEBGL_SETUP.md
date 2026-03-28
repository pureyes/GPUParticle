# GPU Particle System - WebGL 设置指南

本项目是一个基于 Compute Shader 的 GPU 粒子系统，支持 10 万粒子实时渲染和鼠标交互。

## 功能特性

- ✅ **10万粒子实时渲染** - 使用 Compute Shader 进行 GPU 并行计算
- ✅ **鼠标交互** - 点击并拖动鼠标来排斥粒子
- ✅ **相机控制** - 右键旋转视角，滚轮缩放
- ✅ **WebGL 2.0 支持** - 现代浏览器中流畅运行
- ✅ **高性能** - 使用 DrawProceduralIndirect 渲染

## 快速开始

### 方法一：使用编辑器工具（推荐）

1. 打开 Unity 编辑器
2. 选择菜单 `Tools > GPU Particle System > Setup Scene`
3. 在弹出的窗口中设置粒子数量（默认 100000）
4. 点击 `Setup Scene` 按钮
5. 场景将自动配置：相机、粒子系统、UI 和光照

### 方法二：手动配置

1. 在场景中创建一个空物体，命名为 `GPUParticleSystem`
2. 添加 `GPUParticleSystem` 脚本
3. 分配以下引用：
   - **Particle Compute**: `Assets/Shaders/ParticleCompute.compute`
   - **Particle Material**: 创建新材质，使用 `Custom/ParticleRender` Shader
   - **Particle Texture**: （可选）软圆形纹理
4. 在主相机上添加 `CameraController` 脚本
5. 创建 UI Canvas，添加 FPS 显示（可选）

## WebGL 构建设置

### 1. 切换平台
```
File > Build Settings > WebGL
点击 "Switch Platform"
```

### 2. Player Settings 配置
```
Edit > Project Settings > Player > WebGL
```

**Resolution and Presentation:**
- Template: Default
- Width: 960
- Height: 600

**Other Settings:**
- Color Space: Gamma
- Auto Graphics API: ✅ (WebGL 2.0)
- Static Batching: ✅
- Dynamic Batching: ✅
- GPU Skinning: ✅

**Publishing Settings:**
- Compression Format: Gzip
- Name Files As Hashes: ✅
- Data Caching: ✅

**WebGL 2.0 要求:**
- Memory Size: 512 MB
- Exception Support: Explicitly Thrown Exceptions Only

### 3. 质量设置
```
Edit > Project Settings > Quality
```
- 选择 "Good" 或 "Simple" 质量级别
- 关闭抗锯齿（MSAA）以提高性能

### 4. 构建设置
```
File > Build Settings
```
- Development Build: ❌（发布时）
- Compression Format: Gzip

## 构建步骤

### 使用编辑器菜单
```
Tools > GPU Particle System > Build WebGL
```

### 或使用命令行
```bash
# Windows
"C:\Program Files\Unity\Hub\Editor\<版本>\Editor\Unity.exe" -quit -batchmode -projectPath . -executeMethod WebGLBuilder.BuildWebGL

# Mac
/Applications/Unity/Hub/Editor/<版本>/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath . -executeMethod WebGLBuilder.BuildWebGL
```

## WebGL 2.0 浏览器兼容性

| 浏览器 | 版本要求 | 支持状态 |
|--------|---------|---------|
| Chrome | 56+ | ✅ 完全支持 |
| Firefox | 51+ | ✅ 完全支持 |
| Edge | 79+ | ✅ 完全支持 |
| Safari | 15+ | ⚠️ 需要启用 |
| Opera | 43+ | ✅ 完全支持 |

### Safari 启用 WebGL 2.0
1. 打开 Safari 偏好设置
2. 进入 "高级" 选项卡
3. 勾选 "在菜单栏中显示开发菜单"
4. 开发 > 实验性功能 > 启用 WebGL 2.0

## 性能优化建议

### 粒子数量调整
```csharp
// GPUParticleSystem.cs 中调整
[SerializeField] private int particleCount = 100000; // 可降低到 50000 或 30000
```

### 根据设备调整
- **桌面端**: 100,000 粒子
- **笔记本**: 50,000 粒子
- **移动端/平板**: 20,000 - 30,000 粒子

### 渲染优化
- 减小 `_ParticleSize` 以降低像素填充率
- 降低分辨率或使用缩放渲染
- 关闭不必要的后处理效果

### Shader 优化
- Compute Shader 已使用 256 线程/组（最优配置）
- 避免在 Update 中从 GPU 读取数据
- 使用 `Graphics.DrawProceduralIndirect` 最小化 CPU 开销

## 故障排除

### 问题：Compute Shader 不工作
**解决方案:**
1. 确保 Player Settings 中启用了 WebGL 2.0
2. 检查浏览器控制台是否有 WebGL 错误
3. 确认浏览器支持 WebGL 2.0 Compute Shader

### 问题：内存不足错误
**解决方案:**
1. 减少粒子数量
2. 增加 WebGL 内存分配（Player Settings > Memory Size）
3. 使用 Gzip 压缩减小构建大小

### 问题：性能低下
**解决方案:**
1. 降低粒子数量
2. 减小交互半径
3. 使用更小的粒子纹理
4. 降低屏幕分辨率

### 问题：粒子不显示
**解决方案:**
1. 检查材质是否正确分配
2. 确认 Shader 没有编译错误
3. 检查相机是否能够看到粒子范围
4. 查看 Console 是否有错误信息

## 项目结构

```
Assets/
├── Scripts/
│   └── GPUParticle/
│       ├── GPUParticleSystem.cs      # 主粒子系统
│       ├── CameraController.cs       # 相机控制
│       ├── FPSDisplay.cs             # FPS 显示
│       └── ParticleTextureGenerator.cs # 纹理生成
├── Shaders/
│   ├── ParticleCompute.compute       # 粒子更新 Compute Shader
│   └── ParticleRender.shader         # 粒子渲染 Shader
├── Editor/
│   ├── GPUParticleSceneSetup.cs      # 场景设置工具
│   └── WebGLBuilder.cs               # WebGL 构建工具
├── Materials/                        # 生成的材质
├── Textures/                         # 生成的纹理
└── Scenes/                           # 场景文件
```

## 自定义扩展

### 添加新的交互模式
在 `ParticleCompute.compute` 中修改鼠标交互部分：

```hlsl
// 在 UpdateParticles kernel 中添加
if (_MouseActive == 1)
{
    // 现有排斥力代码...
    
    // 添加吸引力
    // p.velocity -= direction * force * _DeltaTime * 0.5;
}
```

### 修改粒子颜色
在 `ParticleRender.shader` 的 fragment shader 中：

```hlsl
fixed4 frag (v2f i) : SV_Target
{
    // 添加自定义颜色计算
    float3 customColor = lerp(float3(1,0,0), float3(0,0,1), speedFactor);
    // ...
}
```

## 浏览器部署

### 本地测试
```bash
# 使用 Python 简单服务器
cd Builds/WebGL
python -m http.server 8000

# 或使用 Node.js
npx serve .
```

### 在线托管
- **GitHub Pages**: 免费，直接部署
- **Netlify**: 拖拽部署
- **Vercel**: 优秀的全球 CDN
- **AWS S3 + CloudFront**: 企业级解决方案

### Web 服务器配置
确保服务器支持以下 MIME 类型：
```
.application/wasm: .wasm
.application/javascript: .js
.application/octet-stream: .data, .mem
```

## 浏览器控制台调试

打开浏览器开发者工具 (F12) 查看：
- **Console**: JavaScript 和 WebGL 错误
- **Performance**: GPU 和 CPU 性能分析
- **Memory**: 内存使用情况
- **Rendering**: 绘制调用和帧率

## 许可证

本项目为示例代码，可自由使用于个人和商业项目。

## 支持与反馈

如有问题，请检查：
1. Unity 版本是否支持 WebGL 2.0 Compute Shader（2021.2+）
2. 浏览器是否支持 WebGL 2.0
3. 项目设置是否正确
