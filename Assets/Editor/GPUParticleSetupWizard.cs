// using UnityEngine;
// using UnityEditor;
// using UnityEngine.Rendering;
// using UnityEngine.Rendering.Universal;

// /// <summary>
// /// GPU粒子系统设置向导
// /// 帮助用户快速将Renderer Feature添加到URP管线
// /// </summary>
// public class GPUParticleSetupWizard : EditorWindow
// {
//     private UniversalRendererData _rendererData;
//     private Vector2 _scrollPosition;
//     private bool _showHelp = true;
    
//     [MenuItem("Tools/GPU Particle System/Setup Wizard")]
//     public static void ShowWindow()
//     {
//         GetWindow<GPUParticleSetupWizard>("GPU粒子设置");
//     }
    
//     private void OnGUI()
//     {
//         _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        
//         DrawHeader();
        
//         if (_showHelp)
//         {
//             DrawHelpSection();
//         }
        
//         DrawSetupSection();
        
//         DrawTroubleshooting();
        
//         EditorGUILayout.EndScrollView();
//     }
    
//     private void DrawHeader()
//     {
//         EditorGUILayout.Space(10);
        
//         GUIStyle titleStyle = new GUIStyle(EditorStyles.largeLabel);
//         titleStyle.fontSize = 20;
//         titleStyle.fontStyle = FontStyle.Bold;
//         titleStyle.alignment = TextAnchor.MiddleCenter;
        
//         EditorGUILayout.LabelField("GPU粒子系统设置向导", titleStyle);
        
//         EditorGUILayout.Space(5);
        
//         GUIStyle subtitleStyle = new GUIStyle(EditorStyles.label);
//         subtitleStyle.alignment = TextAnchor.MiddleCenter;
//         EditorGUILayout.LabelField("将Compute Shader粒子系统集成到URP管线", subtitleStyle);
        
//         EditorGUILayout.Space(10);
        
//         _showHelp = EditorGUILayout.Foldout(_showHelp, "显示/隐藏帮助", true);
        
//         EditorGUILayout.Space(10);
//     }
    
//     private void DrawHelpSection()
//     {
//         GUIStyle boxStyle = new GUIStyle(EditorStyles.helpBox);
//         boxStyle.padding = new RectOffset(15, 15, 15, 15);
        
//         EditorGUILayout.BeginVertical(boxStyle);
        
//         EditorGUILayout.LabelField("配置步骤", EditorStyles.boldLabel);
        
//         EditorGUILayout.LabelField(
//             "1. 找到你的URP Renderer Data资产\n" +
//             "   - 通常在 Assets/Settings 或 Assets/Rendering 文件夹\n" +
//             "   - 文件名通常是 UniversalRendererData 或类似名称\n\n" +
//             "2. 将下面的Renderer Feature添加到Renderer Data\n\n" +
//             "3. 配置Compute Shader和Material",
//             EditorStyles.wordWrappedLabel
//         );
        
//         EditorGUILayout.Space(10);
        
//         EditorGUILayout.LabelField("文件位置", EditorStyles.boldLabel);
//         EditorGUILayout.LabelField(
//             "• Renderer Feature: Assets/Scripts/Rendering/GPUParticleRendererFeature.cs\n" +
//             "• Render Pass: Assets/Scripts/Rendering/GPUParticleRenderPass.cs\n" +
//             "• Compute Shader: Assets/Shaders/ParticleCompute.compute\n" +
//             "• Render Shader: Assets/Shaders/ParticleRender.shader",
//             EditorStyles.wordWrappedLabel
//         );
        
//         EditorGUILayout.EndVertical();
        
//         EditorGUILayout.Space(15);
//     }
    
//     private void DrawSetupSection()
//     {
//         EditorGUILayout.LabelField("URP Renderer配置", EditorStyles.boldLabel);
        
//         // 选择Renderer Data
//         _rendererData = EditorGUILayout.ObjectField(
//             "URP Renderer Data",
//             _rendererData,
//             typeof(UniversalRendererData),
//             false
//         ) as UniversalRendererData;
        
//         EditorGUILayout.Space(10);
        
//         if (_rendererData == null)
//         {
//             EditorGUILayout.HelpBox(
//                 "请先将URP Renderer Data拖到上方字段，或点击按钮自动查找",
//                 MessageType.Info
//             );
            
//             EditorGUILayout.Space(5);
            
//             if (GUILayout.Button("自动查找Renderer Data", GUILayout.Height(30)))
//             {
//                 FindRendererData();
//             }
//         }
//         else
//         {
//             // 检查是否已添加
//             bool alreadyAdded = false;
//             GPUParticleRendererFeature existingFeature = null;
            
//             foreach (var feature in _rendererData.rendererFeatures)
//             {
//                 if (feature is GPUParticleRendererFeature gpuFeature)
//                 {
//                     alreadyAdded = true;
//                     existingFeature = gpuFeature;
//                     break;
//                 }
//             }
            
//             if (alreadyAdded)
//             {
//                 EditorGUILayout.HelpBox(
//                     "✓ Renderer Feature已添加到此Renderer Data",
//                     MessageType.Info
//                 );
                
//                 EditorGUILayout.Space(10);
                
//                 if (GUILayout.Button("选中Renderer Feature", GUILayout.Height(30)))
//                 {
//                     Selection.activeObject = existingFeature;
//                 }
                
//                 if (GUILayout.Button("移除Renderer Feature", GUILayout.Height(25)))
//                 {
//                     RemoveRendererFeature();
//                 }
//             }
//             else
//             {
//                 EditorGUILayout.HelpBox(
//                     "Renderer Feature尚未添加。点击下方按钮添加。",
//                     MessageType.Warning
//                 );
                
//                 EditorGUILayout.Space(10);
                
//                 GUI.backgroundColor = new Color(0.4f, 0.8f, 0.4f);
//                 if (GUILayout.Button("添加GPU粒子Renderer Feature", GUILayout.Height(40)))
//                 {
//                     AddRendererFeature();
//                 }
//                 GUI.backgroundColor = Color.white;
//             }
            
//             EditorGUILayout.Space(10);
            
//             // 快速打开Renderer Data
//             if (GUILayout.Button("选中Renderer Data资产", GUILayout.Height(25)))
//             {
//                 Selection.activeObject = _rendererData;
//                 EditorGUIUtility.PingObject(_rendererData);
//             }
//         }
        
//         EditorGUILayout.Space(20);
        
//         // 快速创建材质按钮
//         DrawMaterialCreationSection();
//     }
    
//     private void DrawMaterialCreationSection()
//     {
//         EditorGUILayout.LabelField("快速创建材质", EditorStyles.boldLabel);
        
//         EditorGUILayout.HelpBox(
//             "如果你还没有粒子渲染材质，可以使用下方按钮快速创建",
//             MessageType.Info
//         );
        
//         EditorGUILayout.Space(5);
        
//         if (GUILayout.Button("创建粒子渲染材质", GUILayout.Height(30)))
//         {
//             CreateParticleMaterial();
//         }
//     }
    
//     private void DrawTroubleshooting()
//     {
//         EditorGUILayout.Space(20);
        
//         EditorGUILayout.LabelField("常见问题", EditorStyles.boldLabel);
        
//         GUIStyle boxStyle = new GUIStyle(EditorStyles.helpBox);
//         EditorGUILayout.BeginVertical(boxStyle);
        
//         EditorGUILayout.LabelField("Q: 找不到Renderer Data？", EditorStyles.boldLabel);
//         EditorGUILayout.LabelField(
//             "A: 在Project窗口搜索 't:UniversalRendererData'，或在\n" +
//             "   Edit > Project Settings > Graphics > Scriptable Render Pipeline Settings\n" +
//             "   查看当前使用的管线资产",
//             EditorStyles.wordWrappedLabel
//         );
        
//         EditorGUILayout.Space(10);
        
//         EditorGUILayout.LabelField("Q: 添加后粒子不显示？", EditorStyles.boldLabel);
//         EditorGUILayout.LabelField(
//             "A: 1. 检查Renderer Feature的配置（Compute Shader和Material）\n" +
//             "   2. 检查渲染时机设置（尝试不同的Render Pass Event）\n" +
//             "   3. 在Scene视图检查粒子是否在相机范围内",
//             EditorStyles.wordWrappedLabel
//         );
        
//         EditorGUILayout.EndVertical();
//     }
    
//     private void FindRendererData()
//     {
//         // 尝试从GraphicsSettings获取
//         var pipelineAsset = GraphicsSettings.defaultRenderPipeline as UniversalRenderPipelineAsset;
//         if (pipelineAsset != null)
//         {
//             // 通过反射获取Renderer Data列表
//             var property = pipelineAsset.GetType().GetProperty("scriptableRenderer", 
//                 System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
//             if (property != null)
//             {
//                 var renderer = property.GetValue(pipelineAsset) as ScriptableRenderer;
//                 if (renderer != null)
//                 {
//                     // 获取Renderer Data
//                     var dataField = renderer.GetType().GetField("m_RendererData", 
//                         System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//                     if (dataField != null)
//                     {
//                         _rendererData = dataField.GetValue(renderer) as UniversalRendererData;
//                     }
//                 }
//             }
//         }
        
//         // 如果没找到，搜索资产
//         if (_rendererData == null)
//         {
//             string[] guids = AssetDatabase.FindAssets("t:UniversalRendererData");
//             if (guids.Length > 0)
//             {
//                 string path = AssetDatabase.GUIDToAssetPath(guids[0]);
//                 _rendererData = AssetDatabase.LoadAssetAtPath<UniversalRendererData>(path);
//             }
//         }
        
//         if (_rendererData == null)
//         {
//             EditorUtility.DisplayDialog("未找到", 
//                 "未找到UniversalRendererData资产。请手动指定。", "确定");
//         }
//     }
    
//     private void AddRendererFeature()
//     {
//         if (_rendererData == null) return;
        
//         // 创建Renderer Feature实例
//         var feature = ScriptableObject.CreateInstance<GPUParticleRendererFeature>();
//         feature.name = "GPU Particle System";
        
//         // 添加到Renderer Data
//         _rendererData.rendererFeatures.Add(feature);
//         _rendererData.SetDirty();
        
//         // 保存
//         AssetDatabase.SaveAssets();
        
//         EditorUtility.DisplayDialog("成功", 
//             "GPU Particle Renderer Feature已添加！\n\n" +
//             "请选中Renderer Feature并配置Compute Shader和Material。", "确定");
        
//         // 选中Renderer Data
//         Selection.activeObject = _rendererData;
//         EditorGUIUtility.PingObject(_rendererData);
//     }
    
//     private void RemoveRendererFeature()
//     {
//         if (_rendererData == null) return;
        
//         for (int i = _rendererData.rendererFeatures.Count - 1; i >= 0; i--)
//         {
//             if (_rendererData.rendererFeatures[i] is GPUParticleRendererFeature)
//             {
//                 _rendererData.rendererFeatures.RemoveAt(i);
//             }
//         }
        
//         _rendererData.SetDirty();
//         AssetDatabase.SaveAssets();
//     }
    
//     private void CreateParticleMaterial()
//     {
//         // 查找Shader
//         Shader shader = Shader.Find("Custom/ParticleRender");
//         if (shader == null)
//         {
//             EditorUtility.DisplayDialog("错误", 
//                 "未找到Shader 'Custom/ParticleRender'。\n" +
//                 "请确保ParticleRender.shader已导入。", "确定");
//             return;
//         }
        
//         // 创建材质
//         Material material = new Material(shader);
//         material.name = "GPUParticleMaterial";
        
//         // 保存路径
//         string path = EditorUtility.SaveFilePanelInProject(
//             "保存粒子材质",
//             "GPUParticleMaterial",
//             "mat",
//             "选择保存位置"
//         );
        
//         if (!string.IsNullOrEmpty(path))
//         {
//             AssetDatabase.CreateAsset(material, path);
//             AssetDatabase.SaveAssets();
            
//             EditorUtility.DisplayDialog("成功", 
//                 $"材质已保存到: {path}", "确定");
            
//             Selection.activeObject = material;
//         }
//     }
// }
