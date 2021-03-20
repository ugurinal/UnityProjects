using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Level;

namespace TowerDefense.Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float _range = 15f;
        [SerializeField] private Transform _target = null;

        private void Start()
        {
            UpdateNearestEnemy();
        }

        private void Update()
        {
            if (_target == null)
            {
                UpdateNearestEnemy();
            }

            if (_target != null)
            {
                if (Vector3.Distance(_target.position, transform.position) >= _range)
                {
                    UpdateNearestEnemy();
                }

                if (_target != null)
                {
                    // lock target
                }
            }
        }

        private void UpdateNearestEnemy()
        {
            List<GameObject> enemiesInScene = LevelController.Instance.Enemies;

            if (enemiesInScene == null)
                return;

            GameObject nearestEnemy = null;
            float shortestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemiesInScene)
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= _range)
            {
                _target = nearestEnemy.transform;
            }
            else
            {
                _target = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}