using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Level;

namespace TowerDefense.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> _waveConfig;
        [SerializeField] private Vector3 _startPosition;

        [SerializeField] private Transform _enemyParent;

        private void Start()
        {
            SetEnemySize();
            StartCoroutine(StartEnemyWaves());
        }

        private void SetEnemySize()
        {
            int enemySize = 0;
            for (int i = 0; i < _waveConfig.Count; i++)
            {
                enemySize += _waveConfig[i].NumberOfEnemies;
            }

            LevelController.Instance.SetEnemySize(enemySize);
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
                EnemyPathing enemy = Instantiate(wave.EnemyPrefab, _startPosition, Quaternion.identity, _enemyParent).GetComponent<EnemyPathing>();
                enemy.SetWaypoints(wave.GetWayPoints());
                enemy.Speed = wave.EnemySpeed;

                LevelController.Instance.AddEnemyList(enemy.gameObject);
                yield return new WaitForSeconds(wave.EnemySpawnSpeed);
            }
        }
    }
}