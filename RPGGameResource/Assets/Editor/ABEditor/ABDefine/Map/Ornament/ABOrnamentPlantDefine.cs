/*
 * @Author: fasthro
 * @Date: 2019-07-01 11:44:19
 * @Description: 地图装饰物植物资源定义
 */
namespace RPGGameResource.ABEditor
{
    public class ABOrnamentPlantDefine : ABBase
    {
        public ABOrnamentPlantDefine()
        {
            abStructure = ABStructure.SubfolderEntire;
            target = "Assets/Art/CubeWorld/Prefabs/Ornament/Plant";
            pattern = "*.prefab";
            bundlePath = "maps/ornaments";
        }
    }
}