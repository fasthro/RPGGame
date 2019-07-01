/*
 * @Author: fasthro
 * @Date: 2019-05-17 15:50:05
 * @Description: 资源管理
 */
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace CubeWorldEditor
{
    public class ResManager
    {
        // 单例模式引用
        private static ResManager _inst;
        public static ResManager Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new ResManager();
                }
                return _inst;
            }
        }

        // 资源字典<资源路径md5, ResObject>
        private Dictionary<string, ResObject> m_resObjects = new Dictionary<string, ResObject>();

        private Dictionary<string, ResGroup> m_groups = new Dictionary<string, ResGroup>();

        public void Initialize()
        {
            // 所有资源
            m_resObjects.Clear();
            string[] files = Directory.GetFiles(SettingManager.Inst.Setting.artPath, "*.prefab", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                var id = Utils.MDToStr(Utils.ReplaceSeparator(files[i]));
                var resObject = new ResObject(id, files[i]);
                m_resObjects.Add(id, resObject);
            }

            // 资源分组
            m_groups.Clear();

            string[] dirs = Directory.GetDirectories(SettingManager.Inst.Setting.artPath, "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < dirs.Length; i++)
            {
                var id = Utils.MDToStr(Utils.ReplaceSeparator(dirs[i]));
                var group = new ResGroup(id, dirs[i]);
                m_groups.Add(id, group);
            }
        }

        public ResGroup GetResGroupByName(string groupName)
        {
            string id = Utils.MDToStr(Utils.ReplaceSeparator(SettingManager.Inst.Setting.artPath + groupName));
            ResGroup group = null;
            if (m_groups.TryGetValue(id, out group))
            {
                return group;
            }
            return null;
        }

        public ResObject GetResObject(string id)
        {
            ResObject resObject = null;
            if (m_resObjects.TryGetValue(id, out resObject))
            {
                return resObject;
            }
            return null;
        }

        public Texture2D GetIconTexture(string iconName)
        {
#if UNITY_EDITOR
            string path = string.Format("Assets/Cube-World-Editor/Icons/{0}.png", iconName);
            return AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)) as Texture2D;
#else
            return null;
#endif
        }
    }

    public class ResGroup
    {
        // 资源组id
        private string m_id;
        public string id { get { return m_id; } }

        // 资源组路径
        private string m_groupPath;

        // 是否有分组 
        private bool m_haveGroup;
        public bool haveGroup { get { return m_haveGroup; } }

        // 组内资源分类
        private List<ResGroup> groups = new List<ResGroup>();

        // 组内资源分类
        private List<ResClass> m_classs = new List<ResClass>();

        public ResGroup(string _id, string _groupPath)
        {
            m_id = _id;
            m_groupPath = _groupPath;
            m_haveGroup = false;

            groups.Clear();
            m_classs.Clear();

            string[] dirs = Directory.GetDirectories(_groupPath, "*", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < dirs.Length; i++)
            {
                if (IsGroup(dirs[i]))
                {
                    m_haveGroup = true;

                    var id = Utils.MDToStr(Utils.ReplaceSeparator(dirs[i]));
                    var group = new ResGroup(id, dirs[i]);
                    groups.Add(group);
                }
                else
                {
                    var id = Utils.MDToStr(Utils.ReplaceSeparator(dirs[i]));
                    var _class = new ResClass(id, dirs[i]);
                    m_classs.Add(_class);
                }
            }
        }

        private bool IsGroup(string tp)
        {
            string[] dirs = Directory.GetDirectories(tp, "*", SearchOption.TopDirectoryOnly);
            return dirs.Length > 0;
        }

        public string GetGroupName()
        {
            return Utils.GetPathSection(m_groupPath, -1);
        }

        public List<ResGroup> GetGroups()
        {
            return groups;
        }

        public List<ResClass> GetClasss()
        {
            return m_classs;
        }
    }

    public class ResClass
    {
        // 资源class id
        private string m_id;
        public string id { get { return m_id; } }

        // 资源class 路径
        private string m_classPath;

        // 资源对象列表
        private List<ResObject> m_resObjects = new List<ResObject>();

        public ResClass(string _id, string _classPath)
        {
            m_id = _id;
            m_classPath = _classPath;

            m_resObjects.Clear();
            string[] files = Directory.GetFiles(_classPath, "*.prefab", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                var id = Utils.MDToStr(Utils.ReplaceSeparator(files[i]));
                var resObject = new ResObject(id, files[i]);
                m_resObjects.Add(resObject);
            }
        }

        public List<ResObject> GetResObjects()
        {
            return m_resObjects;
        }

        public string GetClassName()
        {
            return Utils.GetPathSection(m_classPath, -1);
        }
    }

    public class ResObject
    {
        // 资源文件相对路径
        private string m_relativePath;

        // 资源 id
        private string m_id;
        public string id { get { return m_id; } }

        // 资源材料类型
        private MaterialType m_materialType;
        public MaterialType materialType { get { return m_materialType; } }

        // Prefab
        private GameObject m_prefab;
        public GameObject prefab { get { return m_prefab; } }


        public ResObject(string _id, string _relativePath)
        {
            m_relativePath = _relativePath;
            m_id = _id;
            m_materialType = GetMaterialType();
#if UNITY_EDITOR
            m_prefab = AssetDatabase.LoadAssetAtPath(m_relativePath, typeof(GameObject)) as GameObject;
#endif
        }

        private MaterialType GetMaterialType()
        {
            var path = m_relativePath.Substring(SettingManager.Inst.Setting.artPath.Length, m_relativePath.Length - SettingManager.Inst.Setting.artPath.Length);
            var mName = Utils.GetPathSection(path, 1);
            FieldInfo[] fields = typeof(MaterialType).GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                var fieldName = fields[i].Name;
                if (!fieldName.Equals("value__") && fieldName.Equals(mName))
                {
                    return (MaterialType)fields[i].GetValue(fields[i]);
                }
            }
            return MaterialType.Cube;
        }

        public string GetFileNameWithoutExtension()
        {
            return Path.GetFileNameWithoutExtension(m_relativePath);
        }

        public string GetAssetBundleName()
        {
#if UNITY_EDITOR
            AssetImporter import = AssetImporter.GetAtPath(m_relativePath);
            if (import != null)
            {
                return import.assetBundleName;
            }
#endif
            return "";
        }
    }
}
