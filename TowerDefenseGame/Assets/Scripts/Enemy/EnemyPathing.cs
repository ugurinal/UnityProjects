using System.Collections.Generic;
using UnityEngine;

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
            if (_currentIdx >= _waypoints.Count)
            {
                Debug.Log("Decrease Life!");
                return;
            }

            if (Vector3.Distance(transform.position, _waypoints[_currentIdx].position) <= 0.001f)
            {
                _currentIdx++;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentIdx].position, _movementSpeed * Time.deltaTime);
            }
        }
    }
}