/*
 * @Author: fasthro
 * @Date: 2019-05-17 14:28:57
 * @Description: 工具类
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    public class Utils
    {

        public static T CreateScriptable<T>() where T : ScriptableObject
        {
            T newScriptable = ScriptableObject.CreateInstance<T>();
#if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path.Length == 0)
            {
                path = "Assets/";
            }
            else
            {
                int lastPos = path.LastIndexOf("/");
                path = path.Substring(0, lastPos + 1);
            }

            string className = typeof(T).Name;
            path = AssetDatabase.GenerateUniqueAssetPath(path + "/" + className + ".asset");
            AssetDatabase.CreateAsset(newScriptable, path);

#endif
            return newScriptable;
        }

        /// <summary>
        ///  加载模版
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public static string LoadTemplate(string templateName)
        {
#if UNITY_EDITOR
            string path = Path.Combine("Assets/Cube-World-Editor/Templates/", templateName);
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
            TextAsset asset = obj as TextAsset;
            return asset.text;
#else
            return "";
#endif
        }


        /// <summary>
        /// 字符串首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpperFirstChar(string str)
        {
            string outStr = "";
            char[] chars = str.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 0)
                {
                    outStr += chars[i].ToString().ToUpper();
                }
                else
                {
                    outStr += chars[i].ToString();
                }
            }
            return outStr;
        }

        /// <summary>
        /// 获取路径上的第几个位置内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetPathSection(string path, int index)
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
        public static string ReplaceSeparator(string path, string separator = "")
        {
            if (string.IsNullOrEmpty(separator))
            {
                separator = Path.AltDirectorySeparatorChar.ToString();
            }
            return path.Replace("\\", separator).Replace("//", separator);
        }

        /// <summary>
        /// 获取Assets/下面的完整路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPathToAssets(string path)
        {
            return Path.Combine("Assets", path);
        }

        /// <summary>
        /// 获取场景保存目录
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public static string GetSceneDirectory(string sceneName, bool absolute = true)
        {
            var directory = Path.Combine(SettingManager.Inst.Setting.sceneSavePath, sceneName);
            if (absolute)
                return Path.Combine(Application.dataPath, directory);
            return Path.Combine(SettingManager.Inst.Setting.sceneSavePath, sceneName);
        }

        /// <summary>
        /// 获取场景保存路径
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public static string GetScenePath(string sceneName, bool absolute = true)
        {
            return Path.Combine(GetSceneDirectory(sceneName, absolute), sceneName + ".unity");
        }

        /// <summary>
        /// 获取数据保存路径
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public static string GetSceneDataPath(string sceneName, bool absolute = true)
        {
            return Path.Combine(GetSceneDirectory(sceneName, absolute), sceneName + ".xml");
        }

        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        public static string MDToStr(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
            md5.Clear();

            string destString = "";
            for (int i = 0; i < md5Data.Length; i++)
            {
                destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
            }
            destString = destString.PadLeft(32, '0');
            return destString;
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        public static string MD5ToFile(string file)
        {
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }

        /// <summary>
        /// SceneView 截图
        /// </summary>
        public static void CaptureScreenShot(string saveFilePath, bool openFinder)
        {
#if UNITY_EDITOR
            var w = SettingManager.Inst.Setting.screenshotWidth * SettingManager.Inst.Setting.supersizeResolution;
            var h = SettingManager.Inst.Setting.screenshotHeight * SettingManager.Inst.Setting.supersizeResolution;

            var renderTexture = new RenderTexture(w, h, 24);
            var mCamera = SceneView.lastActiveSceneView.camera;

            mCamera.targetTexture = renderTexture;
            mCamera.Render();

            RenderTexture.active = renderTexture;

            var screenShot = new Texture2D(w, h, TextureFormat.RGB24, false);
            screenShot.ReadPixels(new Rect(0, 0, w, h), 0, 0);
            screenShot.Apply();

            mCamera.targetTexture = null;
            RenderTexture.active = null;

            var dirPath = Path.GetDirectoryName(saveFilePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            var bytes = screenShot.EncodeToPNG();
            File.WriteAllBytes(saveFilePath, bytes);

            if (openFinder) EditorUtility.RevealInFinder(Application.dataPath + "/../" + saveFilePath);
#endif
        }
    }
}