/*
 * @Author: fasthro
 * @Date: 2019-07-03 11:29:37
 * @Description: UI ID 定义
 */

namespace RPGGame
{
    public static class UIID
    {
        #region 面板定义
        
        public static int Init = 1;

        #endregion

        #region 获取面板
        public static AbstractPanel GetPanel(int uiId)
        {
            if (uiId == Init) return new UIInitPanel();
            return null;
        }
        #endregion 
    }
}