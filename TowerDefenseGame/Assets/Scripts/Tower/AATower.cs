using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Level;

namespace TowerDefense.Tower
{
    public class AATower : MonoBehaviour
    {
        [Header("Tower Config")]
        [SerializeField] private TowerConfig towerConfig;

        private float _range;
        private Transform _target = null;

        private void Start()
        {
            _range = towerConfig.TowerRange;
        }

        private void Update()
        {
            if (_target == null)
            {
                UpdateNearestEnemy();
            }

            if (_target != null)
            {
                LookAtTarget();
                // fire

                if (Vector3.Distance(_target.position, transform.position) >= _range)
                {
                    UpdateNearestEnemy();
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

        private void LookAtTarget()
        {
            Vector3 direction = _target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, towerConfig.RotationSpeed * Time.deltaTime).eulerAngles;

            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}