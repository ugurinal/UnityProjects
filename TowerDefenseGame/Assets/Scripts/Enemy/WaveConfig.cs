using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Enemy
{
    [CreateAssetMenu(menuName = "Tower Defense/Enemy/Wave Config")]
    public class WaveConfig : ScriptableObject
    {
        public Transform Path;
        public GameObject EnemyPrefab;
        public int NumberOfEnemies;
        public float EnemySpawnSpeed;
        public float EnemySpeed;
        public float NextWaveTime;

        public List<Transform> GetWayPoints()
        {
            List<Transform> waypoints = new List<Transform>();

            for (int i = 0; i < Path.childCount; i++)
            {
                waypoints.Add(Path.GetChild(i));
            }

            return waypoints;
        }
    }
}