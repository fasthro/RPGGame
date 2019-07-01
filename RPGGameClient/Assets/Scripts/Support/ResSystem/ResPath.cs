/*
 * @Author: fasthro
 * @Date: 2019-06-26 18:08:21
 * @Description: 资源路径
 */
using System.IO;
using UnityEngine;

namespace RPGGame.ResSystem
{
    public class ResPath
    {
        // assetBundle 路径
        private static string assetBundlePath;
        public static string AssetBundlePath
        {
            get
            {
                if (string.IsNullOrEmpty(assetBundlePath))
                    assetBundlePath = Path.Combine(Application.streamingAssetsPath, AssetBundlePlatform);
                return assetBundlePath;
            }
        }

        /// bundle Platform
        private static string assetBundlePlatform;
        public static string AssetBundlePlatform
        {
            get
            {
#if UNITY_ANDROID
                assetBundlePlatform = "Android";
#elif UNITY_IPHONE
                assetBundlePlatform = "IOS";
#elif UNITY_STANDALONE_WIN
                assetBundlePlatform = "Windows";
#elif UNITY_STANDALONE_OSX
                assetBundlePlatform = "OSX";
#endif
                return assetBundlePlatform;
            }
        }


        /// <summary>
        /// bundle 名称转成 bundle 路径
        /// </summary>
        /// <param name="bundleName"></param>
        public static string AssetBundleName2Url(string bundleName)
        {
            return Path.Combine(AssetBundlePath, bundleName);
        }
    }
}