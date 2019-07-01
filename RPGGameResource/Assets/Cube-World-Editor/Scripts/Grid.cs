/*
 * @Author: fasthro
 * @Date: 2019-05-29 18:11:40
 * @Description: Cube Grid
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeWorldEditor
{
    public class Grid : MonoBehaviour
    {
        // key
        public string key;
        // 资源Id
        public string resId;
        // 所在地块
        public int areaIndex;
        // 位置
        public Vector3 position;
        // 位置偏移
        public Vector3 positionOffset;
        // 旋转角度
        public Vector3 angle;
        // 角度偏移
        public Vector3 angleOffset;
        // 缩放
        public Vector3 scale;
        // 缩放偏移
        public Vector3 scaleOffset;
        // 材料类型
        public MaterialType materialType;

        public void Initialize()
        {
            
        }

        public void Refresh()
        {
            transform.position = position + positionOffset + SettingManager.Inst.Setting.cubeAnchorOffset;
            transform.localEulerAngles = angle + angleOffset;
            transform.localScale = scale + scaleOffset;
        }
    }
}
