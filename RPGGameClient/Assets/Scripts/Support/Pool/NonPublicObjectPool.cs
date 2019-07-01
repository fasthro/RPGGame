/*
 * @Author: fasthro
 * @Date: 2019-06-19 17:39:23
 * @Description: 无公共构造函数对象池
 */
using System;

namespace RPGGame
{
    public class NonPublicObjectPool<T> : Pool<T>, ISingleton where T : class, IPoolable
    {
        #region Singleton
        public void OnSingletonInit() { }

        public static NonPublicObjectPool<T> Instance
        {
            get { return SingletonProperty<NonPublicObjectPool<T>>.Instance; }
        }

        protected NonPublicObjectPool()
        {
            m_factory = new NonPublicObjectFactory<T>();
        }

        public void Dispose()
        {
            SingletonProperty<NonPublicObjectPool<T>>.Dispose();
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="maxCount"></param>
        /// <param name="initCount"></param>
        public void Init(int maxCount, int initCount)
        {
            if (maxCount > 0)
                initCount = Math.Min(maxCount, initCount);

            if (count >= initCount) return;

            for (var i = count; i < initCount; ++i)
                Recycle(m_factory.Create());
        }

        public int MaxCacheCount
        {
            get { return m_maxCount; }
            set
            {
                m_maxCount = value;

                if (m_cacheStack == null) return;
                if (m_maxCount <= 0) return;
                if (m_maxCount >= m_cacheStack.Count) return;
                var removeCount = m_maxCount - m_cacheStack.Count;
                while (removeCount > 0)
                {
                    m_cacheStack.Pop();
                    --removeCount;
                }
            }
        }

        public override T Allocate()
        {
            var result = base.Allocate();
            result.isRecycled = false;
            return result;
        }

        public override bool Recycle(T t)
        {
            if (t == null || t.isRecycled)
                return false;

            if (m_maxCount > 0)
            {
                if (m_cacheStack.Count >= m_maxCount)
                {
                    t.OnRecycled();
                    return false;
                }
            }

            t.isRecycled = true;
            t.OnRecycled();
            m_cacheStack.Push(t);

            return true;
        }
    }
}