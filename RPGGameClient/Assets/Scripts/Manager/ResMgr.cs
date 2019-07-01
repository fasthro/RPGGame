/*
 * @Author: fasthro
 * @Date: 2019-01-10 11:11:21
 * @Description: 资源管理
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGGame.ResSystem;

namespace RPGGame
{
    public class ResMgr : Singleton<ResMgr>, IManager
    {
        private ResMgr() { }

        #region 接口
        public void Init()
        {
            throw new NotImplementedException();
        }

        public void FixedUpdate()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void LateUpdate()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 同步加载Resource资源
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="listener"></param>
        public ResourceLoader LoadSync(string assetName, ResNotificationListener listener)
        {
            var loader = ResLoaderFactory.CreateResourceLoader(assetName, listener);
            loader.LoadSync();
            return loader;
        }

        /// <summary>
        /// 同步加载AssetBundle资源
        /// </summary>
        /// <param name="bundleName">assetBundle名称</param>
        /// <param name="assetName">资源名称</param>
        /// <param name="listener"></param>
        public AssetBundleLoader LoadSync(string bundleName, string assetName, ResNotificationListener listener)
        {
            var loader = ResLoaderFactory.CreateAssetBundleLoader(bundleName, assetName, listener);
            loader.LoadSync();
            return loader;
        }

        /// <summary>
        /// 异步加载Resource资源
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="listener"></param>
        public ResourceLoader LoadAsync(string assetName, ResNotificationListener listener)
        {
            var loader = ResLoaderFactory.CreateResourceLoader(assetName, listener);
            loader.LoadAsync();
            return loader;
        }

        /// <summary>
        /// 异步加载AssetBundle资源
        /// </summary>
        /// <param name="bundleName">assetBundle名称</param>
        /// <param name="assetName">资源名称</param>
        /// <param name="listener"></param>
        public AssetBundleLoader LoadAsync(string bundleName, string assetName, ResNotificationListener listener)
        {
            var loader = ResLoaderFactory.CreateAssetBundleLoader(bundleName, assetName, listener);
            loader.LoadAsync();
            return loader;
        }
    }
}

