using Com.Emko.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Emko.Testing
{
    public class CubeMove : MonoBehaviour
    {
        private InputManager inputManager;

        void Start()
        {
            inputManager = InputManager.Instance;
        }

        private void Update()
        {
            if (inputManager.GetKeyDown(KeybindingAction.Jump))
            {
                this.transform.Translate(0, 1, 0);
            }
        }
    }
}
