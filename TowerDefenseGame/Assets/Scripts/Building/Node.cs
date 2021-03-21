using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense.Building
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private Color _hoverColor;

        [SerializeField] private GameObject _shopPanel;

        private GameObject _tower;

        private Renderer _renderer;
        private Color _startColor;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
            _startColor = _renderer.material.color;
        }

        private void OnMouseDown()
        {
            if (_tower != null)
            {
                Debug.Log("You can not place tower!");
                return;
            }

            if (!IsPointerOverGameObject())
            {
                BuildManager.Instance.OpenShopPanel(transform.position);
            }
        }

        private void OnMouseEnter()
        {
            transform.position += new Vector3(0, 1f, 0f);
            _renderer.material.color = _hoverColor;
        }

        private void OnMouseExit()
        {
            transform.position -= new Vector3(0, 1f, 0f);
            _renderer.material.color = _startColor;
        }

        public static bool IsPointerOverGameObject()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}