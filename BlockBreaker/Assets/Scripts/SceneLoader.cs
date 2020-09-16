using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int curIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curIdx + 1);
    }

    public void LoadSameScene()
    {
        int curIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curIdx);
    }

    public void LoadPreviousScene()
    {
        int curIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curIdx - 1);
    }

    public void Quit()
    {
        Application.Quit(1);
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void LoadLevelManager()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void LoadHelpMenu()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DeleteAll() // test purpose only
    {
        PlayerPrefs.DeleteAll();
    }
}