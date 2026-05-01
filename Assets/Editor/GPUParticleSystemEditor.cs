using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;
using System.IO;
using System.Linq;

/// <summary>
/// GPUParticleSystem 的自定义 Inspector
/// 提供分组参数编辑、实时预览按钮、Gizmos控制和Preset系统
/// </summary>
[CustomEditor(typeof(GPUParticleSystem))]
public class GPUParticleSystemEditor : Editor
{
    // SerializedProperties
    private SerializedProperty _particleCount;
    private SerializedProperty _particleLife;
    private SerializedProperty _emitRadius;
    private SerializedProperty _emitSpeed;
    private SerializedProperty _gravity;
    private SerializedProperty _damping;
    private SerializedProperty _windForce;
    private SerializedProperty _enableMouseInteraction;
    private SerializedProperty _interactionRadius;
    private SerializedProperty _interactionStrength;
    private SerializedProperty _renderPassEvent;
    private SerializedProperty _enableSceneViewPreview;
    private SerializedProperty _showDebugInfo;
    private SerializedProperty _targetRendererFeature;
    private SerializedProperty _showEmitRadiusGizmo;
    private SerializedProperty _showInteractionGizmo;
    private SerializedProperty _showParticleDistributionPreview;
    private SerializedProperty _distributionPreviewCount;
    private SerializedProperty _gizmoColor;
    private SerializedProperty _interactionGizmoColor;

    // Foldouts
    private bool _showBasic = true;
    private bool _showPhysics = true;
    private bool _showInteraction = true;
    private bool _showRender = true;
    private bool _showGizmos = true;
    private bool _showRuntime = true;
    private bool _showPreset = true;

    // Preset
    private int _selectedPresetIndex = 0;
    private string[] _presetNames = new string[0];
    private string[] _presetPaths = new string[0];
    private string _newPresetName = "NewPreset";

    private GUIStyle _titleStyle;
    private GUIStyle _boxStyle;

    private void OnEnable()
    {
        _particleCount = serializedObject.FindProperty("particleCount");
        _particleLife = serializedObject.FindProperty("particleLife");
        _emitRadius = serializedObject.FindProperty("emitRadius");
        _emitSpeed = serializedObject.FindProperty("emitSpeed");
        _gravity = serializedObject.FindProperty("gravity");
        _damping = serializedObject.FindProperty("damping");
        _windForce = serializedObject.FindProperty("windForce");
        _enableMouseInteraction = serializedObject.FindProperty("enableMouseInteraction");
        _interactionRadius = serializedObject.FindProperty("interactionRadius");
        _interactionStrength = serializedObject.FindProperty("interactionStrength");
        _renderPassEvent = serializedObject.FindProperty("renderPassEvent");
        _enableSceneViewPreview = serializedObject.FindProperty("enableSceneViewPreview");
        _showDebugInfo = serializedObject.FindProperty("showDebugInfo");
        _targetRendererFeature = serializedObject.FindProperty("targetRendererFeature");
        _showEmitRadiusGizmo = serializedObject.FindProperty("showEmitRadiusGizmo");
        _showInteractionGizmo = serializedObject.FindProperty("showInteractionGizmo");
        _showParticleDistributionPreview = serializedObject.FindProperty("showParticleDistributionPreview");
        _distributionPreviewCount = serializedObject.FindProperty("distributionPreviewCount");
        _gizmoColor = serializedObject.FindProperty("gizmoColor");
        _interactionGizmoColor = serializedObject.FindProperty("interactionGizmoColor");

        RefreshPresetList();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        InitStyles();

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("GPU 粒子系统", _titleStyle);
        EditorGUILayout.Space(10);

        // Renderer Feature 引用
        EditorGUILayout.PropertyField(_targetRendererFeature, new GUIContent("目标 Renderer Feature"));
        if (_targetRendererFeature.objectReferenceValue == null)
        {
            EditorGUILayout.HelpBox("未指定 Renderer Feature，系统将尝试自动查找。", MessageType.Info);
            if (GUILayout.Button("自动查找 Renderer Feature", GUILayout.Height(25)))
            {
                AutoFindRendererFeature();
            }
        }
        EditorGUILayout.Space(10);

        // 基础参数
        _showBasic = EditorGUILayout.Foldout(_showBasic, "基础参数", true);
        if (_showBasic)
        {
            EditorGUILayout.PropertyField(_particleCount);
            EditorGUILayout.PropertyField(_particleLife);
            EditorGUILayout.PropertyField(_emitRadius);
            EditorGUILayout.PropertyField(_emitSpeed);
        }
        EditorGUILayout.Space(5);

        // 物理参数
        _showPhysics = EditorGUILayout.Foldout(_showPhysics, "物理参数", true);
        if (_showPhysics)
        {
            EditorGUILayout.PropertyField(_gravity);
            EditorGUILayout.PropertyField(_damping);
            EditorGUILayout.PropertyField(_windForce);
        }
        EditorGUILayout.Space(5);

        // 交互参数
        _showInteraction = EditorGUILayout.Foldout(_showInteraction, "交互参数", true);
        if (_showInteraction)
        {
            EditorGUILayout.PropertyField(_enableMouseInteraction);
            if (_enableMouseInteraction.boolValue)
            {
                EditorGUILayout.PropertyField(_interactionRadius);
                EditorGUILayout.PropertyField(_interactionStrength);
            }
        }
        EditorGUILayout.Space(5);

        // 渲染设置
        _showRender = EditorGUILayout.Foldout(_showRender, "渲染设置", true);
        if (_showRender)
        {
            EditorGUILayout.PropertyField(_renderPassEvent);
            EditorGUILayout.PropertyField(_enableSceneViewPreview);
            EditorGUILayout.PropertyField(_showDebugInfo);
        }
        EditorGUILayout.Space(5);

        // Gizmos
        _showGizmos = EditorGUILayout.Foldout(_showGizmos, "Gizmos 可视化", true);
        if (_showGizmos)
        {
            EditorGUILayout.PropertyField(_showEmitRadiusGizmo);
            EditorGUILayout.PropertyField(_showInteractionGizmo);
            EditorGUILayout.PropertyField(_showParticleDistributionPreview);
            if (_showParticleDistributionPreview.boolValue)
            {
                EditorGUILayout.PropertyField(_distributionPreviewCount);
            }
            EditorGUILayout.PropertyField(_gizmoColor);
            EditorGUILayout.PropertyField(_interactionGizmoColor);
        }
        EditorGUILayout.Space(5);

        // 实时预览按钮
        _showRuntime = EditorGUILayout.Foldout(_showRuntime, "实时预览", true);
        if (_showRuntime)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("重置粒子", GUILayout.Height(28)))
            {
                ResetParticles();
            }

            var system = target as GPUParticleSystem;
            bool isPaused = system != null && system.isPaused;
            string pauseLabel = isPaused ? "\u25B6 继续" : "\u23F8 暂停";
            if (GUILayout.Button(pauseLabel, GUILayout.Height(28)))
            {
                TogglePause();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("应用设置到 Renderer Feature", GUILayout.Height(28)))
            {
                ApplySettings();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.Space(10);

        // Preset 系统
        _showPreset = EditorGUILayout.Foldout(_showPreset, "Preset 系统", true);
        if (_showPreset)
        {
            DrawPresetSection();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        var system = target as GPUParticleSystem;
        if (system == null) return;

        // 粒子数量统计（Scene视图左上角）
        DrawParticleStatsGUI(system);

        // 鼠标交互范围可视化
        if (system.showInteractionGizmo && system.enableMouseInteraction)
        {
            HandleInteractionGizmo(system);
        }
    }

    /// <summary>
    /// 在Scene视图绘制粒子统计信息
    /// </summary>
    private void DrawParticleStatsGUI(GPUParticleSystem system)
    {
        Handles.BeginGUI();
        Rect rect = new Rect(10, 10, 220, 80);
        GUI.Box(rect, GUIContent.none, EditorStyles.helpBox);
        GUI.Label(new Rect(rect.x + 10, rect.y + 8, 200, 20), $"粒子数量: {system.particleCount:N0}", EditorStyles.boldLabel);
        GUI.Label(new Rect(rect.x + 10, rect.y + 30, 200, 20), $"发射半径: {system.emitRadius:F1}", EditorStyles.label);
        GUI.Label(new Rect(rect.x + 10, rect.y + 50, 200, 20), $"生命周期: {system.particleLife:F1}s", EditorStyles.label);
        Handles.EndGUI();
    }

    /// <summary>
    /// 当鼠标按下时绘制交互范围
    /// </summary>
    private void HandleInteractionGizmo(GPUParticleSystem system)
    {
        Event e = Event.current;
        if (e == null) return;

        bool isMouseDown = e.type == EventType.MouseDown || e.type == EventType.MouseDrag;
        if (!isMouseDown) return;

        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, system.transform.position);
        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Handles.color = system.interactionGizmoColor;
            Handles.DrawWireDisc(hitPoint, Vector3.up, system.interactionRadius);
            Handles.color = new Color(system.interactionGizmoColor.r, system.interactionGizmoColor.g, system.interactionGizmoColor.b, 0.1f);
            Handles.DrawSolidDisc(hitPoint, Vector3.up, system.interactionRadius);
        }
    }

    private void InitStyles()
    {
        if (_titleStyle == null)
        {
            _titleStyle = new GUIStyle(EditorStyles.largeLabel);
            _titleStyle.fontSize = 18;
            _titleStyle.fontStyle = FontStyle.Bold;
            _titleStyle.alignment = TextAnchor.MiddleCenter;
        }
        if (_boxStyle == null)
        {
            _boxStyle = new GUIStyle(EditorStyles.helpBox);
            _boxStyle.padding = new RectOffset(10, 10, 10, 10);
        }
    }

    private void AutoFindRendererFeature()
    {
        string[] guids = AssetDatabase.FindAssets("t:GPUParticleRendererFeature");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var feature = AssetDatabase.LoadAssetAtPath<GPUParticleRendererFeature>(path);
            _targetRendererFeature.objectReferenceValue = feature;
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            EditorUtility.DisplayDialog("未找到", "未找到 GPUParticleRendererFeature，请先通过 Setup Wizard 添加。", "确定");
        }
    }

    private void ResetParticles()
    {
        var system = target as GPUParticleSystem;
        system?.ResetParticles();
    }

    private void TogglePause()
    {
        var system = target as GPUParticleSystem;
        system?.TogglePause();
        EditorUtility.SetDirty(target);
    }

    private void ApplySettings()
    {
        var system = target as GPUParticleSystem;
        if (system == null) return;

        system.SyncToRendererFeature();
        EditorUtility.SetDirty(target);
        Debug.Log("[GPUParticleSystem] 设置已应用到 Renderer Feature");
    }

    // ==================== Preset 系统 ====================

    private void DrawPresetSection()
    {
        EditorGUILayout.BeginVertical(_boxStyle);

        // 保存当前配置
        EditorGUILayout.LabelField("保存 Preset", EditorStyles.boldLabel);
        _newPresetName = EditorGUILayout.TextField("Preset 名称", _newPresetName);
        if (GUILayout.Button("保存当前配置为 Preset", GUILayout.Height(25)))
        {
            SavePreset(_newPresetName);
        }
        EditorGUILayout.Space(10);

        // 加载 Preset
        EditorGUILayout.LabelField("加载 Preset", EditorStyles.boldLabel);
        if (_presetNames.Length > 0)
        {
            _selectedPresetIndex = EditorGUILayout.Popup("选择 Preset", _selectedPresetIndex, _presetNames);
            if (GUILayout.Button("加载选中的 Preset", GUILayout.Height(25)))
            {
                LoadPreset(_presetPaths[_selectedPresetIndex]);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("暂无自定义 Preset", MessageType.Info);
        }
        EditorGUILayout.Space(10);

        // 内置 Preset
        EditorGUILayout.LabelField("内置 Preset", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("创建 爆炸 Preset", GUILayout.Height(25)))
        {
            CreateBuiltinPreset("Explosion", CreateExplosionSettings);
        }
        if (GUILayout.Button("创建 下雪 Preset", GUILayout.Height(25)))
        {
            CreateBuiltinPreset("Snow", CreateSnowSettings);
        }
        if (GUILayout.Button("创建 魔法 Preset", GUILayout.Height(25)))
        {
            CreateBuiltinPreset("Magic", CreateMagicSettings);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    private void RefreshPresetList()
    {
        string[] guids = AssetDatabase.FindAssets("t:GPUParticlePreset");
        _presetPaths = guids.Select(g => AssetDatabase.GUIDToAssetPath(g)).ToArray();
        _presetNames = _presetPaths.Select(p => Path.GetFileNameWithoutExtension(p)).ToArray();
        _selectedPresetIndex = 0;
    }

    private void SavePreset(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            EditorUtility.DisplayDialog("错误", "Preset 名称不能为空", "确定");
            return;
        }

        string dir = "Assets/Data/Presets";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
            AssetDatabase.Refresh();
        }

        string path = $"{dir}/{name}.asset";
        if (File.Exists(path))
        {
            if (!EditorUtility.DisplayDialog("覆盖确认", $"Preset '{name}' 已存在，是否覆盖？", "覆盖", "取消"))
            {
                return;
            }
        }

        var preset = ScriptableObject.CreateInstance<GPUParticlePreset>();
        var system = target as GPUParticleSystem;
        preset.CaptureFrom(system);

        AssetDatabase.CreateAsset(preset, path);
        AssetDatabase.SaveAssets();

        RefreshPresetList();
        EditorUtility.DisplayDialog("成功", $"Preset 已保存到: {path}", "确定");
    }

    private void LoadPreset(string path)
    {
        var preset = AssetDatabase.LoadAssetAtPath<GPUParticlePreset>(path);
        if (preset == null)
        {
            EditorUtility.DisplayDialog("错误", "无法加载 Preset", "确定");
            return;
        }

        var system = target as GPUParticleSystem;
        preset.ApplyTo(system);
        EditorUtility.SetDirty(target);
        serializedObject.Update();
        Debug.Log($"[GPUParticleSystem] 已加载 Preset: {preset.name}");
    }

    private void CreateBuiltinPreset(string name, System.Action<GPUParticlePreset> setup)
    {
        string dir = "Assets/Data/Presets";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
            AssetDatabase.Refresh();
        }

        string path = $"{dir}/{name}.asset";
        if (File.Exists(path))
        {
            if (!EditorUtility.DisplayDialog("覆盖确认", $"Preset '{name}' 已存在，是否覆盖？", "覆盖", "取消"))
            {
                return;
            }
            AssetDatabase.DeleteAsset(path);
        }

        var preset = ScriptableObject.CreateInstance<GPUParticlePreset>();
        setup(preset);

        AssetDatabase.CreateAsset(preset, path);
        AssetDatabase.SaveAssets();

        RefreshPresetList();
        EditorUtility.DisplayDialog("成功", $"内置 Preset '{name}' 已创建", "确定");
    }

    private void CreateExplosionSettings(GPUParticlePreset preset)
    {
        preset.particleCount = 50000;
        preset.particleLife = 1.5f;
        preset.emitRadius = 2f;
        preset.emitSpeed = 20f;
        preset.gravity = 5f;
        preset.damping = 0.05f;
        preset.windForce = Vector3.zero;
        preset.enableMouseInteraction = false;
        preset.interactionRadius = 3f;
        preset.interactionStrength = 50f;
        preset.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    private void CreateSnowSettings(GPUParticlePreset preset)
    {
        preset.particleCount = 100000;
        preset.particleLife = 10f;
        preset.emitRadius = 30f;
        preset.emitSpeed = 2f;
        preset.gravity = 2f;
        preset.damping = 0.02f;
        preset.windForce = new Vector3(2f, 0f, 1f);
        preset.enableMouseInteraction = false;
        preset.interactionRadius = 3f;
        preset.interactionStrength = 50f;
        preset.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    private void CreateMagicSettings(GPUParticlePreset preset)
    {
        preset.particleCount = 30000;
        preset.particleLife = 5f;
        preset.emitRadius = 5f;
        preset.emitSpeed = 3f;
        preset.gravity = 0.5f;
        preset.damping = 0.5f;
        preset.windForce = new Vector3(0f, 1f, 0f);
        preset.enableMouseInteraction = true;
        preset.interactionRadius = 5f;
        preset.interactionStrength = 30f;
        preset.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }
}
