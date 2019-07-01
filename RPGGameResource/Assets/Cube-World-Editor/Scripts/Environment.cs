/*
 * @Author: fasthro
 * @Date: 2019-05-17 14:28:57
 * @Description: Cube World 环境
 */
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace CubeWorldEditor
{
    public class Environment : MonoBehaviour
    {
        // 单例模式引用
        private static Environment _inst;
        public static Environment Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = GameObject.Find(typeof(Environment).Name).transform.GetComponent<Environment>();
                }
                return _inst;
            }
        }

        // 场景名称
        public string sceneName;
        // 区域字典
        public Dictionary<int, EnvironmentArea> areas;
        // navmesh
        public NavMeshSurface navMeshSurface;

        #region editor
        public void Initialize()
        {
            // 初始化区域
            areas = new Dictionary<int, EnvironmentArea>();

            var transCount = transform.childCount;
            for (int i = 0; i < transCount; i++)
            {
                var areaTransform = transform.GetChild(i);
                var areaIndex = int.Parse(areaTransform.gameObject.name);
                var area = areaTransform.GetComponent<EnvironmentArea>();
                area.Initialize(areaIndex);
                areas.Add(areaIndex, area);
            }
        }

        /// <summary>
        /// 画格子
        /// </summary>
        /// <param name="brushGrid"></param>
        /// <param name="areaIndex"></param>
        public void DrawGrid(Grid brushGrid, int areaIndex)
        {
            string key = GetKey(brushGrid.position, areaIndex);
            if (GetGrid(brushGrid.position, areaIndex)) return;

            var resObject = ResManager.Inst.GetResObject(brushGrid.resId);

            var go = GameObject.Instantiate(resObject.prefab) as GameObject;
            go.name = key;
            go.transform.parent = GetGridRoot(resObject.materialType, areaIndex);
            go.transform.position = brushGrid.transform.position;
            go.transform.localEulerAngles = brushGrid.transform.localEulerAngles;

            var grid = go.AddComponent<Grid>();
            grid.key = key;
            grid.resId = brushGrid.resId;
            grid.areaIndex = areaIndex;
            grid.position = brushGrid.position;
            grid.positionOffset = brushGrid.positionOffset;
            grid.angle = brushGrid.angle;
            grid.angleOffset = brushGrid.angleOffset;
            grid.materialType = resObject.materialType;
            grid.scale = brushGrid.scale;
            grid.scaleOffset = brushGrid.scaleOffset;
            grid.Initialize();

            var addSucceed = AddGrid(brushGrid.position, areaIndex, grid);
            if (!addSucceed)
            {
                DestroyImmediate(grid.gameObject);
            }
        }

        /// <summary>
        /// 擦除格子
        /// </summary>
        /// <param name="key"></param>
        public void EraseGrid(Vector3 position, int areaIndex)
        {
            var grid = GetGrid(position, areaIndex);
            if (grid != null)
            {
                RemoveGrid(position, areaIndex);

                GameObject.DestroyImmediate(grid.gameObject);
            }
        }
        #endregion

        #region area and grid
        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="areaIndex"></param>
        /// <returns></returns>
        public EnvironmentArea GetArea(int areaIndex)
        {
            EnvironmentArea area = null;
            if (areas.TryGetValue(areaIndex, out area))
            {
                return area;
            }
            return null;
        }

        /// <summary>
        /// 获取格子
        /// </summary>
        /// <param name="position"></param>
        /// <param name="areaIndex"></param>
        /// <returns></returns>
        public Grid GetGrid(Vector3 position, int areaIndex)
        {
            var key = GetKey(position, areaIndex);
            var area = GetArea(areaIndex);
            if (area != null)
            {
                return area.GetGrid(key);
            }
            return null;
        }

        /// <summary>
        /// 添加格子
        /// </summary>
        /// <param name="position"></param>
        /// <param name="areaIndex"></param>
        /// <param name="grid"></param>
        public bool AddGrid(Vector3 position, int areaIndex, Grid grid)
        {
            var key = GetKey(position, areaIndex);
            var area = GetArea(areaIndex);
            if (area != null)
            {
                return area.AddGrid(key, grid);
            }
            return false;
        }

        /// <summary>
        /// 移除格子
        /// </summary>
        /// <param name="position"></param>
        /// <param name="areaIndex"></param>
        public void RemoveGrid(Vector3 position, int areaIndex)
        {
            var key = GetKey(position, areaIndex);
            var area = GetArea(areaIndex);
            if (area != null)
            {
                area.RemoveGrid(key);
            }
        }

        /// <summary>
        /// 获取最大区域索引
        /// </summary>
        public int GetMaxAreaIndex()
        {
            return areas.Count;
        }

        /// <summary>
        /// 添加区域
        /// </summary>
        public void AddArea()
        {
            int nk = GetMaxAreaIndex() + 1;
            if (!areas.ContainsKey(nk))
            {
                CreateArea(nk);
            }
        }
        #endregion

        #region  scene data
        /// <summary>
        /// 导出关卡数据
        /// </summary>
        public void ExportXml()
        {
            string content = string.Empty;
            int areaCount = 0;
            foreach (KeyValuePair<int, EnvironmentArea> areaItem in areas)
            {
                content += areaItem.Value.ExportXml();
                areaCount++;
            }

            string template = Utils.LoadTemplate("Level.txt");
            template = template.Replace("{#level_name}", sceneName);
            template = template.Replace("{#area_count}", areaCount.ToString());
            template = template.Replace("{#content}", content);

            string file = Utils.GetSceneDataPath(sceneName);
            string dir = Path.GetDirectoryName(file);
            if (File.Exists(dir))
            {
                File.Delete(dir);
            }
            File.WriteAllText(file, template);
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
        #endregion

        /// <summary>
        /// 获取Grid所在节点
        /// </summary>
        /// <param name="mt"></param>
        /// <param name="areaIndex"></param>
        /// <returns></returns>
        public Transform GetGridRoot(MaterialType mt, int areaIndex)
        {
            string areaKey = areaIndex.ToString();

            var areaTransform = transform.Find(areaKey);
            // 创建区域节点
            if (areaTransform == null)
            {
                areaTransform = CreateArea(areaIndex);
            }

            // 创建区域材料节点
            FieldInfo[] fields = typeof(MaterialType).GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                var name = fields[i].Name;
                if (!name.Equals("value__"))
                {
                    if (mt.ToString().Equals(name))
                    {
                        var materialTransform = areaTransform.Find(name);
                        if (materialTransform == null)
                        {
                            GameObject materialGo = new GameObject();
                            materialGo.name = name;
                            materialGo.transform.parent = areaTransform;

                            materialTransform = materialGo.transform;
                        }
                        return materialTransform;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 创建区域节点
        /// </summary>
        /// <param name="areaIndex"></param>
        private Transform CreateArea(int areaIndex)
        {
            string areaKey = areaIndex.ToString();
            GameObject areaGo = new GameObject();
            areaGo.name = areaKey;
            areaGo.transform.parent = transform;

            var area = areaGo.AddComponent<EnvironmentArea>();
            area.Initialize(areaIndex);
            areas.Add(areaIndex, area);

            return areaGo.transform;
        }

        /// <summary>
        /// 获取key
        /// </summary>
        /// <param name="position"></param>
        /// <param name="areaIndex"></param>
        /// <returns></returns>
        public static string GetKey(Vector3 position, int areaIndex)
        {
            return string.Format("x:{0}/y:{1}/z:{2}/area:{3}", position.x, position.y, position.z, areaIndex.ToString());
        }

        /// <summary>
        /// 获取运行时key
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string GetRunTimeKey(Vector3 position)
        {
            return string.Format("{0}|{1}|{2}", position.x, position.y, position.z);
        }

        /// <summary>
        /// 通过key获取areaIndex
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetAreaToKey(string key)
        {
            string[] strs = key.Split('/');
            string areaStr = strs[3];
            return int.Parse(areaStr.Substring(5));
        }
    }
}
