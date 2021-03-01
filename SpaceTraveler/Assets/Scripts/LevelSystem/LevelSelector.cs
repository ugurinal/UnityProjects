using UnityEngine;
using UnityEngine.UI;
using SpaceTraveler.ManagerSystem;

namespace SpaceTraveler.LevelSystem
{
    public class LevelSelector : MonoBehaviour
    {
        [Header("Level Buttons")]
        [SerializeField] private Button[] _levelButtons;

        private GameManager _gameManager = null;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            SetLevelButtons();
        }

        private void SetLevelButtons()
        {
            int levelReached = _gameManager.LevelReached;

            for (int i = 0; i < _levelButtons.Length; i++)
            {
                if (i + 1 > levelReached)
                {
                    _levelButtons[i].GetComponent<Button>().interactable = false;
                }
                else
                {
                    _levelButtons[i].transform.GetChild(0).gameObject.SetActive(true);   // level text
                    _levelButtons[i].transform.GetChild(1).gameObject.SetActive(false);  // lock image

                    SetStars(i);

                    if (i == levelReached - 1)
                    {
                        _levelButtons[i].transform.GetChild(8).gameObject.SetActive(true);
                    }
                }
            }
        }

        private void SetStars(int levelIndex)
        {
            int starCount = _gameManager.LevelStars[levelIndex];

            switch (starCount)
            {
                case 0:
                    _levelButtons[levelIndex].transform.GetChild(2).gameObject.SetActive(true);
                    _levelButtons[levelIndex].transform.GetChild(3).gameObject.SetActive(true);
                    _levelButtons[levelIndex].transform.GetChild(4).gameObject.SetActive(true);

                    _levelButtons[levelIndex].transform.GetChild(5).gameObject.SetActive(false);
                    _levelButtons[levelIndex].transform.GetChild(6).gameObject.SetActive(false);
                    _levelButtons[levelIndex].transform.GetChild(7).gameObject.SetActive(false);
                    break;

                case 1:
                    _levelButtons[levelIndex].transform.GetChild(2).gameObject.SetActive(false);
                    _levelButtons[levelIndex].transform.GetChild(3).gameObject.SetActive(true);
                    _levelButtons[levelIndex].transform.GetChild(4).gameObject.SetActive(true);

                    _levelButtons[levelIndex].transform.GetChild(5).gameObject.SetActive(true);
                    _levelButtons[levelIndex].transform.GetChild(6).gameObject.SetActive(false);
                    _levelButtons[levelIndex].transform.GetChild(7).gameObject.SetActive(false);
                    break;

                case 2:
                    _levelButtons[levelIndex].transform.GetChild(2).gameObject.SetActive(false);
                    _levelButtons[levelIndex].transform.GetChild(3).gameObject.SetActive(false);
                    _levelButtons[levelIndex].transform.GetChild(4).gameObject.SetActive(true);

                    _levelButtons[levelIndex].transform.GetChild(5).gameObject.SetActive(true);
                    _levelButtons[levelIndex].transform.GetChild(6).gameObject.SetActive(true);
                    _levelButtons[levelIndex].transform.GetChild(7).gameObject.SetActive(false);

                    break;

                case 3:
                    _levelButtons[levelIndex].transform.GetChild(2).gameObject.SetActive(false);
                    _levelButtons[levelIndex].transform.GetChild(3).gameObject.SetActive(false);
                    _levelButtons[levelIndex].transform.GetChild(4).gameObject.SetActive(false);

                    _levelButtons[levelIndex].transform.GetChild(5).gameObject.SetActive(true);
                    _levelButtons[levelIndex].transform.GetChild(6).gameObject.SetActive(true);
                    _levelButtons[levelIndex].transform.GetChild(7).gameObject.SetActive(true);
                    break;

                default:
                    Debug.Log("Star count in levelbutton handler not initialised!");
                    break;
            }
        }
    }
}