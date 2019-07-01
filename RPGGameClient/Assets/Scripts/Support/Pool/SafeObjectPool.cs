/*
 * @Author: fasthro
 * @Date: 2019-06-19 17:39:24
 * @Description: 更安全的对象池
 */
using System;

namespace RPGGame
{
    public class SafeObjectPool<T> : Pool<T>, ISingleton where T : IPoolable, new()
    {
        #region Singleton
        void ISingleton.OnSingletonInit() { }

        protected SafeObjectPool()
        {
            m_factory = new DefaultObjectFactory<T>();
        }

        public static SafeObjectPool<T> Instance
        {
            get { return SingletonProperty<SafeObjectPool<T>>.Instance; }
        }

        public void Dispose()
        {
            SingletonProperty<SafeObjectPool<T>>.Dispose();
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="maxCount"></param>
        /// <param name="initCount"></param>
        public void Init(int maxCount, int initCount)
        {
            this.maxCount = maxCount;

            if (maxCount > 0)
            {
                initCount = Math.Min(maxCount, initCount);
            }

            if (count < initCount)
            {
                for (var i = count; i < initCount; ++i)
                {
                    Recycle(new T());
                }
            }
        }

        // 池最大数量
        public int maxCount
        {
            get { return m_maxCount; }
            set
            {
                m_maxCount = value;

                if (m_cacheStack != null)
                {
                    if (m_maxCount > 0)
                    {
                        if (m_maxCount < m_cacheStack.Count)
                        {
                            int removeCount = m_cacheStack.Count - m_maxCount;
                            while (removeCount > 0)
                            {
                                m_cacheStack.Pop();
                                --removeCount;
                            }
                        }
                    }
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