/*
 * @Author: fasthro
 * @Date: 2019-06-19 20:45:33
 * @Description: 场景接口
 */
namespace RPGGame
{
    public interface IScene
    {
        SceneState sceneState { get;}
        void Init(SceneEvent eventCallback = null);
        void Load(SceneEvent eventCallback = null);
        void OnEnter(SceneEvent eventCallback = null);
        void OnExit(SceneEvent eventCallback = null);
        void OnUpdate();
    }
}