/*
 * @Author: fasthro
 * @Date: 2019-06-19 19:31:56
 * @Description: 事件监听对象
 */
using System.Collections.Generic;

namespace RPGGame
{
    public class EventListener
    {
        // 事件委托列表
        private LinkedList<OnEvent> m_events;

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        public bool Fire(int key, params object[] param)
        {
            if (m_events == null)
                return false;

            var next = m_events.First;
            OnEvent call = null;
            LinkedListNode<OnEvent> nextCache = null;

            while (next != null)
            {
                call = next.Value;
                nextCache = next.Next;
                call(key, param);

                next = next.Next ?? nextCache;
            }

            return true;
        }

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="listener"></param>
        public bool Add(OnEvent listener)
        {
            if (m_events == null)
                m_events = new LinkedList<OnEvent>();

            if (m_events.Contains(listener))
                return false;

            m_events.AddLast(listener);
            return true;
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="listener"></param>
        public void Remove(OnEvent listener)
        {
            if (m_events == null)
                return;

            m_events.Remove(listener);
        }

        /// <summary>
        /// 移除全部监听
        /// </summary>
        public void RemoveAll()
        {
            if (m_events == null)
                return;
            m_events.Clear();
        }
    }
}
