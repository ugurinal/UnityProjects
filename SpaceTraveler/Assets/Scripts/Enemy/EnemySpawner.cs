using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceTraveler.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        #region DESCRIPTION

        //  *********************************************************************************************
        //  * This is the main class that takes care of spawning enemies                                *
        //  * This script also prints wave name to the screen                                           *
        //  *********************************************************************************************

        #endregion DESCRIPTION

        #region FIELDS

        public static EnemySpawner instance = null;

        [Header("Waves")]
        [SerializeField] private List<WaveConfig> waveConfigs = null;

        [Header("Wave Name")]
        [Space(20)]
        [SerializeField] private TextMeshProUGUI wave = null;

        private LevelController levelController = null;

        #endregion FIELDS

        private void Awake()
        {
            SetInstance();
        }

        private void Start()
        {
            levelController = LevelController.instance;
            UpdateEnemySize();
        }

        private void SetInstance()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        // this function update the enemy size in level controller
        private void UpdateEnemySize()
        {
            int enemySize = 0;
            foreach (var waves in waveConfigs)
            {
                enemySize += waves.NumberOfEnemies;
            }

            levelController.SetEnemySize(enemySize);
        }

        // starts enemy spawn
        public void StartEnemySpawn()
        {
            StartCoroutine(SpawnAllWaves());
        }

        private IEnumerator SpawnAllWaves()
        {
            int i = 1;
            foreach (var waves in waveConfigs)
            {
                StartCoroutine(PrintWaveName(i, waveConfigs.Count));    // print wave name
                StartCoroutine(SpawnEnemiesInWaves(waves));             // start spawning

                yield return new WaitForSeconds(waves.TimeBetweenWaves);
                i++;
            }
        }

        private IEnumerator SpawnEnemiesInWaves(WaveConfig wave)
        {
            for (int i = 0; i < wave.NumberOfEnemies; i++)
            {
                var enemy = Instantiate(wave.EnemyPrefab, wave.GetWaypoints()[0].transform.position, Quaternion.identity);
                enemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);
                yield return new WaitForSeconds(wave.TimeBetweenSpawns);
            }
        }

        private IEnumerator PrintWaveName(int currenWave, int maxWave)
        {
            for (int i = 0; i < 3; i++)
            {
                wave.text = "Wave " + currenWave + "/" + maxWave;
                wave.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.4f);
                wave.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.4f);
            }
        }
    }
}