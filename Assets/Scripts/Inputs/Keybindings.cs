using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Emko.Inputs
{
    [CreateAssetMenu(fileName = "Keybinding", menuName = "Keybindings")]
    public class Keybindings : ScriptableObject
    {
        [System.Serializable]
        public class Keybinding
        {
            public KeybindingAction action;
            public KeyCode keycode;
        }

        public Keybinding[] keybindings;
    }
}