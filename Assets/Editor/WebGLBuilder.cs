// #if UNITY_EDITOR
// using UnityEngine;
// using UnityEditor;
// using UnityEditor.Build.Reporting;

// /// <summary>
// /// WebGL 构建设置和自动化脚本
// /// </summary>
// public class WebGLBuilder
// {
//     private const string BuildPath = "Builds/WebGL";

//     [MenuItem("Tools/GPU Particle System/Build WebGL")]
//     public static void BuildWebGL()
//     {
//         // 检查并切换平台
//         if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
//         {
//             Debug.Log("Switching to WebGL platform...");
//             EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.WebGL, BuildTarget.WebGL);
//         }

//         // 配置构建设置
//         ConfigureWebGLSettings();

//         // 创建构建目录
//         if (!System.IO.Directory.Exists(BuildPath))
//         {
//             System.IO.Directory.CreateDirectory(BuildPath);
//         }

//         // 配置构建设置
//         BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
//         buildPlayerOptions.scenes = GetEnabledScenes();
//         buildPlayerOptions.locationPathName = BuildPath;
//         buildPlayerOptions.target = BuildTarget.WebGL;
//         buildPlayerOptions.options = BuildOptions.ShowBuiltPlayer;

//         Debug.Log("Starting WebGL build...");
//         BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
//         BuildSummary summary = report.summary;

//         if (summary.result == BuildResult.Succeeded)
//         {
//             Debug.Log($"WebGL build succeeded: {summary.totalSize / 1024 / 1024} MB");
//         }
//         else if (summary.result == BuildResult.Failed)
//         {
//             Debug.LogError("WebGL build failed!");
//         }
//     }

//     [MenuItem("Tools/GPU Particle System/Configure WebGL Settings")]
//     public static void ConfigureWebGLSettings()
//     {
//         // 获取或创建 WebGL 构建设置
//         EditorUserBuildSettings.selectedBuildTargetGroup = BuildTargetGroup.WebGL;
        
//         // Player Settings
//         PlayerSettings.WebGL.memorySize = 512; // 512MB 内存
//         PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.ExplicitlyThrownExceptionsOnly;
//         PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
//         PlayerSettings.WebGL.nameFilesAsHashes = true;
//         PlayerSettings.WebGL.dataCaching = true;
        
//         // WebGL 模板 - 使用自定义模板
//         PlayerSettings.WebGL.template = "PROJECT:GPUParticle";
        
//         // 其他设置
//         PlayerSettings.SetScriptingBackend(BuildTargetGroup.WebGL, ScriptingImplementation.IL2CPP);
//         PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.WebGL, Il2CppCompilerConfiguration.Release);
        
//         // 注意：WebGL 平台的 Graphics API 由 Unity 自动管理，不需要手动设置
//         // WebGL 2.0 支持通过其他 PlayerSettings 控制
        
//         // 质量设置
//         QualitySettings.SetQualityLevel(2); // Good quality
        
//         Debug.Log("WebGL settings configured!");
        
//         // 标记设置已修改
//         EditorUtility.SetDirty(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/ProjectSettings.asset")[0]);
//     }

//     [MenuItem("Tools/GPU Particle System/Quick Setup & Build")]
//     public static void QuickSetupAndBuild()
//     {
//         // 设置场景
//         GPUParticleSceneSetup.ShowWindow();
        
//         // 保存场景
//         if (!System.IO.Directory.Exists("Assets/Scenes"))
//         {
//             System.IO.Directory.CreateDirectory("Assets/Scenes");
//         }
        
//         UnityEditor.SceneManagement.EditorSceneManager.SaveScene(
//             UnityEngine.SceneManagement.SceneManager.GetActiveScene(), 
//             "Assets/Scenes/GPUParticleScene.unity");
        
//         // 构建设置
//         ConfigureWebGLSettings();
        
//         // 构建
//         BuildWebGL();
//     }

//     private static string[] GetEnabledScenes()
//     {
//         var scenes = new System.Collections.Generic.List<string>();
//         for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
//         {
//             if (EditorBuildSettings.scenes[i].enabled)
//             {
//                 scenes.Add(EditorBuildSettings.scenes[i].path);
//             }
//         }
        
//         // 如果没有启用的场景，使用当前场景
//         if (scenes.Count == 0)
//         {
//             string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
//             if (!string.IsNullOrEmpty(currentScene))
//             {
//                 scenes.Add(currentScene);
//             }
//         }
        
//         return scenes.ToArray();
//     }
// }
// #endif
