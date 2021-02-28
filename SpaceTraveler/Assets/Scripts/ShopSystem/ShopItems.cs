using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.ShopSystem
{
    [CreateAssetMenu(menuName = "SpaceTraveler/Shop/ShopItems")]
    public class ShopItems : ScriptableObject
    {
        [System.Serializable]
        public class Items
        {
            public string Name = null;
            public int Price = 0;
            public bool IsDiamond = false;

            public GameObject ShipPrefab = null;
        }

        public List<Items> _ShopItems = null;
    }
}