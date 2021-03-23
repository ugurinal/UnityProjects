using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _startMoney = 400;
    private static int _money;

    public static int Money { get => _money; }

    private void Start()
    {
        _money = _startMoney;
    }

    public static void PurchaseTower(int money)
    {
        _money -= money;

        if (_money <= 0)
        {
            Debug.Log("Money is less than 0 !");
        }
    }
}