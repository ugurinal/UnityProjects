using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TowerDefense.Level;

namespace TowerDefense.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> _waveConfig;
        [SerializeField] private Vector3 _startPosition;

        [SerializeField] private Transform _enemyParent;
        [SerializeField] private Text _countdownText;

        private float _countdown;

        private void Start()
        {
            _countdown = 10f;

            SetEnemySize();
            StartCoroutine(StartEnemyWaves());
            _countdownText.text = string.Format("{0:00.00}", _countdown);
        }

        private void Update()
        {
            _countdown -= Time.deltaTime;
            _countdown = Mathf.Clamp(_countdown, 0, Mathf.Infinity);
            _countdownText.text = string.Format("{0:00.00}", _countdown);
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
            yield return new WaitForSeconds(10f);
            for (int i = 0; i < _waveConfig.Count; i++)
            {
                StartCoroutine(StartSpawningEnemies(_waveConfig[i]));
                _countdown = _waveConfig[i].NextWaveTime;
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