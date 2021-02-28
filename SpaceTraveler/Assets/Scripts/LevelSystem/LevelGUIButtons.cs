using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SpaceTraveler.ShopSystem;

namespace SpaceTraveler.LevelSystem
{
    public class LevelGUIButtons : MonoBehaviour
    {
        [Header("Power Up Shop")]
        [SerializeField] private ShopItems _powerUps = null;

        [Header("Pop Up Panel")]
        [SerializeField] private GameObject _popUpPanel = null;
        [SerializeField] private Button _outsideClickButton = null;

        [Header("Level GUI")]
        [SerializeField] private GameObject _levelGUI = null;

        [Header("Close Button")]
        [SerializeField] private Button _closeGUIBtn = null;

        [Header("Power Up Buttons")]
        [SerializeField] private Button _oneShotPowerUpBtn = null;
        [SerializeField] private Button _twoShotPowerUpBtn = null;
        [SerializeField] private Button _lifePowerUpBtn = null;

        private GameManager _gameManager = null;

        // Start is called before the first frame update
        private void Start()
        {
            _gameManager = GameManager.Instance;
            SetButtonListeners();
            UpdateButtons();
        }

        private void SetButtonListeners()
        {
            _closeGUIBtn.onClick.RemoveAllListeners();
            _closeGUIBtn.onClick.AddListener(CloseGUI);

            _oneShotPowerUpBtn.onClick.RemoveAllListeners();
            _twoShotPowerUpBtn.onClick.RemoveAllListeners();
            _lifePowerUpBtn.onClick.RemoveAllListeners();

            _oneShotPowerUpBtn.onClick.AddListener(BuyOneShotPowerUp);
            _twoShotPowerUpBtn.onClick.AddListener(BuyTwoShotPowerUp);
            _lifePowerUpBtn.onClick.AddListener(BuyLifePowerUp);

            _outsideClickButton.onClick.RemoveAllListeners();
            _outsideClickButton.onClick.AddListener(ClickedOutside);
        }

        private void ClickedOutside()
        {
            _levelGUI.SetActive(false);
            _popUpPanel.SetActive(false);
        }

        private void UpdateButtons()
        {
            _oneShotPowerUpBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + _powerUps._ShopItems[0].Price;
            _twoShotPowerUpBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + _powerUps._ShopItems[1].Price;
            _lifePowerUpBtn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + _powerUps._ShopItems[2].Price;

            if (_gameManager.OneShotPU)
            {
                _oneShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
            }
            if (_gameManager.TwoShotPU)
            {
                _twoShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
            }
            if (_gameManager.LifePU)
            {
                _lifePowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
            }
        }

        private void CloseGUI()
        {
            _popUpPanel.SetActive(false);
            _levelGUI.SetActive(false);
        }

        private void BuyOneShotPowerUp()
        {
            int price = _powerUps._ShopItems[0].Price;

            if (_gameManager.OneShotPU)
            {
                Debug.Log("1");
                _oneShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(false);
                _gameManager.OneShotPU = false;
                _gameManager.SaveData();

                _gameManager.IncreaseDiamond(price);
                MainMenuManager.instance.UpdateCurrency();
                return;
            }

            if (!_gameManager.CanEffort(price, true))
            {
                Debug.LogError("You don't have enough money to buy this item!");
                return;
            }

            _oneShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
            _gameManager.OneShotPU = true;
            _gameManager.SaveData();

            _gameManager.IncreaseDiamond(-price);
            MainMenuManager.instance.UpdateCurrency();
            Debug.Log("Powerup one bought!");
        }

        private void BuyTwoShotPowerUp()
        {
            int price = _powerUps._ShopItems[1].Price;

            if (_gameManager.TwoShotPU)
            {
                Debug.Log("2");
                _twoShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(false);
                _gameManager.TwoShotPU = false;
                _gameManager.SaveData();

                _gameManager.IncreaseDiamond(price);
                MainMenuManager.instance.UpdateCurrency();
                return;
            }

            if (!_gameManager.CanEffort(price, true))
            {
                Debug.LogError("You don't have enough money to buy this item!");
                return;
            }

            _twoShotPowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
            _gameManager.TwoShotPU = true;
            _gameManager.SaveData();

            _gameManager.IncreaseDiamond(-price);
            MainMenuManager.instance.UpdateCurrency();
            Debug.Log("Powerup two bought!");
        }

        private void BuyLifePowerUp()
        {
            int price = _powerUps._ShopItems[2].Price;

            if (_gameManager.LifePU)
            {
                Debug.Log("3");
                _lifePowerUpBtn.transform.GetChild(3).gameObject.SetActive(false);
                _gameManager.LifePU = false;
                _gameManager.SaveData();

                _gameManager.IncreaseDiamond(price);
                MainMenuManager.instance.UpdateCurrency();
                return;
            }

            if (!_gameManager.CanEffort(price, true))
            {
                Debug.LogError("You don't have enough money to buy this item!");
                return;
            }

            _lifePowerUpBtn.transform.GetChild(3).gameObject.SetActive(true);
            _gameManager.LifePU = true;
            _gameManager.SaveData();

            _gameManager.IncreaseDiamond(-price);
            MainMenuManager.instance.UpdateCurrency();
            Debug.Log("Powerup life bought!");
        }
    }
}