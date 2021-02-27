using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelGUIButtons : MonoBehaviour
{
    [Header("Power Up Shop")]
    [SerializeField] private ShopItems powerUps = null;

    [Header("Pop Up Panel")]
    [SerializeField] private GameObject popUpPanel = null;
    [SerializeField] private Button outsideClickButton = null;

    [Header("Level GUI")]
    [SerializeField] private GameObject levelGUI = null;

    [Header("Close Button")]
    [SerializeField] private Button closeGUIBtn = null;

    [Header("Power Up Buttons")]
    [SerializeField] private Button oneShotPowerUpBtn = null;
    [SerializeField] private Button twoShotPowerUpBtn = null;
    [SerializeField] private Button lifePowerUpBtn = null;

    private GameManager gameManager = null;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameManager.instance;
        SetButtonListeners();
        UpdateButtons();
    }

    private void SetButtonListeners()
    {
        closeGUIBtn.onClick.RemoveAllListeners();
        closeGUIBtn.onClick.AddListener(CloseGUI);

        oneShotPowerUpBtn.onClick.RemoveAllListeners();
        twoShotPowerUpBtn.onClick.RemoveAllListeners();
        lifePowerUpBtn.onClick.RemoveAllListeners();

        oneShotPowerUpBtn.onClick.AddListener(BuyOneShotPowerUp);
        twoShotPowerUpBtn.onClick.AddListener(BuyTwoShotPowerUp);
        lifePowerUpBtn.onClick.AddListener(BuyLifePowerUp);

        outsideClickButton.onClick.RemoveAllListeners();
        outsideClickButton.onClick.AddListener(ClickedOutside);
    }

    private void ClickedOutside()
    {
        levelGUI.SetActive(false);
        popUpPanel.SetActive(false);
    }

    private void UpdateButtons()
    {
        oneShotPowerUpBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + powerUps.GetItem(0).GetPrice();
        twoShotPowerUpBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + powerUps.GetItem(1).GetPrice();
        lifePowerUpBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + powerUps.GetItem(2).GetPrice();

        if (gameManager.OneShotPU)
        {
            oneShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
        }
        if (gameManager.TwoShotPU)
        {
            twoShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
        }
        if (gameManager.LifePU)
        {
            lifePowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private void CloseGUI()
    {
        popUpPanel.SetActive(false);
        levelGUI.SetActive(false);
    }

    private void BuyOneShotPowerUp()
    {
        int price = powerUps.GetItem(0).GetPrice();

        if (gameManager.OneShotPU)
        {
            Debug.Log("1");
            oneShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(false);
            gameManager.OneShotPU = false;
            gameManager.SaveData();

            gameManager.IncreaseDiamond(price);
            MainMenuManager.instance.UpdateCurrency();
            return;
        }

        if (!gameManager.CanEffort(price, true))
        {
            Debug.LogError("You don't have enough money to buy this item!");
            return;
        }

        oneShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
        gameManager.OneShotPU = true;
        gameManager.SaveData();

        gameManager.IncreaseDiamond(-price);
        MainMenuManager.instance.UpdateCurrency();
        Debug.Log("Powerup one bought!");
    }

    private void BuyTwoShotPowerUp()
    {
        int price = powerUps.GetItem(1).GetPrice();

        if (gameManager.TwoShotPU)
        {
            Debug.Log("2");
            twoShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(false);
            gameManager.TwoShotPU = false;
            gameManager.SaveData();

            gameManager.IncreaseDiamond(price);
            MainMenuManager.instance.UpdateCurrency();
            return;
        }

        if (!gameManager.CanEffort(price, true))
        {
            Debug.LogError("You don't have enough money to buy this item!");
            return;
        }

        twoShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
        gameManager.TwoShotPU = true;
        gameManager.SaveData();

        gameManager.IncreaseDiamond(-price);
        MainMenuManager.instance.UpdateCurrency();
        Debug.Log("Powerup two bought!");
    }

    private void BuyLifePowerUp()
    {
        int price = powerUps.GetItem(2).GetPrice();

        if (gameManager.LifePU)
        {
            Debug.Log("3");
            lifePowerUpBtn.transform.GetChild(3).gameObject.SetActive(false);
            gameManager.LifePU = false;
            gameManager.SaveData();

            gameManager.IncreaseDiamond(price);
            MainMenuManager.instance.UpdateCurrency();
            return;
        }

        if (!gameManager.CanEffort(price, true))
        {
            Debug.LogError("You don't have enough money to buy this item!");
            return;
        }

        lifePowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
        gameManager.LifePU = true;
        gameManager.SaveData();

        gameManager.IncreaseDiamond(-price);
        MainMenuManager.instance.UpdateCurrency();
        Debug.Log("Powerup life bought!");
    }
}