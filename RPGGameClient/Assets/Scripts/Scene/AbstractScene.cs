/*
 * @Author: fasthro
 * @Date: 2019-06-20 10:41:19
 * @Description: 场景基类
 */

namespace RPGGame
{
    // 场景事件
    public delegate void SceneEvent();
    public abstract class AbstractScene : IScene
    {
        protected SceneState m_sceneState;
        public SceneState sceneState { get { return m_sceneState; } }

        public AbstractScene()
        {
            Init();
        }

        public virtual void Init(SceneEvent eventCallback = null)
        {
            m_sceneState = SceneState.Inactive;
        }
        public virtual void Load(SceneEvent eventCallback = null)
        {
            m_sceneState = SceneState.Loading;
        }
        public virtual void OnEnter(SceneEvent eventCallback = null)
        {
            m_sceneState = SceneState.Active;
        }
        public virtual void OnExit(SceneEvent eventCallback = null)
        {
            m_sceneState = SceneState.Inactive;
        }

        public virtual void OnUpdate() { }
    }
}