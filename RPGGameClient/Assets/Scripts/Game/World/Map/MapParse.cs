/*
 * @Author: fasthro
 * @Date: 2019-06-17 17:42:13
 * @Description: 地图数据解析器
 */
 
using System.Security;
using Mono.Xml;
using UnityEngine;

namespace RPGGame
{
    public class MapParse
    {
        // 区域数量
        private int m_areaCount;
        public int areaCount { get { return m_areaCount; } }

        // 区域列表
        private AreaParse[] m_areas;
        public AreaParse[] areas { get { return m_areas; } }

        private SecurityElement m_mainElement;

        public MapParse(string xmlStr)
        {
            var security = new SecurityParser();
            security.LoadXml(xmlStr);
            m_mainElement = security.ToXml();
        }

        public void Parse()
        {
            m_areaCount = int.Parse(m_mainElement.Attribute("area_count"));
            m_areas = new AreaParse[m_areaCount];

            foreach (SecurityElement child in m_mainElement.Children)
            {
                var area = new AreaParse(child);
                area.Parse();
                m_areas[area.index] = area;
            }
        }
    }

    public class AreaParse
    {
        // 区域索引
        private int m_index;
        public int index { get { return m_index; } }

        // 区域地块
        private CubeParse[] m_cubes;
        public CubeParse[] cubes { get { return m_cubes; } }

        // 区域覆盖物
        private CubeParse[] m_ornaments;
        public CubeParse[] ornaments { get { return m_ornaments; } }

        private SecurityElement m_mainElement;

        public AreaParse(SecurityElement element)
        {
            m_mainElement = element;
        }

        public void Parse()
        {
            m_index = int.Parse(m_mainElement.Attribute("index")) - 1;

            foreach (SecurityElement rootChild in m_mainElement.Children)
            {
                if (rootChild.Tag.Equals("cube"))
                {
                    int count = int.Parse(rootChild.Attribute("count"));
                    int _index = 0;
                    m_cubes = new CubeParse[count];
                    foreach (SecurityElement child in rootChild.Children)
                    {
                        var cube = new CubeParse(child);
                        cube.Parse();
                        m_cubes[_index] = cube;
                        _index++;
                    }
                }
                else if (rootChild.Tag.Equals("ornament"))
                {
                    int count = int.Parse(rootChild.Attribute("count"));
                    int _index = 0;
                    m_ornaments = new CubeParse[count];
                    foreach (SecurityElement child in rootChild.Children)
                    {
                        var cube = new CubeParse(child);
                        cube.Parse();
                        m_ornaments[_index] = cube;
                        _index++;
                    }
                }
            }
        }
    }

    public class CubeParse
    {
        // bundle name
        private string m_ab;
        public string ab { get { return m_ab; } }

        // 资源
        private string m_asset;
        public string assets { get { return m_asset; } }

        // 位置
        private Vector3 m_position;
        public Vector3 position { get { return m_position; } }

        // 角度
        private Vector3 m_angle;
        public Vector3 angle { get { return m_angle; } }

        // 缩放
        private Vector3 m_scale;
        public Vector3 scale { get { return m_scale; } }

        private SecurityElement m_mainElement;

        public CubeParse(SecurityElement element)
        {
            m_mainElement = element;
        }

        public void Parse()
        {
            m_ab = m_mainElement.Attribute("ab");
            m_asset = m_mainElement.Attribute("asset");
            m_position = ParseVector3(m_mainElement.Attribute("pos"));
            m_angle = ParseVector3(m_mainElement.Attribute("angle"));
            m_scale = ParseVector3(m_mainElement.Attribute("scale"));
        }

        private Vector3 ParseVector3(string v3Str)
        {
            string[] vs = v3Str.Split(',');
            float x = float.Parse(vs[0]);
            float y = float.Parse(vs[1]);
            float z = float.Parse(vs[2]);
            return new Vector3(x, y, z);
        }
    }
}
