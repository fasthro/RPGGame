/*
 * @Author: fasthro
 * @Date: 2019-06-17 17:24:42
 * @Description: 地图组件
 */

using UnityEngine;

namespace RPGGame
{
    public class MapComponent : MonoBehaviour
    {
        // 地图区域列表
        private AreaComponent[] m_areas;
        public AreaComponent[] areas { get { return m_areas; } }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parse"></param>
        public void Init(MapParse parse)
        {
            m_areas = new AreaComponent[parse.areaCount];
            for (int i = 0; i < parse.areaCount; i++)
            {
                GameObject go = new GameObject();
                go.name = parse.areas[i].index.ToString();
                go.transform.parent = transform;
                var ac = go.AddComponent<AreaComponent>();
                ac.Init(parse.areas[i]);
                m_areas[i] = ac;
            }
        }

        /// <summary>
        /// 加载
        /// </summary>
        public void Load()
        {
            for (int i = 0; i < m_areas.Length; i++)
            {
                m_areas[i].Load();
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Release()
        {
            for (int i = 0; i < m_areas.Length; i++)
            {
                m_areas[i].Release();
            }
            m_areas = null;
        }
    }
}
