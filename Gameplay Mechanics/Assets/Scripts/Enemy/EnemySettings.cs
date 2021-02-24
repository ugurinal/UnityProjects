using UnityEngine;

namespace GameplayMechanics.Enemy
{
    [CreateAssetMenu(menuName = "Gameplay Mechanics/Enemy/EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        public float EnemySpeed = 300f;
    }
}