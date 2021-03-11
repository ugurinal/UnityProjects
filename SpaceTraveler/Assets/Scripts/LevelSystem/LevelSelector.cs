using UnityEngine;
using UnityEngine.UI;
using SpaceTraveler.ManagerSystem;

namespace SpaceTraveler.LevelSystem
{
    public class LevelSelector : MonoBehaviour
    {
        [Header("Level Buttons")]
        [SerializeField] private GameObject[] _levelButtons;

        [Header("Level Button Sprites")]
        [SerializeField] private Sprite[] _levelButtonSprites;

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
                        _levelButtons[i].transform.GetChild(2).gameObject.SetActive(true);
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
                    // do nothing
                    break;

                case 1:
                    _levelButtons[levelIndex].GetComponent<Image>().sprite = _levelButtonSprites[0];
                    break;

                case 2:
                    _levelButtons[levelIndex].GetComponent<Image>().sprite = _levelButtonSprites[1];

                    break;

                case 3:
                    _levelButtons[levelIndex].GetComponent<Image>().sprite = _levelButtonSprites[2];
                    break;

                default:
                    Debug.Log("Star count in levelbutton handler not initialised!");
                    break;
            }
        }
    }
}