namespace RPGGame.Editor.ABEditor
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