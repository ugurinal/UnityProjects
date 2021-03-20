using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> _waveConfig;
        [SerializeField] private Vector3 _startPosition;

        private void Start()
        {
            StartCoroutine(StartEnemyWaves());
        }

        private IEnumerator StartEnemyWaves()
        {
            for (int i = 0; i < _waveConfig.Count; i++)
            {
                StartCoroutine(StartSpawningEnemies(_waveConfig[i]));
                yield return new WaitForSeconds(_waveConfig[i].NextWaveTime);
            }
        }

        private IEnumerator StartSpawningEnemies(WaveConfig wave)
        {
            for (int i = 0; i < wave.NumberOfEnemies; i++)
            {
                Instantiate(wave.EnemyPrefab, _startPosition, Quaternion.identity);
                yield return new WaitForSeconds(wave.EnemySpawnSpeed);
            }
        }
    }
}