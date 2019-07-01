/*
 * @Author: fasthro
 * @Date: 2019-01-10 11:11:21
 * @Description: 加载场景
 */

using System;

namespace RPGGame
{
    public class LoaderScene : AbstractScene
    {
        public override void Init(SceneEvent eventCallback = null)
        {

        }

        public override void Load(SceneEvent eventCallback = null)
        {
            
        }

        public override void OnEnter(SceneEvent eventCallback = null)
        {
           eventCallback.InvokeGracefully();
        }

        public override void OnExit(SceneEvent eventCallback = null)
        {
            
        }
    }
}


