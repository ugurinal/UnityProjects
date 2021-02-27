using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldChanger : MonoBehaviour
{
    [Header("Level GUI")]
    [SerializeField] private GameObject popUpPanel = null;
    [SerializeField] private GameObject levelGUI = null;

    [Space(20)]
    [SerializeField] private GameObject levelHolder = null;    // scroll rect
    [SerializeField] private GameObject worldOne = null;       // a rect transform which is the content of the level holder that holds the levels
    [SerializeField] private GameObject worldTwo = null;       // same with above

    [SerializeField] private Button worldOneButton = null;
    [SerializeField] private Button worldTwoButton = null;
    [SerializeField] private Sprite[] worldActiveSprites = null;

    [SerializeField] private GameObject backGround = null;
    [SerializeField] private Sprite[] backGroundSprites = null;

    [SerializeField] private GameObject playerShip = null;
    [SerializeField] private Vector3 playerZSpeed = Vector3.zero;

    [SerializeField] private GameObject warpEffect = null;     // parent

    private Vector3 playerOriginalPos = Vector3.zero;
    private bool isWaprEffectActive = false;

    private int activeWorld = 0;
    private static bool isFirst = true;

    private GameManager gameManager = null;

    private void Start()
    {
        gameManager = GameManager.instance;
        int levelReached = gameManager.LevelReached;

        playerOriginalPos = playerShip.transform.position;

        worldOneButton.onClick.AddListener(LoadWorldOne);
        worldTwoButton.onClick.AddListener(LoadWorldTwo);

        if (levelReached > 12)
        {
            if (isFirst)
            {
                LoadWorldTwo();
                isFirst = false;
            }
            else
            {
                LoadWorldTwoWithoutAnim();
            }
        }
        else if (levelReached > 0)
        {
            if (isFirst)
            {
                LoadWorldOne();
                isFirst = false;
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
        if (isWaprEffectActive)
        {
            playerShip.transform.position += playerZSpeed * Time.deltaTime;
        }
    }

    public void LoadWorldOne()
    {
        StartCoroutine(PlayWarpEffect());

        activeWorld = 1;
        levelHolder.GetComponent<ScrollRect>().content = worldOne.GetComponent<RectTransform>();
        worldOne.SetActive(true);

        worldOneButton.image.sprite = worldActiveSprites[1];
        backGround.GetComponent<Image>().sprite = backGroundSprites[0];
        DeactiveWorld();
    }

    public void LoadWorldTwo()
    {
        StartCoroutine(PlayWarpEffect());

        activeWorld = 2;
        levelHolder.GetComponent<ScrollRect>().content = worldTwo.GetComponent<RectTransform>();
        worldTwo.SetActive(true);

        worldTwoButton.image.sprite = worldActiveSprites[3];
        backGround.GetComponent<Image>().sprite = backGroundSprites[1];

        DeactiveWorld();
    }

    private void LoadWorldOneWithoutAnim()
    {
        activeWorld = 1;
        levelHolder.GetComponent<ScrollRect>().content = worldOne.GetComponent<RectTransform>();
        worldOne.SetActive(true);

        worldOneButton.image.sprite = worldActiveSprites[1];
        backGround.GetComponent<Image>().sprite = backGroundSprites[0];
        DeactiveWorld();
    }

    private void LoadWorldTwoWithoutAnim()
    {
        activeWorld = 2;
        levelHolder.GetComponent<ScrollRect>().content = worldTwo.GetComponent<RectTransform>();
        worldTwo.SetActive(true);

        worldTwoButton.image.sprite = worldActiveSprites[3];
        backGround.GetComponent<Image>().sprite = backGroundSprites[1];

        DeactiveWorld();
    }

    private void DeactiveWorld()
    {
        if (levelGUI.activeSelf)
        {
            popUpPanel.SetActive(false);
            levelGUI.SetActive(false);
        }

        switch (activeWorld)
        {
            case 1:
                worldTwo.SetActive(false);
                worldTwoButton.image.sprite = worldActiveSprites[2];

                break;

            case 2:
                worldOne.SetActive(false);
                worldOneButton.image.sprite = worldActiveSprites[0];

                break;

            default:
                Debug.Log("Nothing assigned");
                break;
        }
    }

    private IEnumerator PlayWarpEffect()
    {
        isWaprEffectActive = true;
        playerShip.transform.parent.gameObject.SetActive(true);
        warpEffect.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        isWaprEffectActive = false;
        playerShip.transform.parent.gameObject.SetActive(false);
        warpEffect.SetActive(false);

        playerShip.transform.position = playerOriginalPos;
    }
}