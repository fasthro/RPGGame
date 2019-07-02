/*
 * @Author: fasthro
 * @Date: 2019-07-01 11:44:19
 * @Description: 地图装饰物单独资源定义
 */
namespace RPGGameResource.ABEditor
{
    public class ABOrnamentSingleDefine : ABBase
    {
        public ABOrnamentSingleDefine()
        {
            abStructure = ABStructure.FolderEntire;
            target = "Assets/Art/CubeWorld/Prefabs/Ornament/Single";
            pattern = "*.prefab";
            bundleName = "Single";
            bundlePath = "maps/ornaments";
        }
    }
}