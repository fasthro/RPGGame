/*
 * @Author: fasthro
 * @Date: 2019-06-19 17:39:23
 * @Description: 无公共构造函数对象工厂
 */
using System;
using System.Reflection;

namespace RPGGame
{
    public class NonPublicObjectFactory<T> : IObjectFactory<T> where T : class
    {
        public T Create()
        {
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
            return ctor.Invoke(null) as T;
        }
    }
}