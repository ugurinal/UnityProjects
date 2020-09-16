using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gui;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI timeTextGui;

    // Cached references
    private Ball _ball;
    private Paddle _paddle;

    // Block counters
    private int _hpBlock1;
    private int _hpBlock2;
    private int _hpBlock3;
    private int _hpBlock4;
    private int _hpBlock5;
    private int _blockCounter;

    private int _curScore;
    private int _highScore;
    private int _maxScore;
    private int _scoreBonus;

    private bool _isPaused;
    private float _time;

    private int _levelName;

    private void Start()
    {
        _paddle = FindObjectOfType<Paddle>();
        _ball = FindObjectOfType<Ball>();

        _levelName = int.Parse(SceneManager.GetActiveScene().name);

        Time.timeScale = 1;

        _curScore = 0;
        _scoreBonus = 1;
        scoreText.text = _curScore.ToString();

        _maxScore = 100 * _hpBlock1 + 200 * _hpBlock2 + 300 * _hpBlock3 + 400 * _hpBlock4 + 500 * _hpBlock5;
        PlayerPrefs.SetInt(_levelName + "_MAX_SCORE", _maxScore);

        timeText.text = TimeFormat(_time);
        timeTextGui.text = TimeFormat(_time);
    }

    private void Update()
    {
        if (!_ball.GetHasStarted() || gui.activeSelf) return;
        _time += Time.deltaTime;
        timeText.text = TimeFormat(_time);
        timeTextGui.text = TimeFormat(_time);
    }

    public bool GetIsPaused()
    {
        return _isPaused;
    }

    public void ScoreBonusOn()
    {
        _scoreBonus = 2;
    }

    public void ScoreBonusOff()
    {
        _scoreBonus = 1;
    }

    public void IncreasePaddle()
    {
        if (transform.localScale.x >= 2) return;
        Vector3 scale = new Vector3(0.15f, 0f, 0f);
        _paddle.transform.localScale += scale;
    }

    public void DecreasePaddle()
    {
        if (transform.localScale.x < 0.5) return;
        Vector3 scale = new Vector3(0.15f, 0f, 0f);
        _paddle.transform.localScale -= scale;
    }

    public void PaddleWallOn()
    {
        _paddle.transform.GetChild(3).gameObject.SetActive(true);
        _paddle.transform.GetChild(4).gameObject.SetActive(true);
    }

    public void PaddleWallOff()
    {
        _paddle.transform.GetChild(3).gameObject.SetActive(false);
        _paddle.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void IncScore()
    {
        _curScore += 100 * _scoreBonus;
        scoreText.text = _curScore.ToString();
    }

    public void IncScore(int hp)
    {
        _curScore += 100 * hp * _scoreBonus;
        scoreText.text = _curScore.ToString();
    }

    public void DecCtr()
    {
        _blockCounter--;

        _highScore = PlayerPrefs.GetInt(_levelName + "_SCORE");
        if (_curScore > _highScore)
        {
            PlayerPrefs.SetInt(_levelName + "_SCORE", _curScore);
        }

        if (_blockCounter > 0) return;
        _levelName += 1;
        PlayerPrefs.SetInt(_levelName.ToString(), 1);
        PopUpGui();
    }

    public void Inc1HpBlock()
    {
        _hpBlock1++;
        _blockCounter++;
    }

    public void Inc2HpBlock()
    {
        _hpBlock2++;
        _blockCounter++;
    }

    public void Inc3HpBlock()
    {
        _hpBlock3++;
        _blockCounter++;
    }

    public void Inc4HpBlock()
    {
        _hpBlock4++;
        _blockCounter++;
    }

    public void Inc5HpBlock()
    {
        _hpBlock5++;
        _blockCounter++;
    }

    private void PopUpGui()
    {
        _ball.gameObject.SetActive(false);
        gui.SetActive(true);
        gui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "VICTORY";
        gui.transform.GetChild(2).gameObject.SetActive(true);
        gui.transform.GetChild(3).gameObject.SetActive(false);
        gui.transform.GetChild(4).gameObject.SetActive(false);
        gui.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = _curScore.ToString();
        gui.transform.GetChild(9).GetComponent<Button>().interactable = true;
    }

    public void PauseClick()
    {
        if (_blockCounter > 0 && _ball.gameObject.activeSelf)
        {
            if (Time.timeScale == 0)
            {
                _isPaused = false;
                Time.timeScale = 1f;
                gui.SetActive(false);
            }
            else
            {
                _isPaused = true;
                Time.timeScale = 0f;
                gui.SetActive(true);
                gui.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "PAUSED";
                gui.transform.GetChild(2).gameObject.SetActive(false);
                gui.transform.GetChild(3).gameObject.SetActive(false);
                gui.transform.GetChild(4).gameObject.SetActive(true);
                gui.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = _curScore.ToString();
                gui.transform.GetChild(9).GetComponent<Button>().interactable = false;
            }
        }
    }

    public string TimeFormat(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        return niceTime;
    }
}