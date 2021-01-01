using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;

    private void Awake()
    {
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }
}