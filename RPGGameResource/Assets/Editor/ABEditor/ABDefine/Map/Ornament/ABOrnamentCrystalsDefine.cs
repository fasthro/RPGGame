namespace RPGGame.Editor.ABEditor
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