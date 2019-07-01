/*
 * @Author: fasthro
 * @Date: 2019-06-19 19:47:15
 * @Description: 池对象实现接口
 */

namespace RPGGame
{
    public interface IPoolable
    {
        void OnRecycled();
        bool isRecycled { get; set; }
    }
}