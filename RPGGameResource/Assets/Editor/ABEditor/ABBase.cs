/*
 * @Author: fasthro
 * @Date: 2019-06-17 10:37:18
 * @Description: asset bundle
 */
using System.IO;
using UnityEditor;
using UnityEngine;

namespace RPGGameResource.ABEditor
{
    public enum ABStructure
    {
        Standard,          // 标准形式 *[path]/Prefabs/*.prefab, 根据[path]打包成多个ab
        Single,            // 单一文件打包成一个ab
        FolderEntire,      // 整个文件夹打包成一个ab
        SubfolderEntire,   // 文件夹内子文件夹,整个文件夹打包成一个ab *[path]/folder, 根据[path]打包成多个ab
    }

    public abstract class ABBase
    {
        // 结构
        protected ABStructure abStructure;
        // 目标路径
        protected string target;
        // 匹配规则
        protected string pattern;
        // bundle path
        protected string bundlePath;
        // bundle name
        protected string bundleName;

        public void Build()
        {
            switch (abStructure)
            {
                case ABStructure.Standard:
                    BuildStandard();
                    break;
                case ABStructure.Single:
                    BuildSingle();
                    break;
                case ABStructure.FolderEntire:
                    BuildFolderEntire();
                    break;
                 case ABStructure.SubfolderEntire:
                    BuildSubfolderEntire();
                    break;
                default:
                    break;
            }
        }

        private void BuildStandard()
        {
            string[] typeDirs = Directory.GetDirectories(target, "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < typeDirs.Length; i++)
            {
                var typeDir = typeDirs[i];
                var _type = GetPathSection(typeDir, -1);
                var dir = Path.Combine(typeDir, "Prefabs");
                if (Directory.Exists(dir))
                {
                    string[] files = Directory.GetFiles(dir, pattern, SearchOption.AllDirectories);
                    for (int index = 0; index < files.Length; index++)
                    {
                        var abName = Path.Combine(bundlePath, _type);
                        SetBundleName(files[index], abName);
                    }
                }
            }
        }

        private void BuildSingle()
        {
            if (File.Exists(target))
            {
                var abName = Path.Combine(bundlePath, bundleName);
                SetBundleName(target, abName);
            }
        }

        private void BuildFolderEntire()
        {
            if (Directory.Exists(target))
            {
                string[] files = Directory.GetFiles(target, pattern, SearchOption.AllDirectories);
                for (int index = 0; index < files.Length; index++)
                {
                    var abName = Path.Combine(bundlePath, bundleName);
                    SetBundleName(files[index], abName);
                }
            }
        }

        private void BuildSubfolderEntire()
        {
            string[] typeDirs = Directory.GetDirectories(target, "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < typeDirs.Length; i++)
            {
                var typeDir = typeDirs[i];
                var _type = GetPathSection(typeDir, -1);
                string[] files = Directory.GetFiles(typeDir, pattern, SearchOption.AllDirectories);
                for (int index = 0; index < files.Length; index++)
                {
                    var abName = Path.Combine(bundlePath, _type);
                    SetBundleName(files[index], abName);
                }
            }
        }

        private void SetBundleName(string filePath, string bundleName)
        {
            AssetImporter import = AssetImporter.GetAtPath(filePath);
            if (import != null)
            {
                Debug.Log("Set Bundle Name -> " + filePath + " -> " + bundleName);
                import.assetBundleName = bundleName;
            }
        }

        /// <summary>
        /// 获取路径上的第几个位置内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string GetPathSection(string path, int index)
        {
            if (index == 0)
                return "";

            path = ReplaceSeparator(path);
            char separator = Path.AltDirectorySeparatorChar;
            string[] ps = path.Split(separator);

            if (index < 0)
            {
                index = ps.Length + index + 1;
            }

            if (ps.Length >= index)
            {
                return ps[index - 1];
            }

            return "";
        }

        /// <summary>
        /// 替换路径分隔符
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string ReplaceSeparator(string path, string separator = "")
        {
            if (string.IsNullOrEmpty(separator))
            {
                separator = Path.AltDirectorySeparatorChar.ToString();
            }
            return path.Replace("\\", separator).Replace("//", separator);
        }
    }
}
