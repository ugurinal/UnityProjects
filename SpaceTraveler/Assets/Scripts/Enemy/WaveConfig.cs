using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Wave Config")]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private GameObject enemyPrefab = null;
        [SerializeField] private GameObject pathPrefab = null;
        [SerializeField] private float timeBetweenSpawns = 0.5f;
        [SerializeField] private float randomTimeFactor = 0.3f;
        [SerializeField] private int numberOfEnemies = 7;
        [SerializeField] private float _enemyMovementSpeed = 2f;
        [SerializeField] private float timeBetweenWaves = 1f;

        public GameObject EnemyPrefab { get => enemyPrefab; }

        public List<Transform> GetWaypoints()
        {
            List<Transform> waypoints = new List<Transform>();

            int childCount = pathPrefab.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                waypoints.Add(pathPrefab.transform.GetChild(i));
            }

            return waypoints;
        }

        public float TimeBetweenSpawns { get => timeBetweenSpawns; }
        public float RandomTimeFactor { get => randomTimeFactor; }
        public int NumberOfEnemies { get => numberOfEnemies; }
        public float MoveSpeed { get => _enemyMovementSpeed; }

        public float TimeBetweenWaves { get => timeBetweenWaves; }
    }
}