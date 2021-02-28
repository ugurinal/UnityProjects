using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SpaceTraveler.UISystem
{
    public class MainMenuManager : MonoBehaviour
    {
        private static MainMenuManager _instance;
        public static MainMenuManager Instance { get => _instance; }

        [SerializeField] private GameObject _avatarArea = null;

        [SerializeField] private List<TextMeshProUGUI> _coinTexts = null;
        [SerializeField] private List<TextMeshProUGUI> _diaTexts = null;

        private int _diamondCounter = 0;
        private int _coinCounter = 0;

        private void Awake()
        {
            MakeSingleton();
        }

        private void Start()
        {
            SetCoinAndDiamond();

            TextMeshProUGUI nicknameTMP = _avatarArea.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            nicknameTMP.text = GameManager.Instance.PlayerName;

            for (int i = 0; i < _coinTexts.Count; i++)
            {
                _coinTexts[i].text = _coinCounter.ToString();
                _diaTexts[i].text = _diamondCounter.ToString();
            }
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

        private void SetCoinAndDiamond()
        {
            _coinCounter = GameManager.Instance.Coin;
            _diamondCounter = GameManager.Instance.Diamond;
        }

        public void UpdateCurrency()
        {
            SetCoinAndDiamond();

            for (int i = 0; i < _coinTexts.Count; i++)
            {
                _coinTexts[i].text = _coinCounter.ToString();
                _diaTexts[i].text = _diamondCounter.ToString();
            }
        }
    }
}