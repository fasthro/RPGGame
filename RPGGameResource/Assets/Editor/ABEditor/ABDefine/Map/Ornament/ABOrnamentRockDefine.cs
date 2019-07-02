/*
 * @Author: fasthro
 * @Date: 2019-07-01 11:44:19
 * @Description: 地图装饰物石头资源定义
 */
namespace RPGGameResource.ABEditor
{
    public class ABOrnamentRockDefine : ABBase
    {
        public ABOrnamentRockDefine()
        {
            abStructure = ABStructure.SubfolderEntire;
            target = "Assets/Art/CubeWorld/Prefabs/Ornament/Rock";
            pattern = "*.prefab";
            bundlePath = "maps/ornaments";
        }
    }
}