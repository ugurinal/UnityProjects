using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SpaceTraveler.ManagerSystem;

namespace SpaceTraveler.LevelSystem
{
    public class LevelButtonHandler : MonoBehaviour
    {
        #region FIELDS

        [Header("Pop up panel")]
        [SerializeField] private GameObject _popUpPanel = null;

        [Header("Level GUI")]
        [SerializeField] private GameObject _levelGUI = null;

        private Button _levelButton = null;

        private static Button _startButton = null;
        private static string _levelName = "";

        private GameManager _gameManager = null;

        #endregion FIELDS

        private void Awake()
        {
            _levelButton = GetComponent<Button>();
            _levelButton.onClick.AddListener(OpenLevelGUI);

            if (_startButton == null)
            {
                _startButton = _levelGUI.transform.GetChild(0).GetChild(5).GetComponent<Button>();
                _startButton.onClick.AddListener(LoadLevel);
            }
        }

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        private void OpenLevelGUI()
        {
            _levelName = name;

            _levelGUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Level " + name;

            string[] levelNameSplitted = _levelName.Split('-');
            int currentLevel = int.Parse(levelNameSplitted[0]) * int.Parse(levelNameSplitted[1]);

            int starCount = _gameManager.LevelStars[currentLevel - 1];

            switch (starCount)
            {
                case 0:
                    //UpdateStars(0);

                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(true);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(true);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);

                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(false);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(4).gameObject.SetActive(false);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(5).gameObject.SetActive(false);
                    break;

                case 1:
                    //UpdateStars(1);

                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(true);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);

                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(true);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(4).gameObject.SetActive(false);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(5).gameObject.SetActive(false);
                    break;

                case 2:
                    //UpdateStars(2);

                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(false);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(true);

                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(true);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(4).gameObject.SetActive(true);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(5).gameObject.SetActive(false);

                    break;

                case 3:
                    //UpdateStars(3);

                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.SetActive(false);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(1).gameObject.SetActive(false);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(2).gameObject.SetActive(false);

                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(3).gameObject.SetActive(true);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(4).gameObject.SetActive(true);
                    _levelGUI.transform.GetChild(0).GetChild(3).GetChild(5).gameObject.SetActive(true);
                    break;

                default:
                    Debug.Log("Star count in levelbutton handler not initialised!");
                    break;
            }

            _popUpPanel.SetActive(true);
            _levelGUI.SetActive(true);
        }

        private void LoadLevel()
        {
            SceneManager.LoadScene(_levelName);
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