using UnityEngine;
using TowerDefense.Damage;
using TowerDefense.Level;

namespace TowerDefense.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;

        private float _health;

        private void Start()
        {
            _health = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;

            if (_health <= 0)
            {
                Debug.Log("Enemy health = 0 !");
                LevelController.Instance.RemoveEnemy(gameObject);
                Destroy(gameObject);
            }
        }
    }
}