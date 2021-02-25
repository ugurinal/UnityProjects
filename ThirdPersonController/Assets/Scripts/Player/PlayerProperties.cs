using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPlayer.Player
{
    [CreateAssetMenu(menuName = "ThirdPersonController/Player/PlayerProperties")]
    public class PlayerProperties : ScriptableObject
    {
        public float MovementSpeed = 5f;
        public float turnSmoothtime = 0.1f;
    }
}