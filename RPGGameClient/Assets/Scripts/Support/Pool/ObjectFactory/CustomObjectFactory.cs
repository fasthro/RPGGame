/*
 * @Author: fasthro
 * @Date: 2019-06-19 17:39:23
 * @Description: 自定义对象工厂
 */
using System;

namespace RPGGame
{
    public class CustomObjectFactory<T> : IObjectFactory<T>
    {
        protected Func<T> m_factoryMethod;
        
        public CustomObjectFactory(Func<T> factoryMethod)
        {
            m_factoryMethod = factoryMethod;
        }

        public T Create()
        {
            return m_factoryMethod();
        }
    }
}