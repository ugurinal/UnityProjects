using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceTraveler.LevelSystem
{
    public class LevelButtonHandler : MonoBehaviour
    {
        #region FIELDS

        [Header("Pop up panel")]
        [SerializeField] private GameObject popUpPanel = null;

        [Header("Level GUI")]
        [SerializeField] private GameObject levelGUI = null;

        private Button levelButton = null;

        private static Button startButton = null;
        private static string levelName = "";

        private GameManager gameManager = null;

        #endregion FIELDS

        private void Awake()
        {
            levelButton = GetComponent<Button>();
            levelButton.onClick.AddListener(OpenLevelGUI);

            if (startButton == null)
            {
                startButton = levelGUI.transform.GetChild(0).GetChild(5).GetComponent<Button>();
                startButton.onClick.AddListener(LoadLevel);
            }
        }

        private void Start()
        {
            gameManager = GameManager.instance;
        }

        private void OpenLevelGUI()
        {
            levelName = name;

            levelGUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + name;

            string[] levelNameSplitted = levelName.Split('-');
            int currentLevel = int.Parse(levelNameSplitted[0]) * int.Parse(levelNameSplitted[1]);

            int starCount = gameManager.LevelStars[currentLevel - 1];

            switch (starCount)
            {
                case 0:
                    //UpdateStars(0);

                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(true);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(true);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);

                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(false);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(4).gameObject.SetActive(false);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(5).gameObject.SetActive(false);
                    break;

                case 1:
                    //UpdateStars(1);

                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(true);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);

                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(true);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(4).gameObject.SetActive(false);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(5).gameObject.SetActive(false);
                    break;

                case 2:
                    //UpdateStars(2);

                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(false);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);

                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(true);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(4).gameObject.SetActive(true);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(5).gameObject.SetActive(false);

                    break;

                case 3:
                    //UpdateStars(3);

                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(false);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(false);

                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(true);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(4).gameObject.SetActive(true);
                    levelGUI.transform.GetChild(0).GetChild(3).GetChild(5).gameObject.SetActive(true);
                    break;

                default:
                    Debug.Log("Star count in levelbutton handler not initialised!");
                    break;
            }

            popUpPanel.SetActive(true);
            levelGUI.SetActive(true);
        }

        private void LoadLevel()
        {
            SceneManager.LoadScene(levelName);
        }

        /*
        private void UpdateStars(int active)
        {
            bool temp = active > 0 ? true : false;

            for (int i = 0; i < 3; i++)
            {
                if (active-- >= 0)
                {
                    temp = !temp;
                }
                levelGUI.transform.GetChild(0).GetChild(3).GetChild(i).gameObject.SetActive(!temp);

                levelGUI.transform.GetChild(0).GetChild(3).GetChild((i + 1) * i).gameObject.SetActive(temp);
            }
        }*/
    }
}