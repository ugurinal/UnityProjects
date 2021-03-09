using SpaceTraveler.LevelSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceTraveler.Enemy
{
    #region DESCRIPTION

    /// <summary>
    /// This is the main class that takes care of spawning enemies
    /// This script also prints wave name to the screen
    /// </summary>

    #endregion DESCRIPTION

    public class EnemySpawner : MonoBehaviour
    {
        #region FIELDS

        private static EnemySpawner _instance = null;
        public static EnemySpawner Instance { get => _instance; }

        [Header("Waves")]
        [SerializeField] private List<WaveConfig> _waveConfigs = null;

        [Header("Wave Name")]
        [Space(20)]
        [SerializeField] private TextMeshProUGUI _waveTMP = null;

        [SerializeField] private Transform enemyParent;

        private LevelController _levelController = null;

        #endregion FIELDS

        private void Awake()
        {
            MakeSingleton();
        }

        private void Start()
        {
            _levelController = LevelController.Instance;
            UpdateEnemySize();
        }

        private void MakeSingleton()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        /// <summary>
        /// this function update the enemy size in level controller
        /// </summary>
        private void UpdateEnemySize()
        {
            int enemySize = 0;
            foreach (var waves in _waveConfigs)
            {
                enemySize += waves.NumberOfEnemies;
            }

            _levelController.SetEnemySize(enemySize);
        }

        /// <summary>
        /// starts spawning enemies
        /// </summary>
        public void StartEnemySpawn()
        {
            StartCoroutine(SpawnAllWaves());
        }

        private IEnumerator SpawnAllWaves()
        {
            int i = 1;
            foreach (var waves in _waveConfigs)
            {
                StartCoroutine(PrintWaveName(i, _waveConfigs.Count));    // print wave name
                StartCoroutine(SpawnEnemiesInWaves(waves));             // start spawning

                yield return new WaitForSeconds(waves.TimeBetweenWaves);
                i++;
            }
        }

        private IEnumerator SpawnEnemiesInWaves(WaveConfig wave)
        {
            for (int i = 0; i < wave.NumberOfEnemies; i++)
            {
                GameObject enemy = Instantiate(wave.EnemyPrefab, wave.GetWaypoints()[0].transform.position, Quaternion.identity, enemyParent);
                enemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);
                yield return new WaitForSeconds(wave.TimeBetweenSpawns);
            }
        }

        private IEnumerator PrintWaveName(int currenWave, int maxWave)
        {
            for (int i = 0; i < 3; i++)
            {
                _waveTMP.text = "Wave " + currenWave + "/" + maxWave;
                _waveTMP.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.4f);
                _waveTMP.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}