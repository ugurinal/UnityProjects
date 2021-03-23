using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    public static PlayerStats Instance { get => _instance; }

    [SerializeField] private Text _moneyText;
    [SerializeField] private int _startMoney = 400;

    private int _money;
    public int Money { get => _money; }

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
        _money = _startMoney;
        _moneyText.text = "$" + _money;
    }

    public void PurchaseTower(int money)
    {
        _money -= money;

        if (_money <= 0)
        {
            Debug.Log("Money is less than 0 !");
        }
        _moneyText.text = "$" + _money;
    }
}