using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankShooter.PlayerInput
{
    [CreateAssetMenu(menuName = "TankShooter/Input/InputSettings")]
    public class InputData : ScriptableObject
    {
        [Header("Keyboard Controller")]
        public float Horizontal;
        public float Vertical;

        [Header("Tower Rotation Axis Base Control")]
        [Space(10f)]
        [SerializeField] private bool _mouseRotationActive;

        [Header("Movement Axis Base Control")]
        [Space(10f)]
        [SerializeField] private bool _movementAxisActive;
        [SerializeField] private string _horizontalAxisName;
        [SerializeField] private string _verticalAxisName;

        [Header("Key Base Control")]
        [SerializeField] private bool _keyBaseHorizontalActive;
        [SerializeField] private KeyCode PositiveHorizontalKeyCode;
        [SerializeField] private KeyCode NegativeHorizontalKeyCode;
        [SerializeField] private bool _keyBaseVerticalActive;
        [SerializeField] private KeyCode PositiveVerticalKeyCode;
        [SerializeField] private KeyCode NegativeVerticalKeyCode;
        [SerializeField] private float _increaseAmount = 0.015f;

        public void ProcessInput()
        {
            if (_movementAxisActive)
            {
                Horizontal = Input.GetAxis(_horizontalAxisName);
                Vertical = Input.GetAxis(_verticalAxisName);
            }
            else
            {
                if (_mouseRotationActive)
                {
                    Horizontal = Input.GetAxis("Mouse X");
                    Vertical = Input.GetAxis("Mouse Y");
                }
                else
                {
                    if (_keyBaseHorizontalActive)
                    {
                        KeyBaseAxisControl(ref Horizontal, PositiveHorizontalKeyCode, NegativeHorizontalKeyCode);
                    }
                    if (_keyBaseVerticalActive)
                    {
                        KeyBaseAxisControl(ref Vertical, PositiveVerticalKeyCode, NegativeVerticalKeyCode);
                    }
                }
            }
        }

        private void KeyBaseAxisControl(ref float value, KeyCode positive, KeyCode negative)
        {
            bool positiveActive = Input.GetKey(positive);
            bool negativeActive = Input.GetKey(negative);

            if (positiveActive)
            {
                value += _increaseAmount;
            }
            else if (negativeActive)
            {
                value -= _increaseAmount;
            }
            else
            {
                value = 0;
            }
            value = Mathf.Clamp(value, -1, 1);
        }
    }
}