/*
 * @Author: fasthro
 * @Date: 2019-05-27 16:19:42
 * @Description: 场景窗口基类
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeWorldEditor
{
    public class AbstractSceneWindow
    {
        protected string title;
        protected float x;
        protected float y;
        protected float w;
        protected float h;
        protected bool isShow;
        protected int windowId;

        public void Initialize()
        {
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {

        }

        protected virtual void OnGUI(int id)
        {

        }

        public void ShowWindow()
        {
            if (isShow) return;
            isShow = true;
            Initialize();
            OnShowWindow();
        }

        protected virtual void OnShowWindow()
        {

        }

        public void RestartWindow()
        {
            CloseWindow();
            ShowWindow();
            OnRestartWindow();
        }

        protected virtual void OnRestartWindow()
        {

        }

        public void CloseWindow()
        {
            if (!isShow) return;
            isShow = false;
            OnCloseWindow();
        }

        protected virtual void OnCloseWindow()
        {

        }
    }
}