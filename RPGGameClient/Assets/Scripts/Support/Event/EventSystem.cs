/*
 * @Author: fasthro
 * @Date: 2019-06-19 17:39:38
 * @Description: 事件系统
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{
    // 事件委托
    public delegate void OnEvent(int key, params object[] param);

    public class EventSystem : Singleton<EventSystem>, IPoolable
    {
        // 事件字典
        private readonly Dictionary<int, EventListener> m_eventListenerMap = new Dictionary<int, EventListener>(50);

        public bool isRecycled { get; set; }

        private EventSystem() { }

        #region 功能函数

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eventCallback"></param>
        public bool Register<T>(T key, OnEvent eventCallback) where T : IConvertible
        {
            var kv = key.ToInt32(null);
            EventListener eventListener;
            if (!m_eventListenerMap.TryGetValue(kv, out eventListener))
            {
                eventListener = new EventListener();
                m_eventListenerMap.Add(kv, eventListener);
            }

            if (eventListener.Add(eventCallback))
                return true;

            Debug.LogWarning("Already Register Same Event:" + key);
            return false;
        }

        /// <summary>
        /// 取消事件注册
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eventCallback"></param>
        public void UnRegister<T>(T key, OnEvent eventCallback) where T : IConvertible
        {
            EventListener eventListener;
            if (m_eventListenerMap.TryGetValue(key.ToInt32(null), out eventListener))
            {
                eventListener.Remove(eventCallback);
            }
        }

        /// <summary>
        /// 取消事件注册
        /// </summary>
        /// <param name="key"></param>
        public void UnRegister<T>(T key) where T : IConvertible
        {
            EventListener eventListener;
            if (m_eventListenerMap.TryGetValue(key.ToInt32(null), out eventListener))
            {
                eventListener.RemoveAll();
                eventListener = null;
                m_eventListenerMap.Remove(key.ToInt32(null));
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        public bool Send<T>(T key, params object[] param) where T : IConvertible
        {
            int kv = key.ToInt32(null);
            EventListener eventListener;
            if (m_eventListenerMap.TryGetValue(kv, out eventListener))
                return eventListener.Fire(kv, param);
            return false;
        }

        public void OnRecycled()
        {
            m_eventListenerMap.Clear();
        }

        #endregion

        #region Event API

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="param"></param>
        public static bool SendEvent<T>(T key, params object[] param) where T : IConvertible
        {
            return Instance.Send(key, param);
        }

        /// <summary>
        /// 取消事件注册
        /// </summary>
        /// <param name="key"></param>
        public static bool RegisterEvent<T>(T key, OnEvent eventListener) where T : IConvertible
        {
            return Instance.Register(key, eventListener);
        }

        /// <summary>
        /// 取消事件注册
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eventListener"></param>
        public static void UnRegisterEvent<T>(T key, OnEvent eventListener) where T : IConvertible
        {
            Instance.UnRegister(key, eventListener);
        }
        #endregion
    }
}
