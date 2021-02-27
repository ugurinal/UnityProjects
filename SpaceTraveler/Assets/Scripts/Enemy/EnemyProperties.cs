using UnityEngine;

namespace SpaceTraveler.Enemy
{
    [CreateAssetMenu(menuName = "SpaceTraveler/Enemy/EnemyProperties")]
    public class EnemyProperties : ScriptableObject
    {
        [Header("Enemy Properties")]
        [Space(10)]
        public float MaxHealth = 100f;
        public int ScoreToAdd = 0;        // when this enemy die how much score will player get
        public int CoinToEarn = 0;        // when this enemy die how much coin will player earn

        [Header("Projectile Properties")]
        [Space(10)]
        public GameObject Projectile = null;          // projectile prefab that enemy shoots
        public float MinTimeBetweenShots = 0.2f;      // min time to shoot
        public float MaxTimeBetweenShots = 4f;        // max time to shoot
        public float ShotSpeed = 1.5f;                  // speed of projectile
    }
}