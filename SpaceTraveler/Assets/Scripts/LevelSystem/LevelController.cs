using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance = null;

    [SerializeField] private ShopItems shopSO = null;

    [SerializeField] private GameObject popUpPanel = null;
    [SerializeField] private GameObject winGUI = null;
    [SerializeField] private GameObject loseGUI = null;

    [SerializeField] private GameObject scoreArea = null;

    [SerializeField] private PauseButton pauseButton = null;
    [SerializeField] private TimeText timeText = null;

    [SerializeField] private float diamondRate = 0f;
    [SerializeField] public float powerUpChance = 0f;

    private int currentLevel = 0;

    private int enemySize = 0;
    private int enemyLeft = 0;

    private GameManager gameManager;
    private Player player;
    private TextMeshProUGUI scoreText;

    private string sceneName;
    private int score = 0;
    private int bestScore = 0;
    private int coinEarned = 0;
    private int diamondEarned = 0;
    private int activeStar = 0;

    private int playerLife;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        SetCurrentLevel();

        sceneName = SceneManager.GetActiveScene().name;

        scoreText = scoreArea.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        scoreText.text = "0";

        //bestScore = PlayerPrefs.GetInt(sceneName + "BestScore", 0);
        bestScore = gameManager.HighScores[currentLevel - 1];

        // instantiate selected ship
        Instantiate(shopSO.GetItem(gameManager.SelectedShip).GetShipPrefab(), new Vector3(0f, -8f - 0f), quaternion.identity);

        player = Player.instance;
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

    private void SetCurrentLevel()
    {
        string[] temp = SceneManager.GetActiveScene().name.Split('-');

        currentLevel = int.Parse(temp[0]) * int.Parse(temp[1]);
    }

    public void SetPlayerLife(int value)
    {
        playerLife = value;
    }

    public void SetEnemySize(int value)
    {
        enemySize = value;
        enemyLeft = value;
    }

    public void DestroyEnemy(int enemyScore, int coin)
    {
        enemyLeft--;

        score += enemyScore;
        coinEarned += coin;

        scoreText.text = score.ToString();

        if (enemyLeft <= 0)
        {
            ShowWinGUI();
        }
    }

    public void ShowLoseGUI()
    {
        popUpPanel.SetActive(true);
        loseGUI.SetActive(true);
        SetPlayerPrefs();
        UpdateStars(loseGUI);
        UpdateTexts(loseGUI);
        HideOtherUI();

        gameManager.IsPaused = true;
        //Time.timeScale = 0f;
    }

    public void ShowWinGUI()
    {
        popUpPanel.SetActive(true);

        winGUI.SetActive(true);
        SetPlayerPrefs();
        UpdateStars(winGUI);
        UpdateTexts(winGUI);
        HideOtherUI();

        gameManager.IsPaused = true;
        // Time.timeScale = 0f;
    }

    private void SetPlayerPrefs()
    {
        // Sets best score
        if (score > bestScore)
        {
            bestScore = score;
            gameManager.HighScores[currentLevel - 1] = bestScore;
            //PlayerPrefs.SetInt(sceneName + "BestScore", bestScore);
        }

        // Unlocks next level and gives coin to the player
        if (enemyLeft <= enemySize / 2)
        {
            gameManager.IncreaseLevel(currentLevel + 1);

            // Give player diamonds
            if (UnityEngine.Random.Range(0, 100) < diamondRate)
            {
                diamondEarned = UnityEngine.Random.Range(0, 3);
            }

            //Updates player coin
            gameManager.IncreaseCoin(coinEarned);
            gameManager.IncreaseDiamond(diamondEarned);
        }
    }

    private void UpdateStars(GameObject GUI)
    {
        for (int i = 0; i < 4; i++)
        {
            GUI.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }

        if (enemyLeft < (enemySize - (enemySize * 9 / 10)))
        {
            activeStar = 3;
            if (activeStar > gameManager.LevelStars[currentLevel - 1])
            {
                gameManager.LevelStars[currentLevel - 1] = activeStar;
            }

            //GUI.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else if (enemyLeft < (enemySize - (enemySize * 7 / 10)))
        {
            activeStar = 2;

            if (activeStar > gameManager.LevelStars[currentLevel - 1])
            {
                gameManager.LevelStars[currentLevel - 1] = activeStar;
            }

            //GUI.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        else if (enemyLeft < enemySize / 2)
        {
            activeStar = 1;

            if (activeStar > gameManager.LevelStars[currentLevel - 1])
            {
                gameManager.LevelStars[currentLevel - 1] = activeStar;
            }

            //GUI.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            activeStar = 0;

            if (activeStar > gameManager.LevelStars[currentLevel - 1])
            {
                gameManager.LevelStars[currentLevel - 1] = activeStar;
            }

            //GUI.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        }

        GUI.transform.GetChild(0).GetChild(3 - activeStar).gameObject.SetActive(true);

        /*
        for (int i = 8; i < 8 + activeStar; i++)
        {
            GUI.transform.GetChild(i - 3).gameObject.SetActive(false);
            GUI.transform.GetChild(i).gameObject.SetActive(true);
        }*/

        gameManager.SaveData();
    }

    private void UpdateTexts(GameObject GUI)
    {
        if (!(enemyLeft < enemySize / 2))
        {
            coinEarned = 0;
            diamondEarned = 0;
        }

        GUI.transform.GetChild(0).GetChild(6).GetComponent<TextMeshProUGUI>().text = score.ToString();
        GUI.transform.GetChild(0).GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>().text = bestScore.ToString();
        GUI.transform.GetChild(0).GetChild(8).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = coinEarned.ToString();
        GUI.transform.GetChild(0).GetChild(8).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = diamondEarned.ToString();
    }

    private void HideOtherUI()
    {
        pauseButton.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        scoreArea.gameObject.SetActive(false);
    }

    public void DecreasePlayerLife()
    {
        playerLife--;
        if (playerLife <= 0)
        {
            player.transform.GetChild(0).gameObject.SetActive(false);    // deactivates the rocket thrust in player prefab
            player.GetComponent<Animator>().enabled = true;              // this will destroy player when animation ends

            ShowLoseGUI();
        }
    }
}