using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Level;

namespace TowerDefense.Damage
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private GameObject _projectileVFX;
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target == null)
                return;

            Vector3 direction = _target.position - transform.position;
            float distanceThisFrame = _projectileSpeed * Time.deltaTime;

            if (direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }

        private void HitTarget()
        {
            Debug.Log("HIT - HIT - HIT - HIT - HIT - HIT");
            GameObject impactVFX = Instantiate(_projectileVFX, transform.position, transform.rotation);
            Destroy(impactVFX, 1f);

            LevelController.Instance.RemoveEnemy(_target.gameObject);
            Destroy(_target.gameObject);
            Destroy(gameObject);
        }
    }
}