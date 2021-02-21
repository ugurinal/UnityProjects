using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankShooter.PlayerControls
{
    [CreateAssetMenu(menuName = "TankShooter/Player/PlayerMovementSettings")]
    public class PlayerMovementSettings : ScriptableObject
    {
        [Header("Player Speed")]
        public float HorizontalSpeed = 5f;

        public float VerticalSpeed = 5f;
    }
}