/*
 * @Author: fasthro
 * @Date: 2019-05-28 12:03:31
 * @Description: 场景主窗口界面
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    // 切换工具事件
    public delegate void SwitchToolsEvent(SceneToolsType stt);

    public class SceneWindow : AbstractSceneWindow, ISceneWindow
    {
        // 格子模版
        private TemplateGrid m_templateGrid;
        public TemplateGrid templateGrid { get { return m_templateGrid; } }

        // 当前选择的工具类型
        private SceneToolsType m_sceneToolType;
        public SceneToolsType sceneToolsType { get { return m_sceneToolType; } }

        // 场景工具-选择器
        private SelectorTools m_selectorTools;
        public SelectorTools selectorTools { get { return m_selectorTools; } }

        // 场景工具-笔刷
        private BrushTools m_brushTools;
        public BrushTools brushTools { get { return m_brushTools; } }

        // 场景工具-吸管
        private SuckerTools m_suckerTools;
        public SuckerTools suckerTools { get { return m_suckerTools; } }

        // 场景工具-擦除
        private EraseTools m_eraseTools;
        public EraseTools eraseTools { get { return m_eraseTools; } }

        // 场景窗口-笔刷
        private BrushWindow m_brushWindow;
        public BrushWindow brushWindow { get { return m_brushWindow; } }

        // 场景窗口-选择器
        private SelectorWindow m_selecterWindow;
        public SelectorWindow selectorWindow { get { return m_selecterWindow; } }

        // 切换工具事件
        public SwitchToolsEvent switchToolsEventHandler;

        private readonly int m_toolsCount = 4;
        private readonly int m_operationCount = 5;

        // scene view 视角
        private int m_sceneViewQuaIndex = 0;
        private Quaternion[] m_sceneViewQuas = new Quaternion[] {
            Quaternion.LookRotation(Vector3.down),
            Quaternion.LookRotation(Vector3.left),
            Quaternion.LookRotation(Vector3.right),
            new Quaternion(0.35f, 0, 0, 1)};

        // rect
        private Rect m_rect;
        // controlId
        private int m_controlId;
        private GUILayoutOption m_layoutWidth;
        private GUILayoutOption m_layoutHeight;
        // window id
        private int m_toolsWindowId;
        private int m_operationWindowId;

        protected override void OnInitialize()
        {
            // 创建工具
            if (m_selectorTools == null) m_selectorTools = new SelectorTools();
            if (m_brushTools == null) m_brushTools = new BrushTools();
            if (m_suckerTools == null) m_suckerTools = new SuckerTools();
            if (m_eraseTools == null) m_eraseTools = new EraseTools();

            // 工具事件注册
            m_selectorTools.toolsEventHandler -= OnSelectorToolsEventHandler;
            m_selectorTools.toolsEventHandler += OnSelectorToolsEventHandler;

            m_brushTools.toolsEventHandler -= OnBrushToolsEventHandler;
            m_brushTools.toolsEventHandler += OnBrushToolsEventHandler;

            m_suckerTools.toolsEventHandler -= OnSuckerToolsEventHandler;
            m_suckerTools.toolsEventHandler += OnSuckerToolsEventHandler;

            m_eraseTools.toolsEventHandler -= OnEraseToolsEventHandler;
            m_eraseTools.toolsEventHandler += OnEraseToolsEventHandler;

            // 创建窗口
            if (m_brushWindow == null) m_brushWindow = new BrushWindow();
            if (m_selecterWindow == null) m_selecterWindow = new SelectorWindow();

            // 模版格子
            if (m_templateGrid == null) m_templateGrid = GameObject.Find(typeof(TemplateGrid).Name).gameObject.GetComponent<TemplateGrid>();
            // 注册场景事件
            m_templateGrid.SceneRenderHandler -= OnSceneRender;
            m_templateGrid.SceneRenderHandler += OnSceneRender;

            // window id
            m_toolsWindowId = EUI.GetWindowId();
            m_operationWindowId = EUI.GetWindowId();

            // GUILayoutOption
            m_layoutWidth = GUILayout.Width(SettingManager.Inst.Setting.sceneToolsIconSize);
            m_layoutHeight = GUILayout.Height(SettingManager.Inst.Setting.sceneToolsIconSize);

            // 场景视角设置
            m_sceneViewQuaIndex = 0;
        }

        protected override void OnShowWindow()
        {
            Selection.activeGameObject = null;

            // 设置网格模版
            templateGrid.width = CubeWorldEditorWindow.Inst.templateGridSize.x;
            templateGrid.lenght = CubeWorldEditorWindow.Inst.templateGridSize.y;
            templateGrid.ShowView();

            // 重置工具
            SwitchTools(SceneToolsType.None);

            // 场景视角设置
            SceneView.lastActiveSceneView.LookAt(Vector3.zero, m_sceneViewQuas[m_sceneViewQuaIndex]);
        }

        protected override void OnCloseWindow()
        {
            // 关闭网格模版
            templateGrid.CloseView();

            // 关闭工具事件
            m_selectorTools.Close();
            m_brushTools.Close();
            m_suckerTools.Close();
            m_eraseTools.Close();

            // 关闭场景窗口
            brushWindow.CloseWindow();
            selectorWindow.CloseWindow();
        }

        /// <summary>
        /// 切换场景工具
        /// </summary>
        public void SwitchTools(SceneToolsType stt)
        {
            if (stt == m_sceneToolType) return;

            m_sceneToolType = stt;

            if (stt != SceneToolsType.Selector)
            {
                selectorTools.Close();
                selectorWindow.CloseWindow();
            }
            if (stt != SceneToolsType.Brush)
            {
                brushTools.Close();
                brushWindow.CloseWindow();
            }
            if (stt != SceneToolsType.Sucker) suckerTools.Close();
            if (stt != SceneToolsType.Erase) eraseTools.Close();


            if (stt == SceneToolsType.Selector)
            {
                selectorTools.Open();
                selectorWindow.ShowWindow();
            }
            if (stt == SceneToolsType.Brush)
            {
                brushTools.Open();
                brushWindow.ShowWindow();
            }
            if (stt == SceneToolsType.Sucker) suckerTools.Open();
            if (stt == SceneToolsType.Erase) eraseTools.Open();

            if (switchToolsEventHandler != null) switchToolsEventHandler(stt);

            if (stt == SceneToolsType.None) Selection.activeGameObject = null;
        }

        public void OnSceneGUI(SceneView sceneView)
        {
            if (!isShow) return;

            // 网格模版更新
            templateGrid.OnSceneGUI(sceneView);

            m_controlId = GUIUtility.GetControlID(FocusType.Passive);

            Handles.BeginGUI();

            GUI.skin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Scene);

            GUI.Window(m_toolsWindowId, new Rect(0, 18, SettingManager.Inst.Setting.sceneToolsIconSize + 15, 25 + 5 * (m_toolsCount - 1) + SettingManager.Inst.Setting.sceneToolsIconSize * m_toolsCount), OnToolsGUI, "");
            GUI.Window(m_operationWindowId, new Rect(0, sceneView.position.height - 70, 11 + 5 * (m_operationCount - 1) + SettingManager.Inst.Setting.sceneToolsIconSize * m_operationCount, SettingManager.Inst.Setting.sceneToolsIconSize + 25), OnOperationGUI, "");

            // other window
            selectorWindow.OnSceneGUI(sceneView);
            brushWindow.OnSceneGUI(sceneView);

            Handles.EndGUI();

            HandleUtility.AddDefaultControl(m_controlId);
        }

        private void OnToolsGUI(int id)
        {
            using (new VerticalCenteredScope())
            {
                // 选择工具
                if (GUILayout.Toggle(m_sceneToolType == SceneToolsType.Selector, EUI.GetTextureContent("iconCursor"), GUI.skin.button, m_layoutHeight, m_layoutWidth))
                    SwitchTools(SceneToolsType.Selector);
                // 笔刷工具
                if (GUILayout.Toggle(m_sceneToolType == SceneToolsType.Brush, EUI.GetTextureContent("iconBlockMode"), GUI.skin.button, m_layoutHeight, m_layoutWidth))
                    SwitchTools(SceneToolsType.Brush);
                // 吸管工具
                if (GUILayout.Toggle(m_sceneToolType == SceneToolsType.Sucker, EUI.GetTextureContent("iconPicker"), GUI.skin.button, m_layoutHeight, m_layoutWidth))
                    SwitchTools(SceneToolsType.Sucker);
                // 擦除工具
                if (GUILayout.Toggle(m_sceneToolType == SceneToolsType.Erase, EUI.GetTextureContent("iconErase"), GUI.skin.button, m_layoutHeight, m_layoutWidth))
                    SwitchTools(SceneToolsType.Erase);
            }
        }

        private void OnOperationGUI(int id)
        {
            using (new HorizontalCenteredScope())
            {
                // Template Grid 背景
                templateGrid.transparentEnabled = GUILayout.Toggle(templateGrid.transparentEnabled, EUI.GetTextureContent("iconIsolate"), GUI.skin.button, m_layoutHeight, m_layoutWidth);

                // Template Grid 高度
                if (GUILayout.Toggle(false, EUI.GetTextureContent("iconGridUp"), GUI.skin.button, m_layoutHeight, m_layoutWidth))
                {
                    m_sceneToolType = SceneToolsType.None;
                    templateGrid.height++;
                    CubeWorldEditorWindow.Inst.Repaint();
                }

                // GizmoPanelDown
                if (GUILayout.Toggle(false, EUI.GetTextureContent("iconGridDown"), GUI.skin.button, m_layoutHeight, m_layoutWidth))
                {
                    m_sceneToolType = SceneToolsType.None;
                    templateGrid.height--;
                    CubeWorldEditorWindow.Inst.Repaint();
                }

                // camera view
                if (GUILayout.Button(EUI.GetTextureContent("iconEye"), GUI.skin.button, m_layoutHeight, m_layoutWidth))
                {
                    m_sceneViewQuaIndex++;
                    if (m_sceneViewQuaIndex >= m_sceneViewQuas.Length)
                        m_sceneViewQuaIndex = 0;
                    SceneView.lastActiveSceneView.LookAt(Vector3.zero, m_sceneViewQuas[m_sceneViewQuaIndex]);
                }

                // 截图
                if (GUILayout.Button(EUI.GetTextureContent("iconCapture"), GUI.skin.button, m_layoutHeight, m_layoutWidth))
                {
                    SwitchTools(SceneToolsType.None);

                    var path = Utils.GetPathToAssets(Utils.GetSceneDirectory(Environment.Inst.sceneName, false)) + "/ScreenShot/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                    Utils.CaptureScreenShot(path, SettingManager.Inst.Setting.screenshotOpenFinder);

                    EditorUtility.DisplayDialog("Capture ScreenShot", "Capture ScreenShot Succeed!", "ok");
                }
            }
        }

        private void OnSceneRender(SceneView sceneView, SceneRenderState state, Vector3 mousePosition)
        {
            switch (sceneToolsType)
            {
                case SceneToolsType.Selector:
                    selectorTools.OnSceneRender(sceneView, state, mousePosition);
                    selectorWindow.OnSceneGUI(sceneView);
                    break;
                case SceneToolsType.Brush:
                    brushTools.OnSceneRender(sceneView, state, mousePosition);
                    brushWindow.OnSceneGUI(sceneView);
                    break;
                case SceneToolsType.Sucker:
                    suckerTools.OnSceneRender(sceneView, state, mousePosition);
                    break;
                case SceneToolsType.Erase:
                    eraseTools.OnSceneRender(sceneView, state, mousePosition);
                    break;
            }
        }

        // 选择器工具事件
        private void OnSelectorToolsEventHandler(Grid grid)
        {
            if (grid != null)
            {
                Selection.activeGameObject = grid.gameObject;
                selectorWindow.RestartWindow();
            }
        }

        // 笔刷工具事件
        private void OnBrushToolsEventHandler(EventType eventType, Grid grid)
        {
            if (Event.current.button == 0
            && Event.current.alt == false
            && Event.current.shift == false
            && Event.current.control == false
            && grid != null)
            {
                if (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown)
                {
                    Environment.Inst.DrawGrid(grid, CubeWorldEditorWindow.Inst.area);
                }
            }
        }

        // 吸管工具事件
        private void OnSuckerToolsEventHandler(Vector3 mousePosition)
        {
            var grid = Environment.Inst.GetGrid(mousePosition, CubeWorldEditorWindow.Inst.area);
            if (grid != null)
            {
                CubeWorldEditorWindow.Inst.SetViewSelected(ResManager.Inst.GetResObject(grid.resId));
                SwitchTools(SceneToolsType.Brush);
            }
        }

        // 橡皮擦工具事件
        private void OnEraseToolsEventHandler(Vector3 mousePosition)
        {
            Environment.Inst.EraseGrid(mousePosition, CubeWorldEditorWindow.Inst.area);
        }
    }
}

