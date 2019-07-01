/*
 * @Author: fasthro
 * @Date: 2019-06-17 20:55:48
 * @Description: 环境
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{
    public class World
    {
        // 地图组件
        private MapComponent m_mapComponent;
        public MapComponent mapComponent { get { return m_mapComponent; } }

        #region 地图

        /// <summary>
        /// 初始化地图
        /// </summary>
        /// <param name="mapName"></param>
        public void InitMap(string mapName)
        {
            // 加载地图配置
            var bundleName = "maps/datas/" + mapName.ToLower();
            var assetName = mapName + ".xml";
            var ml = ResMgr.Instance.LoadSync(bundleName, assetName, null);
            var asset = ml.assetRes.GetAsset<TextAsset>();

            MapParse parse = new MapParse(asset.text);
            parse.Parse();

            ml.Unload();
            ml = null;

            // 初始化地图组件
            GameObject map = new GameObject();
            map.name = "MapComponent";
            m_mapComponent = map.AddComponent<MapComponent>();
            m_mapComponent.Init(parse);
        }

        /// <summary>
        /// 加载地图
        /// </summary>
        /// <param name="mapName"></param>
        public void LoadMap(string mapName)
        {
            if (m_mapComponent == null)
                InitMap(mapName);

            m_mapComponent.Load();
        }

        /// <summary>
        /// 释放地图
        /// </summary>
        public void ReleaseMap()
        {
            if(m_mapComponent != null){
                m_mapComponent.Release();
                m_mapComponent = null;
            }
        }

        #endregion
    }
}
