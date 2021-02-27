using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    #region DESCRIPTION

    //  *********************************************************************************************
    //  * This is the main class for scrolling background                                           *
    //  * This script is attached to a gameobject in hierarchy that scrolls the background          *
    //  * The logic behind background scroller is using a quad in scene and increasing              *
    //  * or decreasing its material offset value                                                   *
    //  *********************************************************************************************

    #endregion DESCRIPTION

    #region FIELDS

    [Header("Scroll Properties")]
    [SerializeField] private float scrollSpeed = 0.5f;

    private Material material = null;
    private Vector2 offSet = new Vector2(0f, 0f);

    #endregion FIELDS

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, scrollSpeed);
    }

    private void Update()
    {
        material.mainTextureOffset += offSet * Time.deltaTime;
    }
}