// using UnityEngine;
// using UnityEditor;
// using UnityEngine.Rendering.Universal;

// /// <summary>
// /// GPUParticleRendererFeature的自定义Inspector
// /// 提供渲染时机的可视化说明
// /// </summary>
// [CustomEditor(typeof(GPUParticleRendererFeature))]
// public class GPUParticleRendererFeatureEditor : Editor
// {
//     private SerializedProperty _settings;
//     private SerializedProperty _renderPassEvent;
//     private SerializedProperty _showDebugInfo;
    
//     private GUIStyle _boxStyle;
//     private GUIStyle _titleStyle;
//     private GUIStyle _descriptionStyle;
    
//     // 渲染时机的颜色标识
//     private readonly Color[] _eventColors = new Color[]
//     {
//         new Color(1f, 0.5f, 0.5f),    // BeforeRendering - 红
//         new Color(1f, 0.8f, 0.5f),    // BeforeRenderingShadows - 橙
//         new Color(1f, 1f, 0.5f),      // AfterRenderingShadows - 黄
//         new Color(0.5f, 1f, 0.5f),    // BeforeRenderingOpaques - 绿
//         new Color(0.5f, 1f, 0.8f),    // AfterRenderingOpaques - 青绿
//         new Color(0.5f, 0.8f, 1f),    // BeforeRenderingSkybox - 浅蓝
//         new Color(0.5f, 0.5f, 1f),    // AfterRenderingSkybox - 蓝
//         new Color(0.8f, 0.5f, 1f),    // BeforeRenderingTransparents - 紫
//         new Color(1f, 0.5f, 1f),      // AfterRenderingTransparents - 粉
//         new Color(1f, 0.5f, 0.8f),    // BeforeRenderingPostProcessing - 玫红
//         new Color(1f, 0.3f, 0.5f),    // AfterRenderingPostProcessing - 深红
//         new Color(0.5f, 0.5f, 0.5f),  // AfterRendering - 灰
//     };
    
//     private void OnEnable()
//     {
//         _settings = serializedObject.FindProperty("settings");
//         _renderPassEvent = _settings.FindPropertyRelative("renderPassEvent");
//         _showDebugInfo = _settings.FindPropertyRelative("showDebugInfo");
//     }
    
//     public override void OnInspectorGUI()
//     {
//         serializedObject.Update();
        
//         InitStyles();
        
//         // 标题
//         EditorGUILayout.Space(10);
//         EditorGUILayout.LabelField("URP GPU粒子系统", _titleStyle);
//         EditorGUILayout.Space(10);
        
//         // 基础配置
//         EditorGUILayout.LabelField("基础配置", EditorStyles.boldLabel);
//         EditorGUILayout.PropertyField(_settings.FindPropertyRelative("particleCompute"));
//         EditorGUILayout.PropertyField(_settings.FindPropertyRelative("particleMaterial"));
//         EditorGUILayout.PropertyField(_settings.FindPropertyRelative("particleCount"));
        
//         EditorGUILayout.Space(15);
        
//         // 渲染时机配置（重点）
//         EditorGUILayout.LabelField("渲染时机配置", EditorStyles.boldLabel);
        
//         RenderPassEvent currentEvent = (RenderPassEvent)_renderPassEvent.enumValueIndex;
        
//         // 渲染时机选择
//         EditorGUILayout.PropertyField(_renderPassEvent, new GUIContent("渲染时机", "选择粒子在URP管线中的渲染阶段"));
        
//         // 可视化管线图
//         EditorGUILayout.Space(10);
//         EditorGUILayout.LabelField("URP渲染管线流程图", EditorStyles.boldLabel);
//         DrawRenderPipelineVisual(currentEvent);
        
//         EditorGUILayout.Space(10);
        
//         // 当前选择的说明
//         DrawCurrentEventDescription(currentEvent);
        
//         EditorGUILayout.Space(15);
        
//         // 调试选项
//         EditorGUILayout.LabelField("调试", EditorStyles.boldLabel);
//         EditorGUILayout.PropertyField(_showDebugInfo);
        
//         EditorGUILayout.Space(10);
        
//         // 常用推荐
//         DrawRecommendations();
        
//         serializedObject.ApplyModifiedProperties();
//     }
    
//     private void InitStyles()
//     {
//         if (_boxStyle == null)
//         {
//             _boxStyle = new GUIStyle(EditorStyles.helpBox);
//             _boxStyle.padding = new RectOffset(10, 10, 10, 10);
//         }
        
//         if (_titleStyle == null)
//         {
//             _titleStyle = new GUIStyle(EditorStyles.largeLabel);
//             _titleStyle.fontSize = 18;
//             _titleStyle.fontStyle = FontStyle.Bold;
//             _titleStyle.alignment = TextAnchor.MiddleCenter;
//         }
        
//         if (_descriptionStyle == null)
//         {
//             _descriptionStyle = new GUIStyle(EditorStyles.label);
//             _descriptionStyle.wordWrap = true;
//             _descriptionStyle.richText = true;
//         }
//     }
    
//     /// <summary>
//     /// 绘制URP渲染管线可视化图
//     /// </summary>
//     private void DrawRenderPipelineVisual(RenderPassEvent currentEvent)
//     {
//         string[] eventNames = new string[]
//         {
//             "清屏前",
//             "阴影前",
//             "阴影后",
//             "不透明前",
//             "不透明后",
//             "天空盒前",
//             "天空盒后",
//             "透明前",
//             "透明后",
//             "后处理前",
//             "后处理后",
//             "帧结束"
//         };
        
//         RenderPassEvent[] events = (RenderPassEvent[])System.Enum.GetValues(typeof(RenderPassEvent));
        
//         EditorGUILayout.BeginVertical(_boxStyle);
        
//         for (int i = 0; i < events.Length; i++)
//         {
//             bool isCurrent = (events[i] == currentEvent);
            
//             EditorGUILayout.BeginHorizontal();
            
//             // 颜色标识（防止数组越界）
//             Rect colorRect = GUILayoutUtility.GetRect(20, 20, GUILayout.Width(20));
//             Color color = (i < _eventColors.Length) ? _eventColors[i] : Color.gray;
//             EditorGUI.DrawRect(colorRect, color);
            
//             GUILayout.Space(5);
            
//             // 阶段名称
//             GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
//             if (isCurrent)
//             {
//                 labelStyle.fontStyle = FontStyle.Bold;
//                 labelStyle.normal.textColor = Color.green;
//             }
            
//             EditorGUILayout.LabelField(eventNames[i], labelStyle, GUILayout.Width(100));
            
//             // 当前选中标记
//             if (isCurrent)
//             {
//                 EditorGUILayout.LabelField("◀ 当前选择", GUILayout.Width(80));
//             }
//             else
//             {
//                 GUILayout.FlexibleSpace();
//             }
            
//             EditorGUILayout.EndHorizontal();
//         }
        
//         EditorGUILayout.EndVertical();
//     }
    
//     /// <summary>
//     /// 绘制当前选择的渲染时机的详细说明
//     /// </summary>
//     private void DrawCurrentEventDescription(RenderPassEvent currentEvent)
//     {
//         EditorGUILayout.BeginVertical(_boxStyle);
        
//         EditorGUILayout.LabelField("当前选择说明", EditorStyles.boldLabel);
        
//         string description = GetEventDescription(currentEvent);
//         EditorGUILayout.LabelField(description, _descriptionStyle);
        
//         EditorGUILayout.EndVertical();
//     }
    
//     private string GetEventDescription(RenderPassEvent evt)
//     {
//         switch (evt)
//         {
//             case RenderPassEvent.BeforeRendering:
//                 return "<b>清屏前渲染</b>\n\n" +
//                        "粒子会在清屏之前绘制。\n" +
//                        "<color=red>注意：</color>此时深度缓冲还未准备好，通常不用于场景物体。\n" +
//                        "用途：背景效果、全屏预处理";
                
//             case RenderPassEvent.BeforeRenderingShadows:
//                 return "<b>阴影渲染前</b>\n\n" +
//                        "粒子会在阴影贴图生成之前绘制。\n" +
//                        "<color=red>注意：</color>粒子通常不产生阴影，一般不需要在此阶段渲染。";
                
//             case RenderPassEvent.AfterRenderingShadows:
//                 return "<b>阴影渲染后</b>\n\n" +
//                        "粒子会在阴影贴图生成之后绘制。\n" +
//                        "用途：如果你希望粒子参与阴影计算（极少见）";
                
//             case RenderPassEvent.BeforeRenderingOpaques:
//                 return "<b>不透明物体前</b>\n\n" +
//                        "粒子会在所有不透明物体（如地面、建筑）之前绘制。\n" +
//                        "<color=red>效果：</color>粒子会被后续的不透明物体遮挡。\n" +
//                        "用途：需要被场景遮挡的特效（如地面以下喷出的火焰）";
                
//             case RenderPassEvent.AfterRenderingOpaques:
//                 return "<b>不透明物体后</b>\n\n" +
//                        "粒子会在不透明物体之后，透明物体之前渲染。\n" +
//                        "<color=green>推荐：</color>这是粒子常用的渲染时机。\n" +
//                        "效果：粒子会显示在不透明物体前面，但会被透明物体（如玻璃、水面）覆盖。";
                
//             case RenderPassEvent.BeforeRenderingSkybox:
//                 return "<b>天空盒前</b>\n\n" +
//                        "粒子会在天空盒之前渲染。\n" +
//                        "用途：如果你希望天空盒覆盖粒子（少见）";
                
//             case RenderPassEvent.AfterRenderingSkybox:
//                 return "<b>天空盒后</b>\n\n" +
//                        "粒子会在天空盒之后渲染。\n" +
//                        "<color=yellow>注意：</color>粒子会显示在天空前面，但透明物体可能产生排序问题。";
                
//             case RenderPassEvent.BeforeRenderingTransparents:
//                 return "<b>透明物体前</b>\n\n" +
//                        "粒子会在透明物体（如玻璃、水面）之前渲染。\n" +
//                        "<color=yellow>注意：</color>如果粒子是透明的，可能产生深度排序问题。";
                
//             case RenderPassEvent.AfterRenderingTransparents:
//                 return "<b>透明物体后</b>\n\n" +
//                        "粒子会在所有透明物体之后渲染。\n" +
//                        "<color=green>推荐：</color>如果希望粒子显示在最前面，使用此选项。\n" +
//                        "<color=red>警告：</color>如果粒子也是透明的，可能会有排序问题。";
                
//             case RenderPassEvent.BeforeRenderingPostProcessing:
//                 return "<b>后处理前</b>\n\n" +
//                        "粒子会在后处理（如Bloom、Tone Mapping）之前渲染。\n" +
//                        "<color=green>推荐：</color>如果希望粒子受后处理影响（如Bloom发光效果）。";
                
//             case RenderPassEvent.AfterRenderingPostProcessing:
//                 return "<b>后处理后</b>\n\n" +
//                        "粒子会在后处理之后渲染。\n" +
//                        "<color=yellow>注意：</color>粒子不会受后处理影响（如不会有Bloom效果）。\n" +
//                        "用途：UI特效、不受后处理影响的效果";
                
//             case RenderPassEvent.AfterRendering:
//                 return "<b>帧结束前</b>\n\n" +
//                        "粒子会在所有渲染完成后绘制。\n" +
//                        "用途：调试信息显示、特殊的覆盖效果";
                
//             default:
//                 return "未知渲染时机";
//         }
//     }
    
//     /// <summary>
//     /// 绘制常用推荐
//     /// </summary>
//     private void DrawRecommendations()
//     {
//         EditorGUILayout.BeginVertical(_boxStyle);
        
//         EditorGUILayout.LabelField("常用推荐配置", EditorStyles.boldLabel);
        
//         EditorGUILayout.LabelField("<b>场景粒子（烟雾、火焰）：</b>\n" +
//                                    "→ 选择 <color=green>AfterRenderingOpaques</color>\n" +
//                                    "粒子会被透明物体正确遮挡", _descriptionStyle);
        
//         EditorGUILayout.Space(5);
        
//         EditorGUILayout.LabelField("<b>屏幕特效（魔法光环）：</b>\n" +
//                                    "→ 选择 <color=green>BeforeRenderingPostProcessing</color>\n" +
//                                    "粒子会受Bloom等后处理影响，发光效果更好", _descriptionStyle);
        
//         EditorGUILayout.Space(5);
        
//         EditorGUILayout.LabelField("<b>UI/覆盖特效：</b>\n" +
//                                    "→ 选择 <color=green>AfterRenderingTransparents</color>\n" +
//                                    "粒子显示在最前面", _descriptionStyle);
        
//         EditorGUILayout.EndVertical();
//     }
// }
