using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankShooter.PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputData _movementInput;
        [SerializeField] private InputData _rotationInput;

        private void Update()
        {
            _movementInput.Horizontal = Input.GetAxis("Horizontal");
            _movementInput.Vertical = Input.GetAxis("Vertical");
            _rotationInput.MouseX = Input.GetAxis("Mouse X");
        }
    }
}