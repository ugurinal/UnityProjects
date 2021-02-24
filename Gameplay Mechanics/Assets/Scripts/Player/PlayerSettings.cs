using UnityEngine;

namespace GameplayMechanics.Player
{
    [CreateAssetMenu(menuName = "Gameplay Mechanics/Player/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        public float PlayerSpeed = 5f;
    }
}