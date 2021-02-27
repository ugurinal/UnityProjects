using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    private GameManager gameManager;

    private TextMeshProUGUI timeText;


    private void Start()
    {
        gameManager = GameManager.instance;
        timeText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameManager.IsPaused)
        {
            timeText.text = TimeFormat(Time.timeSinceLevelLoad);
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