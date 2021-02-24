using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayMechanics.Enemy
{
    public class EnemySpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;

        private void Start()
        {
            Instantiate(_enemyPrefab, GenerateSpawnPosition(), Quaternion.identity);
        }

        private Vector3 GenerateSpawnPosition()
        {
            float spawnPosX = Random.Range(-7f, 7f);
            float spawnPosZ = Random.Range(-7f, 7f);

            Debug.Log(spawnPosX);
            Debug.Log(spawnPosZ);

            return new Vector3(spawnPosX, -1.5f, spawnPosZ);
        }
    }
}