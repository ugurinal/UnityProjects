using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Level;

namespace TowerDefense.Enemy
{
    public class EnemyPathing : MonoBehaviour
    {
        private List<Transform> _waypoints;
        private int _currentIdx;

        private float _movementSpeed;

        private void Start()
        {
            _currentIdx = 0;
        }

        public void SetWaypoints(List<Transform> waypoints)
        {
            _waypoints = waypoints;
        }

        public float Speed { get => _movementSpeed; set => _movementSpeed = value; }

        private void Update()
        {
            HandleMovement();
            UpdateWaypoint();
        }

        private void HandleMovement()
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentIdx].position, _movementSpeed * Time.deltaTime);
        }

        private void UpdateWaypoint()
        {
            if (Vector3.Distance(_waypoints[_currentIdx].position, transform.position) < 0.001f)
            {
                if (_currentIdx >= _waypoints.Count - 1)
                {
                    Debug.Log(transform.position);
                    LevelController.Instance.RemoveEnemy(gameObject);
                    Destroy(gameObject);
                    return;
                }

                _currentIdx++;
            }
        }
    }
}