using TMPro;
using UnityEngine;
using SpaceTraveler.ManagerSystem;

namespace SpaceTraveler.Utilities
{
    public class TimeText : MonoBehaviour
    {
        private GameManager _gameManager;

        private TextMeshProUGUI _timeText;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _timeText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (!_gameManager.IsPaused)
            {
                _timeText.text = TimeFormat(Time.timeSinceLevelLoad);
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
}