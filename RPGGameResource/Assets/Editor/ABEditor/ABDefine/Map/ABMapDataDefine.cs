/*
 * @Author: fasthro
 * @Date: 2019-06-18 11:16:05
 * @Description: 地图数据资源定义
 */

namespace RPGGameResource.ABEditor
{
    public class ABMapDataDefine : ABBase
    {
        public ABMapDataDefine()
        {
            abStructure = ABStructure.SubfolderEntire;
            target = "Assets/Art/Maps";
            pattern = "*";
            bundlePath = "maps/datas";
        }
    }
}

