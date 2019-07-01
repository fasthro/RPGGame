using UnityEngine;

namespace RPGGame
{
    public class Game {
        
        // 游戏管理对象
        private static GameObject m_manager = null;
        public static GameObject manager {
            get {
                if (m_manager == null)
                {
                    m_manager = GameObject.FindWithTag("MainGame");
                }
                return m_manager;
            }
        }

        // main game
        private static MainGame m_mainGame = null;
        public static MainGame mainGame {
            get {
                if (m_mainGame == null)
                {
                    m_mainGame = manager.GetComponent<MainGame>();
                }
                return m_mainGame;
            }
        }

        // virtual joy
        private static VirtualJoy m_virtualJoy = null;
        public static VirtualJoy virtualJoy {
            get {
                if (m_virtualJoy == null)
                {
                    m_virtualJoy = mainGame.transform.Find("VirtualJoy").GetComponent<VirtualJoy>();
                }
                return m_virtualJoy;
            }
        }

        // game csv
        private static GameCSV m_gameCSV = null;
        public static GameCSV gameCSV {
            get {
                if (m_gameCSV == null)
                {
                    m_gameCSV = mainGame.transform.Find("GameCSV").GetComponent<GameCSV>();
                }
                return m_gameCSV;
            }
        }
    }
}
