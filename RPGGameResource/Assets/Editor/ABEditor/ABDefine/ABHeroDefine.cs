/*
 * @Author: fasthro
 * @Date: 2019-06-17 11:23:13
 * @Description: 英雄ab资源定义
 */

namespace RPGGame.Editor.ABEditor
{
    public class ABHeroDefine : ABBase
    {
        public ABHeroDefine()
        {
            abStructure = ABStructure.Standard;
            target = "Assets/Art/Heros";
            pattern = "*.prefab";
            bundlePath = "heros";
        }
    }
}

