/*
 * @Author: fasthro
 * @Date: 2019-06-18 11:16:05
 * @Description: 地图
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGGame.Editor.ABEditor
{
    public class ABMapDataDefine : ABBase
    {
        public ABMapDataDefine()
        {
            abStructure = ABStructure.SubfolderEntire;
            target = "Assets/Art/Maps";
            pattern = "*";
            bundlePath = "maps/datas";
        }
    }
}

