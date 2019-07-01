/*
 * @Author: fasthro
 * @Date: 2019-01-10 11:11:21
 * @Description: 管理者接口
 */

namespace RPGGame
{
    public interface IManager
    {
        void Init();
        void Update();
        void FixedUpdate();
        void LateUpdate();
        void Dispose();
    }
}
