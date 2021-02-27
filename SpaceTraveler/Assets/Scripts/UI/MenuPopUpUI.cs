using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuPopUpUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject canvas = null;

    [Header("Pop Up Panels")]
    [SerializeField] private GameObject popUpPanel = null;

    [Header("Outside Click")]
    //[SerializeField] private GameObject outsidePanel = null;
    [SerializeField] private Button outsideClick = null;
    [SerializeField] private GameObject clickPrefab = null;

    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsUI = null;
    [SerializeField] private Button openSettingsButton = null;
    [SerializeField] private Button closeSettingsButton = null;

    [Header("Shop Panel")]
    [SerializeField] private GameObject shopUI = null;
    [SerializeField] private Button openShopButton = null;
    [SerializeField] private Button closeShopButton = null;

    [Header("Confirm GUI")]
    [SerializeField] private GameObject confirmGUI = null;
    [SerializeField] private Button confirmButton = null;
    [SerializeField] private Button cancelButton = null;

    [Header("Shop Item DataBase")]
    [SerializeField] private GameObject[] shopItems = null;
    [SerializeField] private ShopItems shopSO = null;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        SetButtons();
        SetShopItems();
        SetPurchasedShips();
    }

    private void SetButtons()
    {
        //outside click
        outsideClick.onClick.RemoveAllListeners();
        outsideClick.onClick.AddListener(() => ClickedOutside(Input.mousePosition));

        //settings ui buttons
        openSettingsButton.onClick.RemoveAllListeners();
        closeSettingsButton.onClick.RemoveAllListeners();
        openSettingsButton.onClick.AddListener(OpenSettings);
        closeSettingsButton.onClick.AddListener(CloseSettings);

        //shop ui buttons
        openShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);

        // Purchase and use button on click event
        for (int i = 0; i < shopItems.Length; i++)
        {
            int index = i;
            // https://docs.microsoft.com/tr-tr/archive/blogs/ericlippert/closing-over-the-loop-variable-considered-harmful
            //https://answers.unity.com/questions/912202/buttononclicaddlistenermethodobject-wrong-object-s.html
            // if we use i variable in delegate function, No matter what button we click,
            //Purchase ship is always called with the last element of the array,
            //not the one corresponding to the button I clicked.
            // so it would always call PurhcaseShip(i which is last lets say 3) with everybutton.

            Button purchaseButton = shopItems[i].transform.GetChild(2).GetComponent<Button>();
            Button useButton = shopItems[i].transform.GetChild(3).GetComponent<Button>();

            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(delegate
            {
                PurchaseShip(index);
            });

            useButton.onClick.RemoveAllListeners();
            useButton.onClick.AddListener(delegate
            {
                SelectShip(index);
            });
        }
    }

    private void OpenSettings()
    {
        popUpPanel.SetActive(true);
        settingsUI.SetActive(true);
    }

    private void CloseSettings()
    {
        popUpPanel.SetActive(false);
        settingsUI.SetActive(false);
    }

    private void OpenShop()
    {
        popUpPanel.SetActive(true);
        shopUI.SetActive(true);
    }

    private void CloseShop()
    {
        popUpPanel.SetActive(false);
        shopUI.SetActive(false);
    }

    private void SetPurchasedShips()
    {
        List<int> purchasedShips = gameManager.PurchasedShips;

        for (int i = 0; i < purchasedShips.Count; i++)
        {
            shopItems[purchasedShips[i]].transform.GetChild(2).gameObject.SetActive(false);
            shopItems[purchasedShips[i]].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private void SetShopItems()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopSO.GetItem(i).IsDiamond())
            {
                shopItems[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
                shopItems[i].transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
            }

            // The code below gets the data from scriptable object and adjust the shop items in the shop like price, name etc
            shopItems[i].transform.GetChild(2).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = shopSO.GetItem(i).GetPrice().ToString();
            shopItems[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = shopSO.GetItem(i).GetName();
        }

        UpdateUseButton();
    }

    public void PurchaseShip(int index)
    {
        int shipPrice = shopSO.GetItem(index).GetPrice();
        bool isDiamond = shopSO.GetItem(index).IsDiamond();

        // Check if player can effort, if he can then progress else not
        if (!gameManager.CanEffort(shipPrice, isDiamond))
        {
            // play not enogh money anim
            Debug.Log("Not enough money broo get some money");
            return;
        }

        OpenConfirmationWindow(index, shipPrice, isDiamond);
    }

    public void SelectShip(int index)
    {
        int previousShip = gameManager.SelectedShip;
        shopItems[previousShip].transform.GetChild(3).GetComponent<Button>().interactable = true;
        shopItems[previousShip].transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "USE";

        // selects ship to spawn in levels
        gameManager.SelectShip(index);

        UpdateUseButton();
    }

    public void ClickedOutside(Vector3 clickPosition)
    {
        if (settingsUI.activeSelf)
            CloseSettings();
        if (shopUI.activeSelf)
            CloseShop();

        Vector3 temp = Camera.main.ScreenToWorldPoint(clickPosition);
        temp.z = 0;

        Instantiate(clickPrefab, temp, Quaternion.identity, canvas.transform);
        //clickAnim.transform.SetParent(canvas.transform);
    }

    private void UpdateUseButton()
    {
        int selectedShip = gameManager.SelectedShip;

        shopItems[selectedShip].transform.GetChild(3).GetComponent<Button>().interactable = false;
        shopItems[selectedShip].transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "USING";
    }

    private void OpenConfirmationWindow(int index, int shipPrice, bool isDiamond)
    {
        confirmGUI.SetActive(true);

        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener(() => ConfirmPurchase(index, shipPrice, isDiamond));
        cancelButton.onClick.AddListener(CancelPurchase);
    }

    private void ConfirmPurchase(int index, int shipPrice, bool isDiamond)
    {
        // this function purchases the ship and updates the player currency in player prefs
        gameManager.PurchaseShip(index, shipPrice, isDiamond);

        // play money spend anim

        // this updates currency UI in main menu
        MainMenuManager.instance.UpdateCurrency();

        // this function deactives purchase button and activates use button instead
        shopItems[index].transform.GetChild(2).gameObject.SetActive(false);
        shopItems[index].transform.GetChild(3).gameObject.SetActive(true);

        confirmGUI.SetActive(false);
    }

    private void CancelPurchase()
    {
        confirmGUI.SetActive(false);
    }
}