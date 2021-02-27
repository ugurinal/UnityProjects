using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using TMPro;
using UnityEngine.SceneManagement;

public class GoogleAuth : MonoBehaviour
{
    public static PlayGamesPlatform platform;
    private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        gameManager = GameManager.instance;

        text.text = "STATUS : NULL";

        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;

            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("Logged in successfully!");
                text.text = "STATUS : SUCCESS!";

                gameManager.PlayerName = Social.Active.localUser.userName;
            }
            else
            {
                Debug.Log("FAILED TO LOGIN!");
                text.text = "STATUS : FAILED!";
                gameManager.PlayerName = "Not Found";
            }

            StartCoroutine(GoToMainMenu());
        });
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}