/*
 * @Author: fasthro
 * @Date: 2019-07-01 11:44:19
 * @Description: 地图装饰物水晶资源定义
 */
namespace RPGGameResource.ABEditor
{
    public class ABOrnamentCrystalsDefine : ABBase
    {
        public ABOrnamentCrystalsDefine()
        {
            abStructure = ABStructure.SubfolderEntire;
            target = "Assets/Art/CubeWorld/Prefabs/Ornament/Crystals";
            pattern = "*.prefab";
            bundlePath = "maps/ornaments";
        }
    }
}