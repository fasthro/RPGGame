/*
 * @Author: fasthro
 * @Date: 2019-05-17 14:28:56
 * @Description: 选择器工具
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    public delegate void SelectorToolsEvent(Grid grid);

    public class SelectorTools : AbstractSceneTools, ISceneTools
    {
        // 选择器状态
        private SelectorToolsState m_selectState;

        // Grid
        private Grid m_selectGrid;
        public Grid selectGrid { get { return m_selectGrid; } }

        // 事件
        public SelectorToolsEvent toolsEventHandler;

        protected override void OnInitialize()
        {
            m_selectGrid = null;
            m_selectState = SelectorToolsState.Unselected;
        }

        protected override void OnOpen()
        {
            Selection.activeGameObject = null;
        }

        protected override void OnClose()
        {
            Selection.activeGameObject = null;
            m_selectGrid = null;
            toolsEventHandler = null;
        }

        public void OnSceneRender(SceneView sceneView, SceneRenderState state, Vector3 mousePosition)
        {
            if (!isActive) return;
            if (state == SceneRenderState.Exit) return;

            // 选择提示框
            if (m_selectState == SelectorToolsState.Selected)
            {
                var label = string.Format("Position  : {0}\nRotation : {1}", m_selectGrid.transform.position.ToString(), m_selectGrid.transform.localEulerAngles.ToString());
                DrawGizmoPosition(m_selectGrid.transform.position + Vector3.up, m_selectGrid.transform.rotation, label);
            }
            else
            {
                DrawGizmoCube(mousePosition, Vector3.one, Color.yellow);
            }

            // 选择状态
            if ((Event.current.type == EventType.MouseDown)
                && Event.current.button == 0
                 && Event.current.alt == false
                 && Event.current.shift == false
                 && Event.current.control == false)
            {
                if (m_selectGrid != null)
                    Selection.activeGameObject = null;
                
                m_selectGrid = Environment.Inst.GetGrid(mousePosition, CubeWorldEditorWindow.Inst.area);
                m_selectState = m_selectGrid != null ? SelectorToolsState.Selected : SelectorToolsState.Unselected;
                if (toolsEventHandler != null) toolsEventHandler(m_selectGrid);
            }
        }
    }
}
