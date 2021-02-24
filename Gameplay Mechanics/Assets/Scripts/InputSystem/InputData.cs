using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayMechanics.InputSystem
{
    [CreateAssetMenu(menuName = "Gameplay Mechanics/Input/InputData")]
    public class InputData : ScriptableObject
    {
        public float KeyboardHorizontal;
        public float KeyboardVertical;

        public float MouseX;
        public float MouseY;

        public void ProcessInput()
        {
            KeyboardHorizontal = Input.GetAxis("Horizontal");
            KeyboardVertical = Input.GetAxis("Vertical");

            MouseX = Input.GetAxis("Mouse X");
            MouseY = Input.GetAxis("Mouse Y");
        }
    }
}