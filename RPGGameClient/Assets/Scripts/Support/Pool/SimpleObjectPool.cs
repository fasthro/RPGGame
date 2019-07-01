/*
 * @Author: fasthro
 * @Date: 2019-06-19 17:39:24
 * @Description: 
 */
using System;

namespace RPGGame
{
    public class SimpleObjectPool<T> : Pool<T>
    {
        readonly Action<T> mResetMethod;

        public SimpleObjectPool(Func<T> factoryMethod, Action<T> resetMethod = null, int initCount = 0)
        {
            m_factory = new CustomObjectFactory<T>(factoryMethod);
            mResetMethod = resetMethod;

            for (int i = 0; i < initCount; i++)
            {
                m_cacheStack.Push(m_factory.Create());
            }
        }

        public override bool Recycle(T obj)
        {
            mResetMethod.InvokeGracefully(obj);
            m_cacheStack.Push(obj);
            return true;
        }
    }
}