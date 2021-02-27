using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Enemy
{
    [CreateAssetMenu(menuName = "SpaceTraveler/Wave/Wave Config")]
    public class WaveConfig : ScriptableObject
    {
        public GameObject EnemyPrefab = null;
        public GameObject PathPrefab = null;
        public float MovementSpeed = 2f;
        public int NumberOfEnemies = 7;
        public float RandomTimeFactor = 0.3f;
        public float TimeBetweenWaves = 1f;
        public float TimeBetweenSpawns = 0.5f;

        public List<Transform> GetWaypoints()
        {
            List<Transform> waypoints = new List<Transform>();

            int childCount = PathPrefab.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                waypoints.Add(PathPrefab.transform.GetChild(i));
            }

            return waypoints;
        }
    }
}