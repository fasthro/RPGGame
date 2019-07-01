/*
 * @Author: fasthro
 * @Date: 2019-05-17 14:28:56
 * @Description: 笔刷工具
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    public delegate void BrushToolsEvent(EventType eventType, Grid grid);

    public class BrushTools : AbstractSceneTools, ISceneTools
    {
        // 笔刷资源
        private ResObject m_resObject;

        // 笔刷格子
        private Grid m_grid;
        public Grid gird { get { return m_grid; } }

        // 锚点位置偏移
        private Vector3 m_anchorOffset;

        // 位置偏移
        private Vector3 m_positionOffset;
        public Vector3 positionOffset { set { m_positionOffset = value; } }

        // 角度偏移
        private Vector3 m_angleOffset;
        public Vector3 angleOffset { set { m_angleOffset = value; } }

        // 事件
        public BrushToolsEvent toolsEventHandler;

        protected override void OnInitialize()
        {
            m_anchorOffset = SettingManager.Inst.Setting.cubeAnchorOffset;
        }

        public void Bind(ResObject resObject)
        {
            if (m_resObject != null)
            {
                if (resObject.id == m_resObject.id) return;
            }

            m_resObject = resObject;

            if (m_grid != null) GameObject.DestroyImmediate(m_grid.gameObject);
            m_grid = null;
        }

        protected override void OnClose()
        {
            m_resObject = null;

            if (m_grid != null) GameObject.DestroyImmediate(m_grid.gameObject);
            m_grid = null;
        }

        public void OnSceneRender(SceneView sceneView, SceneRenderState state, Vector3 mousePosition)
        {
            if (!isActive) return;
            if (state == SceneRenderState.Exit) return;

            if (m_resObject == null)
            {
                DrawGizmoCube(mousePosition, Vector3.one, Color.red);
                return;
            }

            if (m_grid == null)
            {
                var go = GameObject.Instantiate(m_resObject.prefab) as GameObject;
                go.name = "brush-tools-gameobject";
                go.isStatic = true;
                go.hideFlags = HideFlags.HideAndDontSave;
                m_grid = go.AddComponent<Grid>();
                m_grid.resId = m_resObject.id;
            }

            m_grid.position = mousePosition;
            m_grid.positionOffset = m_positionOffset;
            m_grid.angle = Vector3.zero;
            m_grid.angleOffset = m_angleOffset;
            m_grid.scale = m_grid.transform.localScale;
            m_grid.scaleOffset = Vector3.zero;

            m_grid.transform.position = mousePosition + m_anchorOffset + m_positionOffset;
            m_grid.transform.localEulerAngles = m_angleOffset;

            if (Event.current.button == 0
                && Event.current.alt == false
                && Event.current.shift == false
                && Event.current.control == false
                && gird != null)
            {
                if (toolsEventHandler != null) toolsEventHandler(Event.current.type, m_grid);
            }

            SceneView.RepaintAll();
        }
    }
}
