using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Level
{
    public class LevelController : MonoBehaviour
    {
        private static LevelController _instance;

        public static LevelController Instance { get => _instance; }

        private List<GameObject> _enemies;
        public List<GameObject> Enemies { get => _enemies; }

        private int _enemySize;
        private int _enemyLeft;

        private void Awake()
        {
            MakeSingleton();
        }

        private void Start()
        {
            _enemies = new List<GameObject>();
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

        public void SetEnemySize(int enemySize)
        {
            _enemySize = enemySize;
            _enemyLeft = _enemySize;
        }

        public void AddEnemyList(GameObject enemy)
        {
            _enemies.Add(enemy);
            //Debug.Log("Enemy added!");
        }

        public void RemoveEnemy(GameObject enemy)
        {
            _enemies.Remove(enemy);
        }
    }
}