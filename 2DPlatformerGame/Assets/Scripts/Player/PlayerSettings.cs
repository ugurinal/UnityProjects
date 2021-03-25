using UnityEngine;

namespace Platformer2D.Player
{
    [CreateAssetMenu(menuName = "Platformer2D/Player/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        public float MovementSpeed = 5f;
        public float JumpSpeed = 5f;
    }
}