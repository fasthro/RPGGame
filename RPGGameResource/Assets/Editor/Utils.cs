/*
 * @Author: fasthro
 * @Date: 2019-07-03 10:27:04
 * @Description: Editor Utils
 */
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RPGGameResource.Editor
{
    public class Utils
    {
        /// <summary>
        /// Copy Directory
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        public static void DirectoryCopy(string sourceDirName, string destDirName)
        {
            sourceDirName = sourceDirName.Replace("\\", "/");
            destDirName = destDirName.Replace("\\", "/");

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            foreach (string folderPath in Directory.GetDirectories(sourceDirName, "*", SearchOption.AllDirectories))
            {
                if (!Directory.Exists(folderPath.Replace(sourceDirName, destDirName)))
                    Directory.CreateDirectory(folderPath.Replace(sourceDirName, destDirName));
            }

            foreach (string filePath in Directory.GetFiles(sourceDirName, "*.*", SearchOption.AllDirectories))
            {
                var fileDirName = Path.GetDirectoryName(filePath).Replace("\\", "/");
                var fileName = Path.GetFileName(filePath);
                string newFilePath = Path.Combine(fileDirName.Replace(sourceDirName, destDirName), fileName);
                
                File.Copy(filePath, newFilePath, true);
            }
        }
    }

}
