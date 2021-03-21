using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense.Building
{
    public class Ground : MonoBehaviour
    {
        private void OnMouseDown()
        {
            if (!IsPointerOverGameObject())
            {
                BuildManager.Instance.CloseShopPanel();
            }
        }

        public static bool IsPointerOverGameObject()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}