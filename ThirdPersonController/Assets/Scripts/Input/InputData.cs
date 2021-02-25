using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPlayer.InputSystem
{
    [CreateAssetMenu(menuName = "ThirdPersonController/Input/InputData")]
    public class InputData : ScriptableObject
    {
        public float Horizontal;
        public float Vertical;
        public float MouseX;
        public float MouseY;

        public void ProcessInput()
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");
        }
    }
}