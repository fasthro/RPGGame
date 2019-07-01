/*
 * @Author: fasthro
 * @Date: 2019-06-19 17:39:23
 * @Description: 池接口
 */

namespace RPGGame
{
    public interface IPool<T>
    {
        T Allocate();

        bool Recycle(T obj);
    }
}