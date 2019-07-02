/*
 * @Author: fasthro
 * @Date: 2019-07-01 11:44:19
 * @Description: 地图Cube资源定义
 */
namespace RPGGameResource.ABEditor
{
    public class ABCubeDefine : ABBase
    {
        public ABCubeDefine()
        {
            abStructure = ABStructure.SubfolderEntire;
            target = "Assets/Art/CubeWorld/Prefabs/Cube";
            pattern = "*.prefab";
            bundlePath = "maps/cubes";
        }
    }
}