using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Enemy
{
    public class EnemyPathing : MonoBehaviour
    {
        private List<Transform> _waypoints;

        public void SetWaypoints(List<Transform> waypoints)
        {
            _waypoints = waypoints;
        }
    }
}