using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SpaceTraveler.ShopSystem;
using SpaceTraveler.ManagerSystem;

namespace SpaceTraveler.UISystem
{
    public class MenuPopUpUI : MonoBehaviour
    {
        [Header("Canvas")]
        [SerializeField] private GameObject _canvas = null;

        [Header("Pop Up Panels")]
        [SerializeField] private GameObject _popUpPanel = null;

        [Header("Outside Click")]
        [SerializeField] private Button _outsideClick = null;
        [SerializeField] private GameObject _clickPrefab = null;

        [Header("Settings Panel")]
        [SerializeField] private GameObject _settingsUI = null;
        [SerializeField] private Button _openSettingsButton = null;
        [SerializeField] private Button _closeSettingsButton = null;

        [Header("Shop Panel")]
        [SerializeField] private GameObject _shopUI = null;
        [SerializeField] private Button _openShopButton = null;
        [SerializeField] private Button _closeShopButton = null;

        [Header("Confirm GUI")]
        [SerializeField] private GameObject _confirmGUI = null;
        [SerializeField] private Button _confirmButton = null;
        [SerializeField] private Button _cancelButton = null;

        [Header("Shop Item DataBase")]
        [SerializeField] private GameObject[] _shopItemsInHieararchy = null;
        [SerializeField] private ShopItems _shopItemsSO = null;

        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.Instance;

            SetButtons();
            SetShopItems();
            SetPurchasedShips();
        }

        private void SetButtons()
        {
            //outside click
            _outsideClick.onClick.RemoveAllListeners();
            _outsideClick.onClick.AddListener(() => ClickedOutside(Input.mousePosition));

            //settings ui buttons
            _openSettingsButton.onClick.RemoveAllListeners();
            _closeSettingsButton.onClick.RemoveAllListeners();
            _openSettingsButton.onClick.AddListener(OpenSettings);
            _closeSettingsButton.onClick.AddListener(CloseSettings);

            //shop ui buttons
            _openShopButton.onClick.RemoveAllListeners();
            _closeShopButton.onClick.RemoveAllListeners();
            _openShopButton.onClick.AddListener(OpenShop);
            _closeShopButton.onClick.AddListener(CloseShop);

            // Purchase and use button on click event
            for (int i = 0; i < _shopItemsInHieararchy.Length; i++)
            {
                int index = i;
                // https://docs.microsoft.com/tr-tr/archive/blogs/ericlippert/closing-over-the-loop-variable-considered-harmful
                //https://answers.unity.com/questions/912202/buttononclicaddlistenermethodobject-wrong-object-s.html
                // if we use i variable in delegate function, No matter what button we click,
                //Purchase ship is always called with the last element of the array,
                //not the one corresponding to the button I clicked.
                // so it would always call PurhcaseShip(i which is last lets say 3) with everybutton.

                Button purchaseButton = _shopItemsInHieararchy[i].transform.GetChild(2).GetComponent<Button>();
                Button useButton = _shopItemsInHieararchy[i].transform.GetChild(3).GetComponent<Button>();

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
            _popUpPanel.SetActive(true);
            _settingsUI.SetActive(true);
        }

        private void CloseSettings()
        {
            _popUpPanel.SetActive(false);
            _settingsUI.SetActive(false);
        }

        private void OpenShop()
        {
            _popUpPanel.SetActive(true);
            _shopUI.SetActive(true);
        }

        private void CloseShop()
        {
            _popUpPanel.SetActive(false);
            _shopUI.SetActive(false);
        }

        private void SetPurchasedShips()
        {
            List<int> purchasedShips = _gameManager.PurchasedShips;

            for (int i = 0; i < purchasedShips.Count; i++)
            {
                _shopItemsInHieararchy[purchasedShips[i]].transform.GetChild(2).gameObject.SetActive(false);
                _shopItemsInHieararchy[purchasedShips[i]].transform.GetChild(3).gameObject.SetActive(true);
            }
        }

        private void SetShopItems()
        {
            for (int i = 0; i < _shopItemsInHieararchy.Length; i++)
            {
                if (_shopItemsSO._ShopItems[i].IsDiamond)
                {
                    _shopItemsInHieararchy[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
                    _shopItemsInHieararchy[i].transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
                }

                // The code below gets the data from scriptable object and updates the shop items in the shop like price, name etc
                _shopItemsInHieararchy[i].transform.GetChild(2).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = _shopItemsSO._ShopItems[i].Price.ToString();
                _shopItemsInHieararchy[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _shopItemsSO._ShopItems[i].Name;
            }

            UpdateUseButton();
        }

        public void PurchaseShip(int index)
        {
            int shipPrice = _shopItemsSO._ShopItems[index].Price;
            bool isDiamond = _shopItemsSO._ShopItems[index].IsDiamond;

            // Check if player can effort, if he can then progress else not
            if (!_gameManager.CanEffort(shipPrice, isDiamond))
            {
                // play not enogh money anim
                Debug.Log("Not enough money broo get some money");
                return;
            }

            OpenConfirmationWindow(index, shipPrice, isDiamond);
        }

        public void SelectShip(int index)
        {
            int previousShip = _gameManager.SelectedShip;
            _shopItemsInHieararchy[previousShip].transform.GetChild(3).GetComponent<Button>().interactable = true;
            _shopItemsInHieararchy[previousShip].transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "USE";

            // selects ship to spawn in levels
            _gameManager.SelectShip(index);

            UpdateUseButton();
        }

        public void ClickedOutside(Vector3 clickPosition)
        {
            if (_settingsUI.activeSelf)
                CloseSettings();
            if (_shopUI.activeSelf)
                CloseShop();
            if (_confirmGUI.activeSelf)
                _confirmGUI.SetActive(false);

            Vector3 temp = Camera.main.ScreenToWorldPoint(clickPosition);
            temp.z = 0;

            Instantiate(_clickPrefab, temp, Quaternion.identity, _canvas.transform);
            //clickAnim.transform.SetParent(canvas.transform);
        }

        private void UpdateUseButton()
        {
            int selectedShip = _gameManager.SelectedShip;

            _shopItemsInHieararchy[selectedShip].transform.GetChild(3).GetComponent<Button>().interactable = false;
            _shopItemsInHieararchy[selectedShip].transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "USING";
        }

        private void OpenConfirmationWindow(int index, int shipPrice, bool isDiamond)
        {
            _confirmGUI.SetActive(true);

            _confirmButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();

            _confirmButton.onClick.AddListener(() => ConfirmPurchase(index, shipPrice, isDiamond));
            _cancelButton.onClick.AddListener(CancelPurchase);
        }

        private void ConfirmPurchase(int index, int shipPrice, bool isDiamond)
        {
            // this function purchases the ship and updates the player currency in player prefs
            _gameManager.PurchaseShip(index, shipPrice, isDiamond);

            // play money spend anim

            // this updates currency UI in main menu
            MainMenuManager.Instance.UpdateCurrency();

            // this function deactives purchase button and activates use button instead
            _shopItemsInHieararchy[index].transform.GetChild(2).gameObject.SetActive(false);
            _shopItemsInHieararchy[index].transform.GetChild(3).gameObject.SetActive(true);

            _confirmGUI.SetActive(false);
        }

        private void CancelPurchase()
        {
            _confirmGUI.SetActive(false);
        }
    }
}