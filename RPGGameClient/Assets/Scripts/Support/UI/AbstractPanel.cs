/*
 * @Author: fasthro
 * @Date: 2019-06-20 14:44:28
 * @Description: 面板基类
 */
using System;
using FairyGUI;

namespace RPGGame
{
    public abstract class AbstractPanel : Window, IPanel
    {
        // 面板id
        protected int m_panelId;
        public int panelId { get { return m_panelId; } }

        // 面板所在层
        protected int m_layer;
        public int layer { get { return m_layer; } }

        // 面板状态
        protected bool m_isShow;
        public bool isShow { get { return m_isShow; } }

        // 是否启动Update
        protected bool m_isUpdate = false;
        public bool isUpdate { get { return m_isUpdate; } }

        // 是否启动FixedUpdate
        protected bool m_isFixedUpdate = false;
        public bool isFixedUpdate { get { return m_isFixedUpdate; } }

        // 是否启动LateUpdate
        protected bool m_isLateUpdate = false;
        public bool isLateUpdate { get { return m_isLateUpdate; } }

        // 正在加载
        private bool m_isLoading;
        // 加载完成
        private bool m_isReady;
        // 载完成之后直接打开UI
        private bool m_isLoadedOpen;

        // 组件所在包
        protected string m_mainPackage;
        // 依赖包列表
        protected string[] m_dependPackages;
        // 组件名称
        protected string m_mainComName;

        #region IPanel 接口
        public virtual void Preload()
        {
            m_isLoadedOpen = false;
            if (!m_isReady && !m_isLoading) LoadPackage();
        }

        public virtual void OpenPanel(IPanelData data)
        {
            if (!m_isReady)
            {
                Preload();
                m_isLoadedOpen = true;
            }
            else
            {
                LoadPackageComplete();
            }
        }

        public virtual void ShowPanel()
        {
            Show();
        }

        public virtual void HidePanel()
        {
            Hide();
        }

        public virtual void ClosePanel()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void LateUpdate()
        {

        }

        #endregion

        #region FGUI

        protected override void OnInit()
        {
            MakeFullScreen();
            contentPane = UIPackage.CreateObject(m_mainPackage, m_mainComName).asCom;
            contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
            gameObjectName = "panel_window_" + m_panelId.ToString();
        }

        protected override void OnShown()
        {
            m_isShow = true;
        }

        protected override void OnHide()
        {
            m_isShow = false;
        }

        #endregion

        #region 加载包

        /// <summary>
		/// 加载包
		/// </summary>
		/// <param name="index"></param>
        private void LoadPackage(int index = -1)
        {
            // 加载主包
            if (index == -1)
            {
                UIMgr.Instance.AddPackage(m_mainPackage, (package) =>
                {
                    index++;
                    LoadPackage(index);
                });
                return;
            }
            // 加载依赖包
            if (m_dependPackages.Length > 0 && index < m_dependPackages.Length)
            {
                UIMgr.Instance.AddPackage(m_dependPackages[index], (package) =>
                {
                    index++;
                    LoadPackage(index);
                });
                return;
            }
            
            LoadPackageComplete();
        }

        /// <summary>
		/// 加载包完成
		/// </summary>
        private void LoadPackageComplete()
        {
            // 加载完成
            if (m_isLoadedOpen)
            {
                Init();
                ShowPanel();
            }
        }
        #endregion
    }
}