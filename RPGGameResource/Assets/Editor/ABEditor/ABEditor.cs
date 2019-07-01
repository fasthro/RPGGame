using System.IO;
using System.Reflection;
using UnityEditor;

namespace RPGGame.Editor.ABEditor
{
    public class ABEditor
    {
        [MenuItem("RPGGame/设置AssetBundle名称")]
        public static void GetABDefines()
        {
            AssetDatabase.RemoveUnusedAssetBundleNames();
            
            string[] files = Directory.GetFiles("Assets/Editor/ABEditor/ABDefine", "*.cs", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string className = Path.GetFileNameWithoutExtension(files[i]);
                string fullName = "RPGGame.Editor.ABEditor." + className;
                object obj = Assembly.GetExecutingAssembly().CreateInstance(fullName, true, System.Reflection.BindingFlags.Default, null, null, null, null);
                ((ABBase)obj).Build();
            }
        }
    }
}
