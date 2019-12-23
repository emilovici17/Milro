using UnityEngine;

namespace Com.Emko.Inputs
{

    public class InputManager : MonoBehaviour
    {
        private static InputManager instance;
        
        [SerializeField] private Keybindings keybindings;

        public static InputManager Instance { get { return instance; } }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != null)
            {
                Destroy(this);
            }
            DontDestroyOnLoad(this);
        }

        public KeyCode GetKeyForAction(KeybindingAction action)
        {
            // Find keycode
            foreach(Keybindings.Keybinding keybinding in keybindings.keybindings)
            {
                if(keybinding.action == action)
                {
                    return keybinding.keycode;
                }
            }
            
            return KeyCode.None;
        }

        public bool GetKeyDown(KeybindingAction action) { return Input.GetKeyDown(GetKeyForAction(action)); }

        public bool GetKey(KeybindingAction action) { return Input.GetKey(GetKeyForAction(action)); }

        public bool GetKeyUp(KeybindingAction action) { return Input.GetKey(GetKeyForAction(action)); }
    }

}