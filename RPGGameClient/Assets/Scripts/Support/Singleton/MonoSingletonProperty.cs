/*
 * @Author: fasthro
 * @Date: 2019-06-19 17:39:24
 * @Description: 
 */
using UnityEngine;

namespace RPGGame
{
    public static class MonoSingletonProperty<T> where T : MonoBehaviour, ISingleton
    {
        private static T m_instance = null;

        public static T Instance
        {
            get
            {
                if (null == m_instance)
                    m_instance = MonoSingletonCreator.CreateMonoSingleton<T>();
                return m_instance;
            }
        }

        public static void Dispose()
        {
            if (MonoSingletonCreator.IsUnitTestMode)
            {
                Object.DestroyImmediate(m_instance.gameObject);
            }
            else
            {
                Object.Destroy(m_instance.gameObject);
            }

            m_instance = null;
        }
    }
}