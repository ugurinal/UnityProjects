﻿using System.Collections;
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

        //private Vector3 _towerPosition;
        private Node _node; // which node is selected

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

        public void OpenShopPanel(Node node)
        {
            //_towerPosition = node position;
            _node = node;
            _shopPanel.transform.position = node.transform.position + new Vector3(0f, 8f, 0f);
            _shopPanel.SetActive(true);
        }

        public void CloseShopPanel()
        {
            _shopPanel.SetActive(false);
        }

        private void CreateTowerOne()
        {
            _node.SetTower(Instantiate(_towers[0], _node.transform.position, Quaternion.Euler(new Vector3(0f, -90f, 0f)), _towerParent));
            _shopPanel.SetActive(false);
        }

        private void CreateTowerTwo()
        {
            _node.SetTower(Instantiate(_towers[1], _node.transform.position, Quaternion.Euler(new Vector3(0f, -90f, 0f)), _towerParent));
            _shopPanel.SetActive(false);
        }

        private void CreateTowerThree()
        {
            _node.SetTower(Instantiate(_towers[2], _node.transform.position, Quaternion.Euler(new Vector3(0f, -90f, 0f)), _towerParent));
            _shopPanel.SetActive(false);
        }
    }
}