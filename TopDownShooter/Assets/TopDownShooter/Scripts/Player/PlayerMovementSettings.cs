using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankShooter.PlayerMovement
{
    [CreateAssetMenu(menuName = "TankShooter/Player/PlayerMovementSettings")]
    public class PlayerMovementSettings : ScriptableObject
    {
        public float HorizontalSpeed = 5f;
        public float VerticalSpeed = 5f;
    }
}