using UnityEngine;

namespace SpaceTraveler.UISystem
{
    #region DESCRIPTION

    /// <summary>
    /// This is the main class for scrolling background
    /// This script is attached to a gameobject in hierarchy that scrolls the background
    /// he logic behind background scroller is using a quad in scene and increasing
    /// or decreasing its material offset value
    /// </summary>

    #endregion DESCRIPTION

    public class BackgroundScroller : MonoBehaviour
    {
        #region FIELDS

        [Header("Scroll Properties")]
        [SerializeField] private float _scrollSpeed = 0.05f;

        private Material _material = null;
        private Vector2 _offSet = Vector2.zero;

        #endregion FIELDS

        private void Start()
        {
            _material = GetComponent<Renderer>().material;
            _offSet = new Vector2(0f, _scrollSpeed);
        }

        private void Update()
        {
            _material.mainTextureOffset += _offSet * Time.deltaTime;
        }
    }
}