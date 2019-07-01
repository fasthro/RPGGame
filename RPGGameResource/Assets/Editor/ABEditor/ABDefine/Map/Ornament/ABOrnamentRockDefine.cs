namespace RPGGame.Editor.ABEditor
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