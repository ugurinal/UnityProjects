using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance = null;

    [SerializeField] private GameObject avatarArea = null;

    [SerializeField] private List<TextMeshProUGUI> coinTexts = null;
    [SerializeField] private List<TextMeshProUGUI> diaTexts = null;

    private int diamondCounter = 0;
    private int coinCounter = 0;

    // Start is called before the first frame update
    private void Start()
    {
        SetInstance();

        SetCoinAndDiamond();

        TextMeshProUGUI nicknameTMP = avatarArea.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        nicknameTMP.text = GameManager.instance.PlayerName;

        for (int i = 0; i < coinTexts.Count; i++)
        {
            coinTexts[i].text = coinCounter.ToString();
            diaTexts[i].text = diamondCounter.ToString();
        }
    }

    private void SetCoinAndDiamond()
    {
        coinCounter = GameManager.instance.Coin;
        diamondCounter = GameManager.instance.Diamond;
    }

    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void UpdateCurrency()
    {
        SetCoinAndDiamond();

        for (int i = 0; i < coinTexts.Count; i++)
        {
            coinTexts[i].text = coinCounter.ToString();
            diaTexts[i].text = diamondCounter.ToString();
        }
    }
}