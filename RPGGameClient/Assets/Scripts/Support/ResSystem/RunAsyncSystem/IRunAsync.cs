/*
 * @Author: fasthro
 * @Date: 2019-06-22 20:36:28
 * @Description: 异步接口
 */
using System;
using System.Collections;

namespace RPGGame.ResSystem
{
    public interface IRunAsync
    {
        IEnumerator RunAsync(IRunAsyncSystem asyncSystem);
    }
}