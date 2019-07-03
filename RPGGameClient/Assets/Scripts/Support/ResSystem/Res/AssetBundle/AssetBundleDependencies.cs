/*
 * @Author: fasthro
 * @Date: 2019-06-27 17:33:16
 * @Description: AssetBundle 资源依赖
 */
using UnityEngine;

namespace RPGGame.ResSystem
{
    public class AssetBundleDependencies : Singleton<AssetBundleDependencies>
    {
        private AssetBundle m_bundle;
        private AssetBundleManifest m_manifest;

        private AssetBundleDependencies() { }

        public override void OnSingletonInit()
        {
            m_bundle = AssetBundle.LoadFromFile(AssetBundlePath.GetFullPath(AssetBundlePath.GetPlatformIds()));
            m_manifest = m_bundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
        }

        /// <summary>
        /// 获取依赖列表
        /// </summary>
        /// <param name="bundleName"></param>
        public string[] _GetDependencies(string bundleName)
        {
            return m_manifest.GetAllDependencies(bundleName);
        }

        #region  API
        public static string[] GetDependencies(string bundleName)
        {
            return Instance._GetDependencies(bundleName);
        }
        #endregion
    }
}