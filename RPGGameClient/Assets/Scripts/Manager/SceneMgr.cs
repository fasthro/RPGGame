/*
 * @Author: fasthro
 * @Date: 2019-01-10 11:11:21
 * @Description: 场景管理
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPGGame
{
    public class SceneMgr : Singleton<SceneMgr>, IManager
    {
        private AbstractScene[] m_scenes;

        private int m_sceneType;
        public int sceneType { get { return m_sceneType; } }

        private int m_beforeSceneType;

        public AbstractScene curScene { get { return m_scenes[m_sceneType]; } }
        public AbstractScene beforScene { get { return m_scenes[m_beforeSceneType]; } }
        public AbstractScene loaderScene { get { return m_scenes[SceneType.LoaderScene]; } }

        private SceneMgr() { }

        #region  接口实现

        public void Init()
        {
            m_scenes = new AbstractScene[] {
                new InitScene(),
                new LoaderScene(),
                new LoginScene(),
                new MainScene(),
                new BattleScene()};

            // 初始为初始化场景，默认进入
            m_beforeSceneType = SceneType.InitScene;
            m_sceneType = SceneType.InitScene;
            
            curScene.OnEnter();
        }

        public void Update()
        {
            if (curScene.sceneState == SceneState.Active)
                curScene.OnUpdate();
        }

        public void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void LateUpdate()
        {
            throw new System.NotImplementedException();
        }

        #endregion


        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="sceneType"></param>
        public void SwitchScene(int sceneType)
        {
            if (m_beforeSceneType == sceneType)
                return;

            m_beforeSceneType = m_sceneType;
            m_sceneType = sceneType;

            Game.mainGame.StartCoroutine(Load());
        }

        IEnumerator Load()
        {
            beforScene.OnExit();
            yield return new WaitForEndOfFrame();

            loaderScene.Load();

            AsyncOperation op = SceneManager.LoadSceneAsync(SceneType.LoaderScene);
            yield return op;
            yield return new WaitForSeconds(0.1f);

            loaderScene.OnEnter(LoaderEnterEvent);
        }

        private void LoaderEnterEvent()
        {
            Game.mainGame.StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            curScene.Load();

            int displayProgress = 0;
            int toProgress = 0;

            AsyncOperation op = SceneManager.LoadSceneAsync(m_sceneType);
            op.allowSceneActivation = false;

            while (op.progress < 0.9f)
            {
                toProgress = (int)op.progress * 100;
                while (displayProgress < toProgress)
                {
                    ++displayProgress;
                    yield return new WaitForEndOfFrame();
                }
            }

            toProgress = 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                yield return new WaitForEndOfFrame();
            }
            op.allowSceneActivation = true;

            yield return new WaitForEndOfFrame();

            loaderScene.OnExit();
            curScene.OnEnter();
        }
    }
}

