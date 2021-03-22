using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Level;
using TowerDefense.Enemy;

namespace TowerDefense.Damage
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private GameObject _projectileVFX;
        [SerializeField] private float _explosionRadius;
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 direction = _target.position - transform.position;
            float distanceThisFrame = _projectileSpeed * Time.deltaTime;

            if (direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(direction.normalized * distanceThisFrame, Space.World);

            transform.LookAt(_target);
        }

        private void HitTarget()
        {
            Debug.Log("HIT - HIT - HIT - HIT - HIT - HIT");
            GameObject impactVFX = Instantiate(_projectileVFX, transform.position, transform.rotation);
            Destroy(impactVFX, 1f);

            if (_explosionRadius > 0)
            {
                Explode();
            }
            else
            {
                Enemy.Enemy enemy = _target.GetComponent<Enemy.Enemy>();

                if (enemy != null)
                {
                    enemy.TakeDamage(100);
                }
                else
                {
                    Debug.Log("Enemy is null !");
                }
            }

            Destroy(gameObject);
        }

        private void Explode()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (Collider go in hits)
            {
                Enemy.Enemy enemy = go.GetComponent<Enemy.Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(100);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}