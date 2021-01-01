using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject endScreen;

    private int score = 0;
    private int highScore = 0;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        score = 0;
        scoreText.text = "0";
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void IncScore()
    {
        score++;
        scoreText.text = "" + score;
    }

    public void LoadEndScreen()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }

        endScreen.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        endScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "High Score: " + highScore;
        endScreen.GetComponent<Animator>().SetTrigger("ShowEndScreen");
    }
}