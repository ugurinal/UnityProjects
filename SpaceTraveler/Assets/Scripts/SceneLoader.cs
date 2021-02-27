using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceTraveler.AudioSystem;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
    }

    private void Start()
    {
        SoundController soundController = FindObjectOfType<SoundController>().GetComponent<SoundController>();
        soundController.PlayMusic(SceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1.0f;
        int curIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curIdx + 1);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadTo(string levelName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevelMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelMenu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ClearAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}