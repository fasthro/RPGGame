/*
 * @Author: fasthro
 * @Date: 2019-05-29 18:11:40
 * @Description: 设置管理
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CubeWorldEditor
{
    public class SettingManager
    {
        // 单例模式引用
        private static SettingManager _inst;
        public static SettingManager Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new SettingManager();
                    _inst.Initialize();
                }
                return _inst;
            }
        }

        // 设置 STO
        public SettingSTO Setting;

        // 初始化
        public void Initialize()
        {

#if UNITY_EDITOR
            Setting = AssetDatabase.LoadAssetAtPath("Assets/Cube-World-Editor/STO/SettingSTO.asset", typeof(SettingSTO)) as SettingSTO;
#endif
        }
    }
}
