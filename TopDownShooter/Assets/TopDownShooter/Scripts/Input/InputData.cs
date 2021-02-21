using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankShooter.PlayerInput
{
    [CreateAssetMenu(menuName = "TankShooter/Input/InputSettings")]
    public class InputData : ScriptableObject
    {
        public float Horizontal;
        public float Vertical;
        public float MouseX;
    }
}