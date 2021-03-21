using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Building
{
    public class BuildManager : MonoBehaviour
    {
        private static BuildManager _instance;
        public static BuildManager Instance { get => _instance; }

        [SerializeField] private List<GameObject> _towers;
        [SerializeField] private Transform _towerParent;
        [SerializeField] private GameObject _shopPanel;

        private Vector3 _towerPosition;

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
            Instantiate(_towers[0], position, Quaternion.identity, _towerParent);
        }

        private void Start()
        {
            _shopPanel.SetActive(false);

            Button towerOne = _shopPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>();
            Button towerTwo = _shopPanel.transform.GetChild(0).GetChild(1).GetComponent<Button>();
            Button towerThree = _shopPanel.transform.GetChild(0).GetChild(2).GetComponent<Button>();

            towerOne.onClick.RemoveAllListeners();
            towerTwo.onClick.RemoveAllListeners();
            towerThree.onClick.RemoveAllListeners();

            towerOne.onClick.AddListener(() => CreateTowerOne());
            towerTwo.onClick.AddListener(() => CreateTowerTwo());
            towerThree.onClick.AddListener(() => CreateTowerThree());
        }

        public void OpenShopPanel(Vector3 position)
        {
            _towerPosition = position;
            _shopPanel.transform.position = position + new Vector3(0f, 8f, 0f);
            _shopPanel.SetActive(true);
        }

        public void CloseShopPanel()
        {
            _shopPanel.SetActive(false);
        }

        private void CreateTowerOne()
        {
            Instantiate(_towers[0], _towerPosition, Quaternion.identity, _towerParent);
            _shopPanel.SetActive(false);
        }

        private void CreateTowerTwo()
        {
            Instantiate(_towers[1], _towerPosition, Quaternion.identity, _towerParent);
            _shopPanel.SetActive(false);
        }

        private void CreateTowerThree()
        {
            Instantiate(_towers[2], _towerPosition, Quaternion.identity, _towerParent);
            _shopPanel.SetActive(false);
        }
    }
}