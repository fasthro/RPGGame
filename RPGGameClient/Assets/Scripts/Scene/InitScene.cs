/*
 * @Author: fasthro
 * @Date: 2019-01-10 11:11:21
 * @Description: 初始场景
 */

using UnityEngine;

namespace RPGGame
{
    public class InitScene : AbstractScene
    {
        public override void Init(SceneEvent eventCallback = null)
        {
            
        }

        public override void Load(SceneEvent eventCallback = null)
        {
            
        }

        public override void OnEnter(SceneEvent eventCallback = null)
        {
            // 热更新
            // 进入登录流程
            // SceneMgr.Instance.SwitchScene(SceneType.LoginScene);
            var env = new World();
            env.LoadMap("Main");
        }

        public override void OnExit(SceneEvent eventCallback = null)
        {
            
        }
    }
}
