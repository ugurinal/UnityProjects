using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject canvas = null;

    [Header("Main PopUp")]
    [SerializeField] private GameObject mainPopUp = null;

    [Header("PauseGUI")]
    [SerializeField] private GameObject pauseGUI = null;
    [SerializeField] private Sprite[] pauseSprite = null;

    private GameManager gameManager = null;
    private Button pauseButon = null;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.IsPaused = false;

        pauseButon = GetComponent<Button>();
        pauseButon.onClick.AddListener(OnClickEvent);
    }

    public void OnClickEvent()
    {
        if (!gameManager.IsPaused)
        {
            pauseButon.image.sprite = pauseSprite[1];
            SetPauseGUI(true);

            Invoke("PauseTheGame", 0.3f);
            //Time.timeScale = 0f;

            gameManager.IsPaused = true;
        }
        else
        {
            pauseButon.image.sprite = pauseSprite[0];
            SetPauseGUI(false);
            Time.timeScale = 1f;

            gameManager.IsPaused = false;
        }

        //gameManager.PauseClicked();
    }

    public void SetPauseGUI(bool value)
    {
        pauseGUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "LEVEL " + SceneManager.GetActiveScene().name;
        mainPopUp.SetActive(value);

        pauseGUI.SetActive(value);
    }

    private void PauseTheGame()
    {
        Time.timeScale = 0f;
    }
}