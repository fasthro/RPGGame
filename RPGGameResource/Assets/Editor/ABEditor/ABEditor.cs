/*
 * @Author: fasthro
 * @Date: 2019-07-01 11:44:19
 * @Description: AssetBundle 工具
 */
using System.IO;
using System.Reflection;
using AssetBundleBrowser.AssetBundleDataSource;
using UnityEditor;
using UnityEngine;

namespace RPGGameResource.ABEditor
{
    public class ABEditor
    {
        [MenuItem("RPGGameResource/Set AssetBundle Name")]
        public static void SetAssetBundleName()
        {
            AssetDatabase.RemoveUnusedAssetBundleNames();

            string[] files = Directory.GetFiles("Assets/Editor/ABEditor/ABDefine", "*.cs", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string className = Path.GetFileNameWithoutExtension(files[i]);
                string fullName = "RPGGameResource.ABEditor." + className;
                object obj = Assembly.GetExecutingAssembly().CreateInstance(fullName, true, System.Reflection.BindingFlags.Default, null, null, null, null);
                ((ABBase)obj).Build();
            }
        }

        [MenuItem("RPGGameResource/Clean Build AssetBundle")]
        public static void CleanBuild()
        {
            string outPath = GetAssetBundleOutPath();
            if (Directory.Exists(outPath))
                Directory.Delete(outPath, true);

            Build();
        }

        [MenuItem("RPGGameResource/Build AssetBundle")]
        public static void Build()
        {
             string outPath = GetAssetBundleOutPath();

            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);

            ABBuildInfo buildInfo = new ABBuildInfo();

            buildInfo.outputDirectory = outPath;
            buildInfo.options = BuildAssetBundleOptions.ChunkBasedCompression;
            buildInfo.buildTarget = EditorUserBuildSettings.activeBuildTarget;

            AssetBundleBrowser.AssetBundleModel.Model.DataSource.BuildAssetBundles(buildInfo);
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        // assetbundle outpath
        private static string GetAssetBundleOutPath()
        {
            var outputDirectory = Application.streamingAssetsPath;
#if UNITY_ANDROID
                outputDirectory = Path.Combine(outputDirectory, "Android");
#elif UNITY_IPHONE
                outputDirectory = Path.Combine(outputDirectory, "IOS");
#elif UNITY_STANDALONE_WIN
            outputDirectory = Path.Combine(outputDirectory, "Windows");
#elif UNITY_STANDALONE_OSX
                outputDirectory = Path.Combine(outputDirectory, "OSX");
#endif
            return outputDirectory.Replace("RPGGameResource", "RPGGameClient");
        }
    }
}
