/*
 * @Author: fasthro
 * @Date: 2019-06-24 12:11:05
 * @Description: 资源异步管理系统
 */
using System.Collections;
using System.Collections.Generic;

namespace RPGGame.ResSystem
{
    [MonoSingletonPath("ResLoader/RunAsyncSystem")]
    public class RunAsyncSystem : MonoSingleton<RunAsyncSystem>, IRunAsyncSystem
    {
        // 同事运行异步最大数
        private const int maxRunCount = 8;
        // 当前运行异步数量
        private int m_runCount;
        // 异步链表
        private LinkedList<IRunAsync> m_enumerators = new LinkedList<IRunAsync>();

        /// <summary>
        /// 添加异步
        /// </summary>
        public void Push(IRunAsync enumerator)
        {
            m_enumerators.AddLast(enumerator);
            TryRun();
        }

        /// <summary>
        /// 尝试运行异步
        /// </summary>
        private void TryRun()
        {
            if (m_enumerators.Count == 0) return;
            if (m_runCount >= maxRunCount) return;

            var enumerator = m_enumerators.First.Value;
            m_enumerators.RemoveFirst();

            ++m_runCount;
            StartCoroutine(enumerator.RunAsync(this));
        }

        /// <summary>
        /// 尝试运行下一个异步
        /// </summary>
        private void TryNextRun()
        {
            --m_runCount;
            TryRun();
        }

        #region IRunAsyncSystem
        public void OnRunAsyncComplete()
        {
            TryNextRun();
        }
        #endregion

        #region  API
        /// <summary>
        /// API-添加异步
        /// </summary>
        /// <param name="enumerator"></param>
        public static void PushEnumerator(IRunAsync enumerator)
        {
            Instance.Push(enumerator);
        }
        #endregion
    }
}