/*
 * @Author: fasthro
 * @Date: 2019-05-30 11:43:35
 * @Description: GUI封装
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GUI;

namespace CubeWorldEditor
{
    public class EUI
    {
        #region content
        private static GUIContent content;

        public static GUIContent GetTextContent(string text)
        {
            content = new GUIContent(text);
            return content;
        }

        public static GUIContent GetTextContent(string text, string tooltip)
        {
            content = new GUIContent(text, tooltip);
            return content;
        }

        public static GUIContent GetTextureContent(string iconName)
        {
            content = new GUIContent(ResManager.Inst.GetIconTexture(iconName));
            return content;
        }

        public static GUIContent GetTextureContent(string iconName, string tooltip)
        {
            content = new GUIContent(ResManager.Inst.GetIconTexture(iconName), tooltip);
            return content;
        }

        #endregion

        #region window
        private static int windowId = 0;
        public static int GetWindowId()
        {
            windowId++;
            return windowId;
        }
        #endregion
    }
}
