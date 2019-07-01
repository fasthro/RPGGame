/*
 * @Author: fasthro
 * @Date: 2019-06-20 14:45:07
 * @Description: 面板接口
 */
namespace RPGGame
{
    public interface IPanel
    {
        void Preload();
        void OpenPanel(IPanelData data);
        void ShowPanel();
        void HidePanel();
        void ClosePanel();
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}