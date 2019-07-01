/*
 * @Author: fasthro
 * @Date: 2019-05-27 14:51:33
 * @Description: 场景窗口接口
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    public interface ISceneWindow
    {
        void OnSceneGUI(SceneView sceneView);
    }
}