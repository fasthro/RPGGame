/*
 * @Author: fasthro
 * @Date: 2019-05-28 18:22:59
 * @Description: 场景工具接口
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    public interface ISceneTools
    {
        void OnSceneRender(SceneView sceneView, SceneRenderState state, Vector3 mousePosition);
    }
}
