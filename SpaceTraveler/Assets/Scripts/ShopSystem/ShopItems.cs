using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Items")]
public class ShopItems : ScriptableObject
{
    [System.Serializable]
    public class Items
    {
        [SerializeField] private string name = "";
        [SerializeField] private int price = 0;
        [SerializeField] private bool isDiamond = false;

        [SerializeField] private GameObject shipPrefab = null;

        public bool IsDiamond()
        {
            return isDiamond;
        }

        public int GetPrice()
        {
            return price;
        }

        public GameObject GetShipPrefab()
        {
            return shipPrefab;
        }

        public string GetName()
        {
            return name;
        }
    }

    [SerializeField] public List<Items> shopItems = null;

    public Items GetItem(int index)
    {
        return shopItems[index];
    }
}