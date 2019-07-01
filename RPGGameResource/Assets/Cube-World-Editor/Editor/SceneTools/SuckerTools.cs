/*
 * @Author: fasthro
 * @Date: 2019-05-17 14:28:56
 * @Description: 吸管工具
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    public delegate void SuckerToolsEvent(Vector3 mousePosition);

    public class SuckerTools : AbstractSceneTools, ISceneTools
    {

        // 事件
        public SuckerToolsEvent toolsEventHandler;

        protected override void OnClose()
        {
            toolsEventHandler = null;
        }

        public void OnSceneRender(SceneView sceneView, SceneRenderState state, Vector3 mousePosition)
        {
            if (!isActive) return;
            if (state == SceneRenderState.Exit) return;

            DrawGizmoCube(mousePosition, Vector3.one, Color.green);

            if ((Event.current.type == EventType.MouseDown)
               && Event.current.button == 0
               && Event.current.alt == false
               && Event.current.shift == false
               && Event.current.control == false)
            {
                if (toolsEventHandler != null) toolsEventHandler(mousePosition);
            }
        }
    }
}

