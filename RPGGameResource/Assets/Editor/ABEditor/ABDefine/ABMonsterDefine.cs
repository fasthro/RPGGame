/*
 * @Author: fasthro
 * @Date: 2019-06-17 11:23:13
 * @Description: 怪ab资源定义
 */

namespace RPGGame.Editor.ABEditor
{
    public class ABMonsterDefine : ABBase
    {
        public ABMonsterDefine()
        {
            abStructure = ABStructure.Standard;
            target = "Assets/Art/Monsters";
            pattern = "*.prefab";
            bundlePath = "monsters";
        }
    }
}


