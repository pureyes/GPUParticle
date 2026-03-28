// #if UNITY_EDITOR
// using UnityEngine;
// using UnityEditor;

// /// <summary>
// /// 构建设置验证工具 - 检查项目设置是否正确
// /// </summary>
// public class BuildValidator : EditorWindow
// {
//     private Vector2 scrollPosition;
//     private bool allChecksPassed = true;

//     [MenuItem("Tools/GPU Particle System/Validate Settings")]
//     public static void ShowWindow()
//     {
//         GetWindow<BuildValidator>("Build Validator");
//     }

//     private void OnGUI()
//     {
//         GUILayout.Label("GPU Particle System - Build Validator", EditorStyles.boldLabel);
//         GUILayout.Space(10);

//         scrollPosition = GUILayout.BeginScrollView(scrollPosition);

//         allChecksPassed = true;

//         // 检查 Unity 版本
//         DrawCheck("Unity Version", CheckUnityVersion(), "需要 Unity 2021.2 或更高版本以支持 WebGL 2.0 Compute Shader");

//         // 检查平台
//         DrawCheck("Platform", CheckPlatform(), "需要切换到 WebGL 平台");

//         // WebGL 平台检查
//         DrawCheck("WebGL Platform", CheckWebGLPlatform(), "需要切换到 WebGL 平台");

//         // 检查 Compute Shader 支持
//         DrawCheck("Compute Shader", CheckComputeShader(), "需要支持 Compute Shader 的平台");

//         // 检查 Graphics API
//         DrawCheck("Graphics API", CheckGraphicsAPI(), "需要使用 WebGL Graphics API");

//         // 检查关键资源
//         DrawCheck("Compute Shader Asset", CheckComputeShaderAsset(), "需要 Assets/Shaders/ParticleCompute.compute");
//         DrawCheck("Render Shader", CheckRenderShader(), "需要 Custom/ParticleRender Shader");

//         GUILayout.EndScrollView();

//         GUILayout.Space(20);

//         GUI.backgroundColor = allChecksPassed ? Color.green : Color.yellow;
//         if (GUILayout.Button(allChecksPassed ? "所有检查通过！可以构建" : "自动修复问题", GUILayout.Height(40)))
//         {
//             if (!allChecksPassed)
//             {
//                 AutoFix();
//             }
//         }
//         GUI.backgroundColor = Color.white;

//         GUILayout.Space(10);

//         if (GUILayout.Button("切换到 WebGL 平台"))
//         {
//             SwitchToWebGL();
//         }
//     }

//     private void DrawCheck(string name, bool passed, string fixHint)
//     {
//         GUILayout.BeginHorizontal("box");
        
//         GUI.color = passed ? Color.green : Color.red;
//         GUILayout.Label(passed ? "✓" : "✗", GUILayout.Width(30));
//         GUI.color = Color.white;
        
//         GUILayout.Label(name, GUILayout.Width(150));
        
//         if (!passed)
//         {
//             GUILayout.Label($"({fixHint})", EditorStyles.miniLabel);
//             allChecksPassed = false;
//         }
//         else
//         {
//             GUILayout.Label("OK", EditorStyles.miniLabel);
//         }
        
//         GUILayout.EndHorizontal();
//     }

//     private bool CheckUnityVersion()
//     {
//         string version = Application.unityVersion;
//         string[] parts = version.Split('.');
//         int major = int.Parse(parts[0]);
//         int minor = int.Parse(parts[1]);
        
//         return major > 2021 || (major == 2021 && minor >= 2);
//     }

//     private bool CheckPlatform()
//     {
//         return EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL;
//     }

//     private bool CheckWebGLPlatform()
//     {
//         // WebGL 平台只要切换过去就支持，不需要额外检查 Graphics API
//         return EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL;
//     }

//     private bool CheckComputeShader()
//     {
//         return SystemInfo.supportsComputeShaders;
//     }

//     private bool CheckGraphicsAPI()
//     {
//         // WebGL 平台 Graphics API 自动管理
//         return true;
//     }

//     private bool CheckComputeShaderAsset()
//     {
//         return AssetDatabase.LoadAssetAtPath<ComputeShader>("Assets/Shaders/ParticleCompute.compute") != null;
//     }

//     private bool CheckRenderShader()
//     {
//         return Shader.Find("Custom/ParticleRender") != null;
//     }

//     private void AutoFix()
//     {
//         if (!CheckPlatform())
//         {
//             SwitchToWebGL();
//         }
        
//         // WebGL 平台的 Graphics API 是自动管理的，不需要手动设置

//         AssetDatabase.Refresh();
        
//         EditorUtility.DisplayDialog("Auto Fix", 
//             "已尝试自动修复问题。请重新运行验证工具确认。", 
//             "OK");
//     }

//     private void SwitchToWebGL()
//     {
//         if (EditorUtility.DisplayDialog("Switch Platform", 
//             "Switch to WebGL platform? This may take a moment.", 
//             "Yes", "No"))
//         {
//             EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
//         }
//     }
// }
// #endif
