/*
 * @Author: fasthro
 * @Date: 2019-06-17 19:21:46
 * @Description: 地图区域
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{
    public class AreaComponent : MonoBehaviour
    {

        // 基础
        private CubeComponent[] m_cubes;
        public CubeComponent[] cubes { get { return m_cubes; } }

        // 基础节点
        private Transform m_cubeRoot;

        // 装饰物
        private CubeComponent[] m_ornaments;
        public CubeComponent[] ornaments { get { return m_ornaments; } }

        // 装饰物节点
        private Transform m_ornamentRoot;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(AreaParse parse)
        {
            m_cubeRoot = CreateRoot("CubeRoot");
            m_ornamentRoot = CreateRoot("OrnamentRoot");

            m_cubes = new CubeComponent[parse.cubes.Length];
            for (int i = 0; i < parse.cubes.Length; i++)
            {
                m_cubes[i] = CreateCubeComponent(parse.cubes[i], m_cubeRoot);
            }

            m_ornaments = new CubeComponent[parse.ornaments.Length];
            for (int i = 0; i < parse.ornaments.Length; i++)
            {
                m_ornaments[i] = CreateCubeComponent(parse.ornaments[i], m_ornamentRoot);
            }
        }

        /// <summary>
        /// 加载
        /// </summary>
        public void Load()
        {
            for (int i = 0; i < m_cubes.Length; i++)
            {
                m_cubes[i].Load();
            }

            for (int i = 0; i < m_ornaments.Length; i++)
            {
                m_ornaments[i].Load();
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Release()
        {
            for (int i = 0; i < m_cubes.Length; i++)
            {
                m_cubes[i].Release();
            }
            m_cubes = null;

            for (int i = 0; i < m_ornaments.Length; i++)
            {
                m_ornaments[i].Release();
            }
            m_ornaments = null;
        }

        /// <summary>
        /// 区域内创建节点
        /// </summary>
        /// <param name="name"></param>
        private Transform CreateRoot(string name)
        {
            GameObject root = new GameObject();
            root.name = name;
            root.transform.parent = transform;
            return root.transform;
        }

        /// <summary>
        /// 创建CubeComponent
        /// </summary>
        /// <param name="cubeParse"></param>
        /// <param name="root"></param>
        private CubeComponent CreateCubeComponent(CubeParse cubeParse, Transform root)
        {
            GameObject cube = new GameObject();
            cube.name = cubeParse.position.ToString();
            cube.transform.parent = root;
            var cubeComponent = cube.AddComponent<CubeComponent>();
            cubeComponent.Init(cubeParse);
            return cubeComponent;
        }
    }
}
