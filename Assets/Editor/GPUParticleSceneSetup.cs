// #if UNITY_EDITOR
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEditor;
// using UnityEngine.SceneManagement;
// using UnityEditor.SceneManagement;

// /// <summary>
// /// GPU粒子系统场景设置工具 - 自动创建完整的场景
// /// </summary>
// public class GPUParticleSceneSetup : EditorWindow
// {
//     private int particleCount = 100000;
//     private bool createUICanvas = true;

//     [MenuItem("Tools/GPU Particle System/Setup Scene")]
//     public static void ShowWindow()
//     {
//         GetWindow<GPUParticleSceneSetup>("GPU Particle Setup");
//     }

//     private void OnGUI()
//     {
//         GUILayout.Label("GPU Particle System Scene Setup", EditorStyles.boldLabel);
//         GUILayout.Space(10);

//         particleCount = EditorGUILayout.IntField("Particle Count", particleCount);
//         particleCount = Mathf.Clamp(particleCount, 1000, 500000);
        
//         createUICanvas = EditorGUILayout.Toggle("Create UI Canvas", createUICanvas);
        
//         GUILayout.Space(20);
        
//         GUI.backgroundColor = Color.green;
//         if (GUILayout.Button("Setup Scene", GUILayout.Height(40)))
//         {
//             SetupScene();
//         }
//         GUI.backgroundColor = Color.white;
        
//         GUILayout.Space(10);
        
//         if (GUILayout.Button("Clear Scene"))
//         {
//             if (EditorUtility.DisplayDialog("Clear Scene", 
//                 "Are you sure you want to clear all objects in the current scene?", 
//                 "Yes", "No"))
//             {
//                 ClearScene();
//             }
//         }
//     }

//     private void SetupScene()
//     {
//         // 创建或获取主相机
//         Camera mainCamera = SetupCamera();
        
//         // 创建粒子系统
//         SetupParticleSystem();
        
//         // 创建UI
//         if (createUICanvas)
//         {
//             SetupUI();
//         }
        
//         // 创建光源
//         SetupLighting();
        
//         // 标记场景已修改
//         EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        
//         Debug.Log("GPU Particle System scene setup complete!");
//     }

//     private Camera SetupCamera()
//     {
//         Camera mainCamera = Camera.main;
        
//         if (mainCamera == null)
//         {
//             GameObject cameraObj = new GameObject("Main Camera");
//             mainCamera = cameraObj.AddComponent<Camera>();
//             cameraObj.tag = "MainCamera";
//         }

//         // 设置相机参数
//         mainCamera.clearFlags = CameraClearFlags.SolidColor;
//         mainCamera.backgroundColor = new Color(0.05f, 0.05f, 0.1f); // 深蓝黑色背景
//         mainCamera.fieldOfView = 60f;
//         mainCamera.nearClipPlane = 0.1f;
//         mainCamera.farClipPlane = 200f;
        
//         // 添加相机控制器
//         CameraController controller = mainCamera.GetComponent<CameraController>();
//         if (controller == null)
//         {
//             controller = mainCamera.gameObject.AddComponent<CameraController>();
//         }
        
//         // 设置初始位置
//         mainCamera.transform.position = new Vector3(30, 20, 30);
//         mainCamera.transform.LookAt(Vector3.zero);
        
//         Undo.RegisterFullObjectHierarchyUndo(mainCamera.gameObject, "Setup Camera");
        
//         return mainCamera;
//     }

//     private void SetupParticleSystem()
//     {
//         // 查找现有的粒子系统
//         GPUParticleSystem existingSystem = FindObjectOfType<GPUParticleSystem>();
//         GameObject particleObj;
        
//         if (existingSystem != null)
//         {
//             particleObj = existingSystem.gameObject;
//         }
//         else
//         {
//             particleObj = new GameObject("GPUParticleSystem");
//         }

//         GPUParticleSystem system = particleObj.GetComponent<GPUParticleSystem>();
//         if (system == null)
//         {
//             system = particleObj.AddComponent<GPUParticleSystem>();
//         }

//         // 添加 WebGL 兼容性检查
//         WebGLCompatibility compatibility = particleObj.GetComponent<WebGLCompatibility>();
//         if (compatibility == null)
//         {
//             compatibility = particleObj.AddComponent<WebGLCompatibility>();
//         }

//         // 加载或创建资源
//         ComputeShader computeShader = AssetDatabase.LoadAssetAtPath<ComputeShader>("Assets/Shaders/ParticleCompute.compute");
//         Shader renderShader = Shader.Find("Custom/ParticleRender");
        
//         // 创建材质
//         Material particleMaterial = new Material(renderShader);
//         particleMaterial.name = "ParticleMaterial";
        
//         // 创建纹理
//         Texture2D particleTexture = CreateParticleTexture();
        
//         // 保存资源
//         if (!AssetDatabase.IsValidFolder("Assets/Materials"))
//         {
//             AssetDatabase.CreateFolder("Assets", "Materials");
//         }
//         if (!AssetDatabase.IsValidFolder("Assets/Textures"))
//         {
//             AssetDatabase.CreateFolder("Assets", "Textures");
//         }
        
//         AssetDatabase.CreateAsset(particleMaterial, "Assets/Materials/ParticleMaterial.mat");
//         AssetDatabase.CreateAsset(particleTexture, "Assets/Textures/ParticleTexture.asset");
//         AssetDatabase.SaveAssets();

//         // 设置组件引用（通过反射设置私有字段）
//         SerializedObject so = new SerializedObject(system);
//         so.FindProperty("particleCount").intValue = particleCount;
//         so.FindProperty("particleCompute").objectReferenceValue = computeShader;
//         so.FindProperty("particleMaterial").objectReferenceValue = particleMaterial;
//         so.FindProperty("particleTexture").objectReferenceValue = particleTexture;
//         so.FindProperty("emitRadius").floatValue = 10f;
//         so.FindProperty("emitSpeed").floatValue = 5f;
//         so.FindProperty("particleLife").floatValue = 5f;
//         so.FindProperty("particleSize").floatValue = 0.08f;
//         so.FindProperty("interactionRadius").floatValue = 5f;
//         so.FindProperty("interactionStrength").floatValue = 30f;
//         so.FindProperty("enableMouseInteraction").boolValue = true;
//         so.ApplyModifiedProperties();

//         Undo.RegisterCreatedObjectUndo(particleObj, "Create Particle System");
        
//         Selection.activeGameObject = particleObj;
//     }

//     private void SetupUI()
//     {
//         // 创建 Canvas
//         GameObject canvasObj = new GameObject("Canvas");
//         Canvas canvas = canvasObj.AddComponent<Canvas>();
//         canvas.renderMode = RenderMode.ScreenSpaceOverlay;
//         canvas.sortingOrder = 100;
        
//         CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
//         scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
//         scaler.referenceResolution = new Vector2(1920, 1080);
//         scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
//         scaler.matchWidthOrHeight = 0.5f;
        
//         canvasObj.AddComponent<GraphicRaycaster>();
        
//         // 创建 EventSystem
//         GameObject eventSystem = new GameObject("EventSystem");
//         eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
//         eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        
//         // 创建 FPS 文本
//         GameObject fpsObj = new GameObject("FPSText");
//         fpsObj.transform.SetParent(canvasObj.transform, false);
        
//         RectTransform fpsRect = fpsObj.AddComponent<RectTransform>();
//         fpsRect.anchorMin = new Vector2(0, 1);
//         fpsRect.anchorMax = new Vector2(0, 1);
//         fpsRect.pivot = new Vector2(0, 1);
//         fpsRect.anchoredPosition = new Vector2(20, -20);
//         fpsRect.sizeDelta = new Vector2(200, 50);
        
//         Text fpsText = fpsObj.AddComponent<Text>();
//         fpsText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
//         fpsText.fontSize = 24;
//         fpsText.color = Color.green;
//         fpsText.text = "FPS: 60";
        
//         // 创建粒子数量文本
//         GameObject countObj = new GameObject("ParticleCountText");
//         countObj.transform.SetParent(canvasObj.transform, false);
        
//         RectTransform countRect = countObj.AddComponent<RectTransform>();
//         countRect.anchorMin = new Vector2(0, 1);
//         countRect.anchorMax = new Vector2(0, 1);
//         countRect.pivot = new Vector2(0, 1);
//         countRect.anchoredPosition = new Vector2(20, -60);
//         countRect.sizeDelta = new Vector2(300, 50);
        
//         Text countText = countObj.AddComponent<Text>();
//         countText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
//         countText.fontSize = 20;
//         countText.color = Color.white;
//         countText.text = $"Particles: {particleCount:N0}";
        
//         // 创建说明文本
//         GameObject infoObj = new GameObject("InfoText");
//         infoObj.transform.SetParent(canvasObj.transform, false);
        
//         RectTransform infoRect = infoObj.AddComponent<RectTransform>();
//         infoRect.anchorMin = new Vector2(0, 0);
//         infoRect.anchorMax = new Vector2(0, 0);
//         infoRect.pivot = new Vector2(0, 0);
//         infoRect.anchoredPosition = new Vector2(20, 20);
//         infoRect.sizeDelta = new Vector2(500, 100);
        
//         Text infoText = infoObj.AddComponent<Text>();
//         infoText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
//         infoText.fontSize = 16;
//         infoText.color = new Color(0.8f, 0.8f, 0.8f);
//         infoText.text = "Controls:\n" +
//                        "• Left Click + Drag: Interact with particles\n" +
//                        "• Right Click + Drag: Rotate camera\n" +
//                        "• Mouse Wheel: Zoom in/out";
        
//         // 添加 FPS 显示器脚本
//         FPSDisplay fpsDisplay = canvasObj.AddComponent<FPSDisplay>();
//         SerializedObject fpsSo = new SerializedObject(fpsDisplay);
//         fpsSo.FindProperty("fpsText").objectReferenceValue = fpsText;
//         fpsSo.FindProperty("particleCountText").objectReferenceValue = countText;
//         fpsSo.ApplyModifiedProperties();

//         Undo.RegisterCreatedObjectUndo(canvasObj, "Create UI");
//         Undo.RegisterCreatedObjectUndo(eventSystem, "Create EventSystem");
//     }

//     private void SetupLighting()
//     {
//         // 创建环境光
//         RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
//         RenderSettings.ambientIntensity = 0.5f;
        
//         // 创建方向光
//         Light[] lights = FindObjectsOfType<Light>();
//         Light dirLight = null;
//         foreach (var light in lights)
//         {
//             if (light.type == LightType.Directional)
//             {
//                 dirLight = light;
//                 break;
//             }
//         }
        
//         if (dirLight == null)
//         {
//             GameObject lightObj = new GameObject("Directional Light");
//             dirLight = lightObj.AddComponent<Light>();
//             dirLight.type = LightType.Directional;
//             Undo.RegisterCreatedObjectUndo(lightObj, "Create Light");
//         }
        
//         dirLight.color = new Color(0.95f, 0.95f, 1f);
//         dirLight.intensity = 0.8f;
//         dirLight.transform.rotation = Quaternion.Euler(50, -30, 0);
//     }

//     private Texture2D CreateParticleTexture()
//     {
//         int size = 128;
//         Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
//         texture.wrapMode = TextureWrapMode.Clamp;
//         texture.filterMode = FilterMode.Bilinear;
        
//         Color[] pixels = new Color[size * size];
//         Vector2 center = new Vector2(size / 2f, size / 2f);
//         float maxDist = size / 2f;
        
//         for (int y = 0; y < size; y++)
//         {
//             for (int x = 0; x < size; x++)
//             {
//                 Vector2 pos = new Vector2(x, y);
//                 float dist = Vector2.Distance(pos, center);
                
//                 // 软圆形渐变
//                 float alpha = 1f - Mathf.SmoothStep(0f, maxDist * 0.8f, dist);
//                 float glow = Mathf.Exp(-dist * dist / (maxDist * maxDist * 0.3f));
                
//                 int index = y * size + x;
//                 pixels[index] = new Color(1f, 1f, 1f, alpha * glow);
//             }
//         }
        
//         texture.SetPixels(pixels);
//         texture.Apply();
        
//         return texture;
//     }

//     private void ClearScene()
//     {
//         GameObject[] allObjects = SceneManager.GetActiveScene().GetRootGameObjects();
//         foreach (var obj in allObjects)
//         {
//             Undo.DestroyObjectImmediate(obj);
//         }
//     }
// }
// #endif
