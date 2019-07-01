/*
 * @Author: fasthro
 * @Date: 2019-05-27 17:37:12
 * @Description: 选择器窗口
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    public class SelectorWindow : AbstractSceneWindow, ISceneWindow
    {
        // 位置偏移
        private Vector3 m_positionOffset;
        // 角度偏移
        private Vector3 m_angleOffset;
        // 缩放偏移
        private Vector3 m_scaleOffset;

        // 选择器工具
        private SelectorTools m_selectorTools;

        private int m_positionSelectedSnapping;
        private float m_positionSnapValue;
        private readonly string[] m_positionOptions = { "Custom", "0.25", "0.5", "1.0" };
        private readonly float[] m_positionOptionValues = { 0.0f, 0.25f, 0.5f, 1.0f };

        private int m_scaleSelectedSnapping;
        private float m_scaleSnapValue;
        private readonly string[] m_scaleOptions = { "Custom", "1", "2", "5" };
        private readonly float[] m_scaleOptionValues = { 0f, 1f, 2f, 5f };


        private readonly Color backgroundRed = new Color(1.0f, 0.467f, 0.465f);
        private readonly Color backgroundGreen = new Color(0.467f, 1.0f, 0.514f);
        private readonly Color backgroundBlue = new Color(0.467f, 0.67f, 1.0f);



        protected override void OnInitialize()
        {
            title = "Selecter Setting";
            w = 330;
            h = 225;
            windowId = EUI.GetWindowId();

            m_positionSelectedSnapping = 1;
            m_scaleSelectedSnapping = 1;

            m_selectorTools = CubeWorldEditorWindow.Inst.sceneWindow.selectorTools;
            if (m_selectorTools.selectGrid != null)
            {
                m_positionOffset = m_selectorTools.selectGrid.positionOffset;
                m_angleOffset = m_selectorTools.selectGrid.angleOffset;
                m_scaleOffset = m_selectorTools.selectGrid.scaleOffset;
            }
        }

        public void OnSceneGUI(SceneView sceneView)
        {
            if (!isShow) return;
            if (m_selectorTools.selectGrid == null) return;

            x = sceneView.position.width - w - 2;
            y = sceneView.position.height - h - 5;
            GUI.Window(windowId, new Rect(x, y, w, h), OnGUI, title);
        }

        protected override void OnGUI(int id)
        {
            GUILayout.Label("-Rotation");
            using (new HorizontalCenteredScope())
            {
                var originalColour = GUI.backgroundColor;
                GUI.backgroundColor = backgroundRed;
                if (GUILayout.Button(EUI.GetTextureContent("iconX90"), GUILayout.Width(50), GUILayout.Height(50)))
                    m_angleOffset.x += 90;
                if (GUILayout.Button(EUI.GetTextureContent("iconX180"), GUILayout.Width(50), GUILayout.Height(50)))
                    m_angleOffset.x += 180;

                GUI.backgroundColor = backgroundGreen;
                if (GUILayout.Button(EUI.GetTextureContent("iconY90"), GUILayout.Width(50), GUILayout.Height(50)))
                    m_angleOffset.y += 90;
                if (GUILayout.Button(EUI.GetTextureContent("iconY180"), GUILayout.Width(50), GUILayout.Height(50)))
                    m_angleOffset.y += 180;


                GUI.backgroundColor = backgroundBlue;
                if (GUILayout.Button(EUI.GetTextureContent("iconZ90"), GUILayout.Width(50), GUILayout.Height(50)))
                    m_angleOffset.z += 90;
                if (GUILayout.Button(EUI.GetTextureContent("iconZ180"), GUILayout.Width(50), GUILayout.Height(50)))
                    m_angleOffset.z += 180;

                GUI.backgroundColor = originalColour;
            }

            GUILayout.Label("-Position");
            m_positionSelectedSnapping = GUILayout.SelectionGrid(m_positionSelectedSnapping, m_positionOptions, m_positionOptions.Length, EditorStyles.miniButton);
            if (m_positionSelectedSnapping < m_positionOptionValues.Length && m_positionSelectedSnapping > 0)
            {
                m_positionSnapValue = m_positionOptionValues[m_positionSelectedSnapping];
            }
            else
            {
                m_positionSnapValue = EditorGUILayout.FloatField("Custom Value", m_positionSnapValue);
            }

            using (new HorizontalCenteredScope())
            {
                var originalColour = GUI.backgroundColor;
                GUI.backgroundColor = backgroundRed; ;

                if (GUILayout.Button(EUI.GetTextContent("+X"), GUILayout.Width(50)))
                    m_positionOffset.x += m_positionSnapValue;

                if (GUILayout.Button(EUI.GetTextContent("-X"), GUILayout.Width(50)))
                    m_positionOffset.x -= m_positionSnapValue;

                GUI.backgroundColor = backgroundGreen;
                if (GUILayout.Button(EUI.GetTextContent("+Y"), GUILayout.Width(50)))
                    m_positionOffset.y += m_positionSnapValue;
                if (GUILayout.Button(EUI.GetTextContent("-Y"), GUILayout.Width(50)))
                    m_positionOffset.y -= m_positionSnapValue;

                GUI.backgroundColor = backgroundBlue;
                if (GUILayout.Button(EUI.GetTextContent("+Z"), GUILayout.Width(50)))
                    m_positionOffset.z += m_positionSnapValue;
                if (GUILayout.Button(EUI.GetTextContent("-Z"), GUILayout.Width(50)))
                    m_positionOffset.z -= m_positionSnapValue;

                GUI.backgroundColor = originalColour;
            }

            GUILayout.Label("-Scale");
            m_scaleSelectedSnapping = GUILayout.SelectionGrid(m_scaleSelectedSnapping, m_scaleOptions, m_scaleOptions.Length, EditorStyles.miniButton);
            if (m_scaleSelectedSnapping < m_scaleOptionValues.Length && m_scaleSelectedSnapping > 0)
            {
                m_scaleSnapValue = m_scaleOptionValues[m_scaleSelectedSnapping];
            }
            else
            {
                m_scaleSnapValue = EditorGUILayout.FloatField("Custom Value", m_scaleSnapValue);
            }

            using (new HorizontalCenteredScope())
            {
                var originalColour = GUI.backgroundColor;
                GUI.backgroundColor = backgroundRed; ;

                if (GUILayout.Button(EUI.GetTextContent("X"), GUILayout.Width(103)))
                    m_scaleOffset.x = m_scaleSnapValue;

                GUI.backgroundColor = backgroundGreen;
                if (GUILayout.Button(EUI.GetTextContent("Y"), GUILayout.Width(103)))
                    m_scaleOffset.y = m_scaleSnapValue;

                GUI.backgroundColor = backgroundBlue;
                if (GUILayout.Button(EUI.GetTextContent("Z"), GUILayout.Width(103)))
                    m_scaleOffset.z = m_scaleSnapValue;

                GUI.backgroundColor = originalColour;
            }

            // 设置笔刷
            m_selectorTools.selectGrid.positionOffset = m_positionOffset;
            m_selectorTools.selectGrid.angleOffset = m_angleOffset;
            m_selectorTools.selectGrid.scaleOffset = m_scaleOffset;

            m_selectorTools.selectGrid.Refresh();
        }
    }
}
