using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceTraveler.Player;
using SpaceTraveler.ShopSystem;
using SpaceTraveler.UISystem;
using SpaceTraveler.ManagerSystem;
using SpaceTraveler.Utilities;

namespace SpaceTraveler.LevelSystem
{
    public class LevelController : MonoBehaviour
    {
        private static LevelController _instance = null;
        public static LevelController Instance { get => _instance; }

        [SerializeField] private ShopItems _shopItems = null;

        [SerializeField] private GameObject _popUpPanel = null;
        [SerializeField] private GameObject _winGUI = null;
        [SerializeField] private GameObject _loseGUI = null;

        [SerializeField] private GameObject _scoreArea = null;

        [SerializeField] private PauseButton _pauseButton = null;
        [SerializeField] private TimeText _timeText = null;

        [SerializeField] private float _diamondRate = 0f;
        [SerializeField] private float _powerUpChance = 0f;

        [SerializeField] private GameObject playerShip; // test purpose only

        public float PowerUpChance { get => _powerUpChance; }

        private int _currentLevel = 0;

        private int _enemySize = 0;
        private int _enemyLeft = 0;

        private GameManager _gameManager = null;
        private PlayerController _player = null;
        private TextMeshProUGUI _scoreTMP = null;

        private string sceneName;
        private int _score = 0;
        private int _bestScore = 0;
        private int _coinEarned = 0;
        private int _diamondEarned = 0;
        private int _activeStar = 0;

        private int _playerLife = 0;

        private void Awake()
        {
            MakeSingleton();
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
            SetCurrentLevel();

            sceneName = SceneManager.GetActiveScene().name;

            _scoreTMP = _scoreArea.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _scoreTMP.text = "0";

            _bestScore = _gameManager.HighScores[_currentLevel - 1];

            // instantiate selected ship
            Instantiate(_shopItems._ShopItems[_gameManager.SelectedShip].ShipPrefab, new Vector3(0f, -8f - 0f), quaternion.identity);
            //Instantiate(playerShip, new Vector3(0f, -8f - 0f), quaternion.identity);    // test purpose only

            _player = PlayerController.Instance;
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

        private void SetCurrentLevel()
        {
            string[] temp = SceneManager.GetActiveScene().name.Split('-');

            _currentLevel = int.Parse(temp[0]) * int.Parse(temp[1]);
        }

        public void SetPlayerLife(int value)
        {
            _playerLife = value;
        }

        public void SetEnemySize(int value)
        {
            _enemySize = value;
            _enemyLeft = value;
        }

        public void DestroyEnemy(int enemyScore, int coin)
        {
            _enemyLeft--;

            _score += enemyScore;
            _coinEarned += coin;

            _scoreTMP.text = _score.ToString();

            if (_enemyLeft <= 0)
            {
                ShowWinGUI();
            }
        }

        public void ShowLoseGUI()
        {
            if (_winGUI.activeSelf)
                return;

            _popUpPanel.SetActive(true);
            _loseGUI.SetActive(true);
            SetPlayerPrefs();
            UpdateStars(_loseGUI);
            UpdateTexts(_loseGUI);
            HideOtherUI();

            _gameManager.IsPaused = true;
            //Time.timeScale = 0f;
        }

        public void ShowWinGUI()
        {
            if (_loseGUI.activeSelf)
                return;

            _popUpPanel.SetActive(true);

            _winGUI.SetActive(true);
            SetPlayerPrefs();
            UpdateStars(_winGUI);
            UpdateTexts(_winGUI);
            HideOtherUI();

            _gameManager.IsPaused = true;
            // Time.timeScale = 0f;
        }

        private void SetPlayerPrefs()
        {
            // Sets best score
            if (_score > _bestScore)
            {
                _bestScore = _score;
                _gameManager.HighScores[_currentLevel - 1] = _bestScore;
            }

            // Unlocks next level and gives coin to the player
            if (_enemyLeft <= _enemySize / 2)
            {
                _gameManager.IncreaseLevel(_currentLevel + 1);

                // Give player diamonds
                if (UnityEngine.Random.Range(0, 100) < _diamondRate)
                {
                    _diamondEarned = UnityEngine.Random.Range(0, 3);
                }

                //Updates player coin
                _gameManager.IncreaseCoin(_coinEarned);
                _gameManager.IncreaseDiamond(_diamondEarned);
            }
        }

        private void UpdateStars(GameObject GUI)
        {
            for (int i = 0; i < 4; i++)
            {
                GUI.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            if (_enemyLeft < (_enemySize - (_enemySize * 9 / 10)))
            {
                _activeStar = 3;
                if (_activeStar > _gameManager.LevelStars[_currentLevel - 1])
                {
                    _gameManager.LevelStars[_currentLevel - 1] = _activeStar;
                }
            }
            else if (_enemyLeft < (_enemySize - (_enemySize * 7 / 10)))
            {
                _activeStar = 2;

                if (_activeStar > _gameManager.LevelStars[_currentLevel - 1])
                {
                    _gameManager.LevelStars[_currentLevel - 1] = _activeStar;
                }
            }
            else if (_enemyLeft <= _enemySize / 2)
            {
                _activeStar = 1;

                if (_activeStar > _gameManager.LevelStars[_currentLevel - 1])
                {
                    _gameManager.LevelStars[_currentLevel - 1] = _activeStar;
                }
            }
            else
            {
                _activeStar = 0;

                if (_activeStar > _gameManager.LevelStars[_currentLevel - 1])
                {
                    _gameManager.LevelStars[_currentLevel - 1] = _activeStar;
                }
            }

            GUI.transform.GetChild(0).GetChild(3 - _activeStar).gameObject.SetActive(true);

            _gameManager.SaveData();
        }

        private void UpdateTexts(GameObject GUI)
        {
            if (!(_enemyLeft < _enemySize / 2))
            {
                _coinEarned = 0;
                _diamondEarned = 0;
            }

            GUI.transform.GetChild(0).GetChild(6).GetComponent<TextMeshProUGUI>().text = _score.ToString();
            GUI.transform.GetChild(0).GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>().text = _bestScore.ToString();
            GUI.transform.GetChild(0).GetChild(8).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = _coinEarned.ToString();
            GUI.transform.GetChild(0).GetChild(8).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = _diamondEarned.ToString();
        }

        private void HideOtherUI()
        {
            _pauseButton.gameObject.SetActive(false);
            _timeText.gameObject.SetActive(false);
            _scoreArea.gameObject.SetActive(false);
        }

        public void DecreasePlayerLife()
        {
            Debug.Log("Decrease Player Life");

            _playerLife--;
            if (_playerLife <= 0)
            {
                _player.transform.GetChild(0).gameObject.SetActive(false);    // deactivates the rocket thrust in player prefab
                _player.GetComponent<Animator>().enabled = true;              // this will destroy player when animation ends

                ShowLoseGUI();
            }
        }
        
        
    }
}