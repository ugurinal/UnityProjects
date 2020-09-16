using System;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class LevelManager : MonoBehaviour
{
    [Serializable]
    public class Level
    {
        public string levelText;
        public int unLocked;
        public bool isIntractable;
    }

    public GameObject levelButton;
    public Transform parent;
    public List<Level> levelList;

    private void Start()
    {
        PlayerPrefs.SetInt("1", 1);
        FillList();
    }

    private void FillList()
    {
        foreach (var level in levelList)
        {
            GameObject newButton = Instantiate(levelButton, parent);
            LevelButton button = newButton.GetComponent<LevelButton>();
            button.levelText.text = level.levelText;

            button.levelText.color = new Color32(79, 99, 99, 255);
            button.lockObj.GetComponent<Image>().color = new Color32(79, 99, 99, 255);


            if (PlayerPrefs.GetInt(button.levelText.text) == 1)
            {
                button.levelText.color = new Color(203, 250, 249, 255);
                level.unLocked = 1;
                level.isIntractable = true;

                button.lockObj.SetActive(false);
                button.starBack0.SetActive(true);
                button.starBack1.SetActive(true);
                button.starBack2.SetActive(true);

                if (PlayerPrefs.HasKey(button.levelText.text + "_MAX_SCORE"))
                {
                    if (PlayerPrefs.GetInt(button.levelText.text + "_SCORE") >=
                        PlayerPrefs.GetInt(button.levelText.text + "_MAX_SCORE"))
                    {
                        button.star0.SetActive(true);
                        button.star1.SetActive(true);
                        button.star2.SetActive(true);
                    }
                    else if (PlayerPrefs.GetInt(button.levelText.text + "_SCORE") >=
                             PlayerPrefs.GetInt(button.levelText.text + "_MAX_SCORE") / 2f)
                    {
                        button.star0.SetActive(true);
                        button.star1.SetActive(true);
                        button.starBack2.SetActive(true);
                    }
                    else if (PlayerPrefs.GetInt(button.levelText.text + "_SCORE") >=
                             PlayerPrefs.GetInt(button.levelText.text + "_MAX_SCORE") / 3f)
                    {
                        button.star0.SetActive(true);
                        button.starBack1.SetActive(true);
                        button.starBack2.SetActive(true);
                    }
                }
            }


            button.unLocked = level.unLocked;
            button.GetComponent<Button>().interactable = level.isIntractable;
        }

        SaveAll();
    }

    void SaveAll()
    {
        if (PlayerPrefs.HasKey("1"))
        {
            return;
        }

        GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject button in allButtons)
        {
            LevelButton levelButton = button.GetComponent<LevelButton>();
            PlayerPrefs.SetInt(levelButton.levelText.text, levelButton.unLocked);
        }
    }

}