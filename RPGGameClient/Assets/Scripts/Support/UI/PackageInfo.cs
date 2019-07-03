/*
 * @Author: fasthro
 * @Date: 2019-06-20 17:02:26
 * @Description: UI包信息
 */
using FairyGUI;
using RPGGame.ResSystem;

namespace RPGGame
{
    public class PackageInfo
    {
        // 包id
        public string packageId;
        // 包实例
        public UIPackage package;
        // 加载器实例
        public AssetBundleLoader loader;
        // 引用数
        public int rc;

        public PackageInfo()
        {
            rc = 1;
        }
    }
}