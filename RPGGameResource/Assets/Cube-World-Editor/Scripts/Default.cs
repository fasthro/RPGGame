using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubeWorldEditor
{
    /// <summary>
    /// 场景工具类型
    /// </summary>
    public enum SceneToolsType
    {
        None,
        // 选择
        Selector,
        // 笔刷                
        Brush,
        // 吸管                  
        Sucker,
        // 擦除
        Erase,
    }

    /// <summary>
    /// 场景所需材料类型
    /// </summary>
    public enum MaterialType
    {
        // Cube
        Cube,
        // 装饰物    
        Ornament,
    }

    /// <summary>
    /// 场景渲染状态
    /// </summary>
    public enum SceneRenderState
    {
        Exit,
        Enter,
        Render,
    }

    /// <summary>
    /// 选择器工具状态
    /// </summary>
    public enum SelectorToolsState
    {
        Unselected,
        Selected,
    }


    /// <summary>
    /// 周围方向
    /// </summary>
    public enum AroundDirection
    {
        PositiveX,
        NegativeX,
        PositiveZ,
        NegativeZ,
    }
}

