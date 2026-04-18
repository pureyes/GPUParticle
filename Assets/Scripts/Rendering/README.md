# URP GPU粒子系统 - 渲染集成

今天完成的工作：将原来的`OnRenderObject()`渲染方式改为URP Renderer Feature，实现可控的渲染时机。

## 新增文件

| 文件 | 作用 |
|------|------|
| `GPUParticleRendererFeature.cs` | URP Renderer Feature，可配置粒子渲染时机 |
| `GPUParticleRenderPass.cs` | 实际的渲染Pass，处理Compute Shader调度和绘制 |
| `Editor/GPUParticleRendererFeatureEditor.cs` | 可视化Inspector，显示URP管线流程图 |
| `Editor/GPUParticleSetupWizard.cs` | 设置向导，帮助配置Renderer Feature |

## 使用步骤

### 1. 打开设置向导
```
Tools > GPU Particle System > Setup Wizard
```

### 2. 配置Renderer Feature
- 在向导中选择你的URP Renderer Data资产
- 点击"添加GPU粒子Renderer Feature"
- 或者在Project窗口找到Renderer Data，手动Add Renderer Feature

### 3. 配置参数
选中添加的Renderer Feature，配置：
- **Compute Shader**: `Assets/Shaders/ParticleCompute.compute`
- **Material**: 使用 `Custom/ParticleRender` Shader创建的材质
- **渲染时机**: 选择你想要的Render Pass Event

## 渲染时机说明

在Inspector中可以看到URP管线的可视化流程图：

```
[清屏前] → [阴影] → [不透明物体] → [天空盒] → [透明物体] → [后处理] → [帧结束]
```

常用选择：
- **AfterRenderingOpaques**: 粒子在不透明物体后渲染，会被透明物体遮挡
- **BeforeRenderingPostProcessing**: 粒子在后处理前渲染，会受Bloom影响
- **AfterRenderingTransparents**: 粒子在最前面渲染

## 测试不同渲染时机的效果

1. 在场景中添加一些不透明物体（Cube）
2. 添加透明物体（带透明材质）
3. 在Inspector中切换Render Pass Event
4. 观察粒子与不同物体的遮挡关系

## 与原版的区别

| 特性 | 原版 (MonoBehaviour) | 新版 (Renderer Feature) |
|------|---------------------|------------------------|
| 渲染时机 | 不确定（OnRenderObject） | 精确控制 |
| Scene View支持 | 支持 | 支持 |
| SRP Batcher兼容 | 有限 | 完全兼容 |
| 多相机支持 | 手动处理 | 自动处理 |
| 渲染顺序控制 | 无 | 完整控制 |

## 下一步

完成今天的任务后：
1. 测试不同渲染时机的效果差异
2. 添加粒子发射器的Gizmos可视化
3. 创建自定义Inspector控制粒子参数
