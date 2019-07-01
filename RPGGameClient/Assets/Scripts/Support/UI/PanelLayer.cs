/*
 * @Author: fasthro
 * @Date: 2019-06-20 18:05:39
 * @Description: panel 层
 */
namespace RPGGame
{
    public static class PanelLayer
    {
        public static int BASE_NUMBER = 100000000;

        class LayerInfo
        {
            private int m_layer;
            public int layer { get { return m_layer++; } }

            public LayerInfo(int layer)
            {
                m_layer = layer;
            }
        }

        // 场景层
        private static LayerInfo _Scene = new LayerInfo(1 * BASE_NUMBER);
        public static int Scene { get { return _Scene.layer; } }

        // 场景效果层
        private static LayerInfo _SceneEffect = new LayerInfo(2 * BASE_NUMBER);
        public static int SceneEffect { get { return _SceneEffect.layer; } }

        // 背景层
        private static LayerInfo _BG = new LayerInfo(3 * BASE_NUMBER);
        public static int BG { get { return _BG.layer; } }

        // 普通面板层
        private static LayerInfo _NormalPanel = new LayerInfo(4 * BASE_NUMBER);
        public static int NormalPanel { get { return _NormalPanel.layer; } }

        // 弹出层
        private static LayerInfo _Pop = new LayerInfo(5 * BASE_NUMBER);
        public static int Pop { get { return _Pop.layer; } }

        // 引导层
        private static LayerInfo _Guide = new LayerInfo(6 * BASE_NUMBER);
        public static int Guide { get { return _Guide.layer; } }

        // 对话框层
        private static LayerInfo _Dialog = new LayerInfo(7 * BASE_NUMBER);
        public static int Dialog { get { return _Dialog.layer; } }

        // 固定层
        private static LayerInfo _Const = new LayerInfo(8 * BASE_NUMBER);
        public static int Const { get { return _Const.layer; } }
        
        // 最上层
        private static LayerInfo _Forward = new LayerInfo(9 * BASE_NUMBER);
        public static int Forward { get { return _Forward.layer; } }
    }
}