/*
 * @Author: fasthro
 * @Date: 2019-06-20 17:02:26
 * @Description: UI包信息
 */
using FairyGUI;

namespace RPGGame
{
    public class PackageInfo
    {
        // 包id
        public string packageId { get { return m_package.id; } }

        // 包实例
        private UIPackage m_package;
        public UIPackage package { get { return m_package; } }

        // 引用数
        private int m_rc;
        public int rc { get { return m_rc; } set { m_rc = value; } }

        public PackageInfo(UIPackage package)
        {
            m_package = package;
            m_rc = 1;
        }
    }
}