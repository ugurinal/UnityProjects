using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SpaceTraveler.ManagerSystem;

namespace SpaceTraveler.LevelSystem
{
    public class WorldChanger : MonoBehaviour
    {
        [Header("Level GUI")]
        [SerializeField] private GameObject _popUpPanel = null;
        [SerializeField] private GameObject _levelGUI = null;

        [Space(20)]
        [SerializeField] private GameObject _levelHolder = null;    // scroll rect
        [SerializeField] private GameObject _worldOne = null;       // a rect transform which is the content of the level holder that holds the levels
        [SerializeField] private GameObject _worldTwo = null;       // same with above

        [SerializeField] private Button _worldOneButton = null;
        [SerializeField] private Button _worldTwoButton = null;
        [SerializeField] private Sprite[] _worldActiveSprites = null;

        [SerializeField] private GameObject _backGround = null;
        [SerializeField] private Sprite[] _backGroundSprites = null;

        [SerializeField] private GameObject _playerShip = null;
        [SerializeField] private Vector3 _playerZSpeed = Vector3.zero;

        [SerializeField] private GameObject _warpEffect = null;     // parent

        private Vector3 _playerOriginalPos = Vector3.zero;
        private bool _isWaprEffectActive = false;

        private int _activeWorld = 0;
        private static bool _isFirst = true;

        private GameManager _gameManager = null;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            int levelReached = _gameManager.LevelReached;

            _playerOriginalPos = _playerShip.transform.position;

            _worldOneButton.onClick.AddListener(LoadWorldOne);
            _worldTwoButton.onClick.AddListener(LoadWorldTwo);

            if (levelReached > 12)
            {
                if (_isFirst)
                {
                    LoadWorldTwo();
                    _isFirst = false;
                }
                else
                {
                    LoadWorldTwoWithoutAnim();
                }
            }
            else if (levelReached > 0)
            {
                if (_isFirst)
                {
                    LoadWorldOne();
                    _isFirst = false;
                }
                else
                {
                    LoadWorldOneWithoutAnim();
                }
            }
            else
            {
                // this else will never work!
                Debug.Log("5");

                LoadWorldOneWithoutAnim();
            }
        }

        private void Update()
        {
            if (_isWaprEffectActive)
            {
                _playerShip.transform.position += _playerZSpeed * Time.deltaTime;
            }
        }

        public void LoadWorldOne()
        {
            StartCoroutine(PlayWarpEffect());

            _activeWorld = 1;
            _levelHolder.GetComponent<ScrollRect>().content = _worldOne.GetComponent<RectTransform>();
            _worldOne.SetActive(true);

            _worldOneButton.image.sprite = _worldActiveSprites[1];
            _backGround.GetComponent<Image>().sprite = _backGroundSprites[0];
            DeactiveWorld();
        }

        public void LoadWorldTwo()
        {
            StartCoroutine(PlayWarpEffect());

            _activeWorld = 2;
            _levelHolder.GetComponent<ScrollRect>().content = _worldTwo.GetComponent<RectTransform>();
            _worldTwo.SetActive(true);

            _worldTwoButton.image.sprite = _worldActiveSprites[3];
            _backGround.GetComponent<Image>().sprite = _backGroundSprites[1];

            DeactiveWorld();
        }

        private void LoadWorldOneWithoutAnim()
        {
            _activeWorld = 1;
            _levelHolder.GetComponent<ScrollRect>().content = _worldOne.GetComponent<RectTransform>();
            _worldOne.SetActive(true);

            _worldOneButton.image.sprite = _worldActiveSprites[1];
            _backGround.GetComponent<Image>().sprite = _backGroundSprites[0];
            DeactiveWorld();
        }

        private void LoadWorldTwoWithoutAnim()
        {
            _activeWorld = 2;
            _levelHolder.GetComponent<ScrollRect>().content = _worldTwo.GetComponent<RectTransform>();
            _worldTwo.SetActive(true);

            _worldTwoButton.image.sprite = _worldActiveSprites[3];
            _backGround.GetComponent<Image>().sprite = _backGroundSprites[1];

            DeactiveWorld();
        }

        private void DeactiveWorld()
        {
            if (_levelGUI.activeSelf)
            {
                _popUpPanel.SetActive(false);
                _levelGUI.SetActive(false);
            }

            switch (_activeWorld)
            {
                case 1:
                    _worldTwo.SetActive(false);
                    _worldTwoButton.image.sprite = _worldActiveSprites[2];

                    break;

                case 2:
                    _worldOne.SetActive(false);
                    _worldOneButton.image.sprite = _worldActiveSprites[0];

                    break;

                default:
                    Debug.Log("Nothing assigned");
                    break;
            }
        }

        private IEnumerator PlayWarpEffect()
        {
            _isWaprEffectActive = true;
            _playerShip.transform.parent.gameObject.SetActive(true);
            _warpEffect.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            _isWaprEffectActive = false;
            _playerShip.transform.parent.gameObject.SetActive(false);
            _warpEffect.SetActive(false);

            _playerShip.transform.position = _playerOriginalPos;
        }
    }
}