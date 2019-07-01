using UnityEngine;
using DG.Tweening;
using FairyGUI;

namespace RPGGame
{
    public delegate void OnUpdateHandler();
    public delegate void OnLateUpdateHandler();

    public class MainGame : MonoBehaviour
    {
        public event OnUpdateHandler OnUpdate;
        public event OnLateUpdateHandler OnLateUpdate;

        void Awake()
        {
            InitGame();
        }

        private void InitGame()
        {
            DontDestroyOnLoad(gameObject);

            // 应用基本设置
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = App.targetFrameRate;
            QualitySettings.vSyncCount = App.vSyncCount;

            // 设置屏幕分辨率
            Screen.SetResolution(App.resolutionWidth, App.resolutionHeight, false);

            // dotween
            DOTween.Init(true, true, LogBehaviour.Default);

            // FairyGUI 设置
            GRoot.inst.SetContentScaleFactor(App.resolutionWidth, App.resolutionHeight, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);

            // 初始化场景
            SceneMgr.Instance.Init();
        }

        
        void Update()
        {
            if (OnUpdate != null)
            {
                OnUpdate();
            }
        }

        void LateUpdate()
        {
            if (OnUpdate != null)
            {
                OnLateUpdate();
            }
        }
    }
}

