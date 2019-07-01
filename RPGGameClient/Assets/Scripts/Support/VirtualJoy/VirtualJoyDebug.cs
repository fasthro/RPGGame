using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{
    public class VirtualJoyDebug
    {
		public delegate void OnGUIDelegate();

        public static OnGUIDelegate OnGUIHandlers;
    }
}
