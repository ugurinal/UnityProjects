using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [Header("Level Buttons")]
    public Button[] levelButtons;

    private GameManager gameManager = null;

    private void Start()
    {
        gameManager = GameManager.instance;
        SetLevelButtons();
    }

    private void SetLevelButtons()
    {
        int levelReached = gameManager.LevelReached;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                levelButtons[i].transform.GetChild(0).gameObject.SetActive(true);   // level text
                levelButtons[i].transform.GetChild(1).gameObject.SetActive(false);  // lock image

                SetStars(levelButtons[i]);

                if (i == levelReached - 1)
                {
                    levelButtons[i].transform.GetChild(8).gameObject.SetActive(true);
                }
            }
        }
    }

    private void SetStars(Button levelButton)
    {
        string[] levelName = levelButton.name.Split('-');
        int currentLevel = int.Parse(levelName[0]) * int.Parse(levelName[1]);

        int starCount = gameManager.LevelStars[currentLevel - 1];

        switch (starCount)
        {
            case 0:
                levelButton.transform.GetChild(2).gameObject.SetActive(true);
                levelButton.transform.GetChild(3).gameObject.SetActive(true);
                levelButton.transform.GetChild(4).gameObject.SetActive(true);

                levelButton.transform.GetChild(5).gameObject.SetActive(false);
                levelButton.transform.GetChild(6).gameObject.SetActive(false);
                levelButton.transform.GetChild(7).gameObject.SetActive(false);
                break;

            case 1:
                levelButton.transform.GetChild(2).gameObject.SetActive(false);
                levelButton.transform.GetChild(3).gameObject.SetActive(true);
                levelButton.transform.GetChild(4).gameObject.SetActive(true);

                levelButton.transform.GetChild(5).gameObject.SetActive(true);
                levelButton.transform.GetChild(6).gameObject.SetActive(false);
                levelButton.transform.GetChild(7).gameObject.SetActive(false);
                break;

            case 2:
                levelButton.transform.GetChild(2).gameObject.SetActive(false);
                levelButton.transform.GetChild(3).gameObject.SetActive(false);
                levelButton.transform.GetChild(4).gameObject.SetActive(true);

                levelButton.transform.GetChild(5).gameObject.SetActive(true);
                levelButton.transform.GetChild(6).gameObject.SetActive(true);
                levelButton.transform.GetChild(7).gameObject.SetActive(false);

                break;

            case 3:
                levelButton.transform.GetChild(2).gameObject.SetActive(false);
                levelButton.transform.GetChild(3).gameObject.SetActive(false);
                levelButton.transform.GetChild(4).gameObject.SetActive(false);

                levelButton.transform.GetChild(5).gameObject.SetActive(true);
                levelButton.transform.GetChild(6).gameObject.SetActive(true);
                levelButton.transform.GetChild(7).gameObject.SetActive(true);
                break;

            default:
                Debug.Log("Star count in levelbutton handler not initialised!");
                break;
        }
    }
}