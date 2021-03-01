using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SpaceTraveler.ManagerSystem;

namespace SpaceTraveler.UISystem
{
    public class PauseButton : MonoBehaviour
    {
        //[Header("Canvas")]
        //[SerializeField] private GameObject canvas = null;

        [Header("Main PopUp")]
        [SerializeField] private GameObject _mainPopUp = null;

        [Header("PauseGUI")]
        [SerializeField] private GameObject _pauseGUI = null;
        [SerializeField] private Sprite[] _pauseSprite = null;

        private GameManager _gameManager = null;
        private Button _pauseButon = null;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _gameManager.IsPaused = false;

            _pauseButon = GetComponent<Button>();
            _pauseButon.onClick.AddListener(OnClickEvent);
        }

        public void OnClickEvent()
        {
            if (!_gameManager.IsPaused)
            {
                _pauseButon.image.sprite = _pauseSprite[1];
                SetPauseGUI(true);

                Invoke("PauseTheGame", 0.3f);   // play pause gui scale animation then pause the game
                //Time.timeScale = 0f;

                _gameManager.IsPaused = true;
            }
            else
            {
                _pauseButon.image.sprite = _pauseSprite[0];
                SetPauseGUI(false);
                Time.timeScale = 1f;

                _gameManager.IsPaused = false;
            }

            //gameManager.PauseClicked();
        }

        public void SetPauseGUI(bool value)
        {
            _pauseGUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "LEVEL " + SceneManager.GetActiveScene().name;
            _mainPopUp.SetActive(value);

            _pauseGUI.SetActive(value);
        }

        private void PauseTheGame()
        {
            Time.timeScale = 0f;
        }
    }
}