/*
 * @Author: fasthro
 * @Date: 2019-06-17 19:22:03
 * @Description: 地图cube
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGGame.ResSystem;

namespace RPGGame
{
    public class CubeComponent : MonoBehaviour
    {
        // cube 数据
        private CubeParse m_cubeParse;

        // 资源加载loader
        private AssetBundleLoader m_loader;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(CubeParse parse)
        {
            m_cubeParse = parse;
        }

        /// <summary>
        /// 加载
        /// </summary>
        public void Load()
        {
            m_loader = AssetBundleLoader.Allocate(m_cubeParse.ab, m_cubeParse.assets, (ready, res) =>
            {
                GameObject go = GameObject.Instantiate(res.GetAsset<GameObject>());
                go.transform.parent = transform;
                go.transform.position = m_cubeParse.position;
                go.transform.eulerAngles = m_cubeParse.angle;
                go.transform.localScale = m_cubeParse.scale;
            });
            m_loader.LoadAsync();
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Release()
        {
            if (m_loader != null)
            {
                m_loader.Unload();
                m_loader = null;
            }
        }
    }
}
