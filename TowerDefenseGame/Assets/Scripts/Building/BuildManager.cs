using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Building
{
    public class BuildManager : MonoBehaviour
    {
        private static BuildManager _instance;
        public static BuildManager Instance { get => _instance; }

        [SerializeField] private List<GameObject> _towers;
        [SerializeField] private Transform _towerParent;

        private void Awake()
        {
            MakeSingleton();
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

        public void BuildTower(Vector3 position)
        {
            // show gui then instantiate

            /*GameObject tower =*/
            Instantiate(_towers[0], position, Quaternion.identity, _towerParent);
        }

        private void Start()
        {
        }

        private void Update()
        {
        }
    }
}