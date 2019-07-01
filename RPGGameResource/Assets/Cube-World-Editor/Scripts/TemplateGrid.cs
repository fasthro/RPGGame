/*
 * @Author: fasthro
 * @Date: 2019-05-17 14:28:57
 * @Description: 网格模版面板
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
#if UNITY_EDITOR
    public delegate void SceneRenderEvent(SceneView sceneView, SceneRenderState state, Vector3 mousePosition);

    public class TemplateGrid : MonoBehaviour
    {
        // 地块片的尺寸
        private float m_gridSize = 1;

        // 网格尺寸宽(X轴)
        private int m_width = 40;
        public int width { get { return m_width; } set { m_width = value; } }

        // 网格尺寸长(Z轴)
        private int m_lenght = 40;
        public int lenght { get { return m_lenght; } set { m_lenght = value; } }

        // 网格高度(Y轴)
        private int m_height = 0;
        public int height { get { return m_height; } set { m_height = value; } }

        // 是否以网格中心点为原点
        private bool m_centreGrid = true;

        // 背景是否透明
        public bool transparentEnabled = true;

        // 是否显示网格
        private bool m_isShowView;

        // 鼠标位置
        private Vector3 m_mousePosition;

        // 场景渲染状态
        private SceneRenderState m_sceneRenderState;
        // 场景渲染事件
        public SceneRenderEvent SceneRenderHandler;

        // color
        private Color m_normalColor = Color.white;
        private Color m_borderColor = Color.green;
        private Color m_fillColor = new Color(1, 0, 0, 0.5f);

        private float m_gridSizeOffset;
        private float gridWidthOffset;
        private float gridLengthOffset;
        private float gridOffset;

        private Vector3 m_gridMin;
        private Vector3 m_gridMax;

        private BoxCollider m_gridCollider;
        private Vector3 m_gridColliderCenter;
        private Vector3 m_gridColliderSize;

        public void ShowView()
        {
            m_isShowView = true;
            m_sceneRenderState = SceneRenderState.Exit;
        }

        public void CloseView()
        {
            m_isShowView = false;
        }

        public void OnSceneGUI(SceneView sceneView)
        {
            if (Event.current == null) return;

            Vector2 mp = new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y);
            Ray ray = HandleUtility.GUIPointToWorldRay(mp);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer(typeof(TemplateGrid).Name)) == true)
            {
                Vector3 shiftOffset = transform.position;
                shiftOffset.x = shiftOffset.x - (int)shiftOffset.x;
                shiftOffset.y = shiftOffset.y - (int)shiftOffset.y;
                shiftOffset.z = shiftOffset.z - (int)shiftOffset.z;

                m_mousePosition.x = Mathf.Round(((hit.point.x + shiftOffset.x) - hit.normal.x * 0.001f) / 1) * 1 - shiftOffset.x;
                m_mousePosition.z = Mathf.Round(((hit.point.z + shiftOffset.z) - hit.normal.z * 0.001f) / 1) * 1 - shiftOffset.z;
                m_mousePosition.y = height + transform.position.y;

                if (m_sceneRenderState == SceneRenderState.Exit)
                {
                    m_sceneRenderState = SceneRenderState.Enter;
                    if (SceneRenderHandler != null)
                    {
                        SceneRenderHandler(sceneView, m_sceneRenderState, m_mousePosition);
                    }
                }
                else
                {
                    if (SceneRenderHandler != null)
                    {
                        SceneRenderHandler(sceneView, SceneRenderState.Render, m_mousePosition);
                    }
                }
            }
            else
            {
                if (m_sceneRenderState == SceneRenderState.Enter)
                {
                    m_sceneRenderState = SceneRenderState.Exit;
                    if (SceneRenderHandler != null)
                    {
                        SceneRenderHandler(sceneView, m_sceneRenderState, m_mousePosition);
                    }
                }
            }
        }

        void OnDrawGizmos()
        {
            if (!m_isShowView)
                return;

            m_gridSizeOffset = m_gridSize / 2.0f;
            if (m_centreGrid)
            {
                gridWidthOffset = m_width * m_gridSize / 2;
                gridLengthOffset = m_lenght * m_gridSize / 2;
            }
            else
            {
                gridWidthOffset = 0;
                gridLengthOffset = 0;
            }

            m_gridMin.x = gameObject.transform.position.x - gridWidthOffset - m_gridSizeOffset;
            m_gridMin.y = gameObject.transform.position.y + m_height - m_gridSizeOffset - gridOffset;
            m_gridMin.z = gameObject.transform.position.z - gridLengthOffset - m_gridSizeOffset;
            m_gridMax.x = m_gridMin.x + (m_gridSize * m_width);
            m_gridMax.z = m_gridMin.z + (m_gridSize * m_lenght);
            m_gridMax.y = m_gridMin.y;

            DrawBase();
            DrawGrid();
            DrawBorder();

            SetCollider();
        }

        // 画背板
        private void DrawBase()
        {
            if (!transparentEnabled)
                return;

            if (m_centreGrid)
            {
                Gizmos.DrawCube(new Vector3(gameObject.transform.position.x - m_gridSizeOffset, gameObject.transform.position.y + m_height - m_gridSizeOffset - gridOffset, gameObject.transform.position.z - m_gridSizeOffset),
                    new Vector3((m_width * m_gridSize), 0.01f, (m_lenght * m_gridSize)));
            }
            else
            {
                Gizmos.DrawCube(new Vector3(gameObject.transform.position.x + (m_width / 2 * m_gridSize) - m_gridSizeOffset,
                    gameObject.transform.position.y + m_height - m_gridSizeOffset - gridOffset,
                    gameObject.transform.position.z + (m_lenght / 2 * m_gridSize) - m_gridSizeOffset),
                    new Vector3((m_width * m_gridSize), 0.01f, (m_lenght * m_gridSize)));
            }
        }

        // 画网格
        private void DrawGrid()
        {
            Gizmos.color = m_normalColor;

            if (m_gridSize != 0)
            {
                for (float i = m_gridSize; i < (m_width * m_gridSize); i += m_gridSize)
                {
                    Gizmos.DrawLine(
                        new Vector3((float)i + m_gridMin.x, m_gridMin.y, m_gridMin.z),
                        new Vector3((float)i + m_gridMin.x, m_gridMin.y, m_gridMax.z)
                        );
                }
            }

            if (m_gridSize != 0)
            {
                for (float j = m_gridSize; j < (m_lenght * m_gridSize); j += m_gridSize)
                {
                    Gizmos.DrawLine(
                        new Vector3(m_gridMin.x, m_gridMin.y, j + m_gridMin.z),
                        new Vector3(m_gridMax.x, m_gridMin.y, j + m_gridMin.z)
                        );
                }
            }
        }

        // 画边
        private void DrawBorder()
        {
            Gizmos.color = m_borderColor;

            // left side
            Gizmos.DrawLine(new Vector3(m_gridMin.x, m_gridMin.y, m_gridMin.z), new Vector3(m_gridMin.x, m_gridMin.y, m_gridMax.z));

            //bottom
            Gizmos.DrawLine(new Vector3(m_gridMin.x, m_gridMin.y, m_gridMin.z), new Vector3(m_gridMax.x, m_gridMin.y, m_gridMin.z));

            // Right side
            Gizmos.DrawLine(new Vector3(m_gridMax.x, m_gridMin.y, m_gridMin.z), new Vector3(m_gridMax.x, m_gridMin.y, m_gridMax.z));

            //top
            Gizmos.DrawLine(new Vector3(m_gridMin.x, m_gridMin.y, m_gridMax.z), new Vector3(m_gridMax.x, m_gridMin.y, m_gridMax.z));
        }

        /// <summary>
        /// 设置碰撞框
        /// </summary>
        private void SetCollider()
        {
            m_gridCollider = gameObject.GetComponent<BoxCollider>();
            if (m_gridCollider == null)
                m_gridCollider = gameObject.AddComponent<BoxCollider>();

            // center
            if (m_centreGrid)
            {
                m_gridColliderCenter.x = 0 - m_gridSizeOffset;
                m_gridColliderCenter.y = 0 + m_height - m_gridSizeOffset;
                m_gridColliderCenter.z = 0 - m_gridSizeOffset;
            }
            else
            {
                m_gridColliderCenter.x = 0 + m_width / 2 * m_gridSize - m_gridSizeOffset;
                m_gridColliderCenter.y = 0 + m_height - m_gridSizeOffset;
                m_gridColliderCenter.z = 0 + m_lenght / 2 * m_gridSize - m_gridSizeOffset;
            }

            m_gridCollider.center = m_gridColliderCenter;

            // size
            m_gridColliderSize.x = m_width;
            m_gridColliderSize.y = 0.1f;
            m_gridColliderSize.z = m_lenght;
            m_gridCollider.size = m_gridColliderSize;
        }
    }
    #endif
}
