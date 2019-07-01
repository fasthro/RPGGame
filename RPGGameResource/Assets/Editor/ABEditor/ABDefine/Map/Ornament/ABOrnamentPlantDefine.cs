namespace RPGGame.Editor.ABEditor
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