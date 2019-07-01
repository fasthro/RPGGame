/*
 * @Author: fasthro
 * @Date: 2019-05-17 14:28:56
 * @Description: 场景工具基类
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    public abstract class AbstractSceneTools
    {
        // 激活状态
        protected bool isActive;

        public void Initialize()
        {
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {

        }

        public void Open()
        {
            if (isActive) return;
            isActive = true;
            Initialize();
            OnOpen();
        }

        protected virtual void OnOpen()
        {

        }

        public void Close()
        {
            if (!isActive) return;
            isActive = false;
            OnClose();
        }

        protected virtual void OnClose()
        {

        }

        /// <summary>
        /// 画格子
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="gizmoColor"></param>
        protected void DrawGizmoCube(Vector3 position, Vector3 size, Color gizmoColor)
        {
            Handles.color = gizmoColor;

            var full = size * 1.0f;
            var half = full / 2;
            var scale = 0.5f;

            // draw front
            Handles.DrawLine(position + new Vector3(-half.x, -scale, half.z), position + new Vector3(half.x, -scale, half.z));
            Handles.DrawLine(position + new Vector3(-half.x, -scale, half.z), position + new Vector3(-half.x, full.y - scale, half.z));
            Handles.DrawLine(position + new Vector3(half.x, full.y - scale, half.z), position + new Vector3(half.x, -scale, half.z));
            Handles.DrawLine(position + new Vector3(half.x, full.y - scale, half.z), position + new Vector3(-half.x, full.y - scale, half.z));

            // draw back
            Handles.DrawLine(position + new Vector3(-half.x, -scale, -half.z), position + new Vector3(half.x, -scale, -half.z));
            Handles.DrawLine(position + new Vector3(-half.x, -scale, -half.z), position + new Vector3(-half.x, full.y - scale, -half.z));
            Handles.DrawLine(position + new Vector3(half.x, full.y - scale, -half.z), position + new Vector3(half.x, -scale, -half.z));
            Handles.DrawLine(position + new Vector3(half.x, full.y - scale, -half.z), position + new Vector3(-half.x, full.y - scale, -half.z));

            // draw corners
            Handles.DrawLine(position + new Vector3(-half.x, -scale, -half.z), position + new Vector3(-half.x, -scale, half.z));
            Handles.DrawLine(position + new Vector3(half.x, -scale, -half.z), position + new Vector3(half.x, -scale, half.z));
            Handles.DrawLine(position + new Vector3(-half.x, full.y - scale, -half.z), position + new Vector3(-half.x, full.y - scale, half.z));
            Handles.DrawLine(position + new Vector3(half.x, full.y - scale, -half.z), position + new Vector3(half.x, full.y - scale, half.z));

            SceneView.RepaintAll();
        }

        /// <summary>
        /// 画文本内容
        /// </summary>
        /// <param name="position"></param>
        /// <param name="quaternion"></param>
        /// <param name="label"></param>
        protected void DrawGizmoPosition(Vector3 position, Quaternion quaternion, string label)
        {
            // Handles.PositionHandle(position, quaternion);

            if (!string.IsNullOrEmpty(label))
                Handles.Label(position, label);

            SceneView.RepaintAll();
        }
    }
}
