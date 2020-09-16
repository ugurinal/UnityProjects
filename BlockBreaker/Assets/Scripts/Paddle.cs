using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float initialX; // starting position of paddle
    [SerializeField] private float initialY;

    [SerializeField] private float minX; // The boundary of left
    [SerializeField] private float maxX; // right

    [SerializeField] private float padSpeed; // paddle speed

    // cached reference
    private Level _level;

    private void Awake()
    {
        _level = FindObjectOfType<Level>();
        Vector2 initialPosPad = new Vector2(initialX, initialY);
        transform.position = initialPosPad;
    }

    void Update()
    {

        if (Input.GetMouseButton(0) && !_level.GetIsPaused())
        {
            var mousePosX = Input.mousePosition.x;
            var mousePosY = Input.mousePosition.y;

            var padPosX = transform.position.x;
            Vector2 nextPadPos = new Vector2(padPosX, transform.position.y);

            if (mousePosX <= Screen.width / 2 && mousePosY < Screen.height / 3)
            {
                nextPadPos.x = Mathf.Clamp(padPosX - padSpeed, minX, maxX);
                transform.position = nextPadPos;
            }
            else if (mousePosX > Screen.width / 2 && mousePosY < Screen.height / 3)
            {
                nextPadPos.x = Mathf.Clamp(padPosX + padSpeed, minX, maxX);
                transform.position = nextPadPos;
            }
        }

        /* For keyboard

        if (Input.GetKey("a") && !_level.GetIsPaused())
        {

            var padPosX = transform.position.x;
            Vector2 nextPadPos = new Vector2(padPosX, transform.position.y);
            nextPadPos.x = Mathf.Clamp(padPosX - padSpeed, minX, maxX);
            transform.position = nextPadPos;
        }
        else if (Input.GetKey("d") && !_level.GetIsPaused())
        {
            var padPosX = transform.position.x;
            Vector2 nextPadPos = new Vector2(padPosX, transform.position.y);
            nextPadPos.x = Mathf.Clamp(padPosX + padSpeed, minX, maxX);
            transform.position = nextPadPos;
        }
        */


    }

    public float GetPaddleXPos()
    {
        return transform.position.x;
    }
}