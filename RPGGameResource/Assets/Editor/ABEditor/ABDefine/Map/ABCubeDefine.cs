namespace RPGGame.Editor.ABEditor
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