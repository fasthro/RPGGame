/*
 * @Author: fasthro
 * @Date: 2019-05-18 10:56:35
 * @Description: 设置参数
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeWorldEditor
{
    [System.Serializable]
    public class SettingSTO : ScriptableObject
    {
        [Header("编辑器宽")]
        public int EditorWidth = 450;

        [Header("关卡场景保存路径")]
        public string artPath = "Assets/Cube-World-Editor-Art/Levels/Prefabs/";

        [Header("关卡场景保存路径")]
        public string sceneSavePath = "Cube-World-Editor-Sample/Scenes/";

        [Header("cube offset")]
        public Vector3 cubeAnchorOffset = new Vector3(0, -0.5f, 0);

        [Header("场景工具Icon尺寸")]
        public int sceneToolsIconSize = 40;

        [Header("屏幕截图尺寸宽")]
        public int screenshotWidth = 1920;
        [Header("屏幕截图尺寸高")]
        public int screenshotHeight = 1080;
        [Header("屏幕截图分辨率")]
        public int supersizeResolution = 1;
        [Header("屏幕截图默认打开文件夹")]
        public bool screenshotOpenFinder = true;

    }

}
