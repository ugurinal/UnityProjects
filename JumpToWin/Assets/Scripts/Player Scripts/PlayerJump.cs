using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
    public static PlayerJump instance;

    private Rigidbody2D playerBody;
    private Animator animator;

    private float forceX;
    private float forceY;
    private float tresholdX = 7f;
    private float tresholdY = 14f;

    private bool setPower;
    private bool didJump;

    private bool isIncreasing = true;

    [SerializeField] private Image powerIndicator;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        didJump = false;
        setPower = false;

        powerIndicator = GameObject.Find("Power_Indicator").GetComponent<Image>();
    }

    private void Update()
    {
        SetPower();
    }

    private void SetPower()
    {
        if (setPower)
        {
            if (isIncreasing)
            {
                forceX += tresholdX * Time.deltaTime;
                forceY += tresholdY * Time.deltaTime;

                powerIndicator.fillAmount = forceX / 6.5f;

                if (forceX >= 6.5f)
                    isIncreasing = false;

                if (forceY > 13.5f)
                    forceY = 13.5f;
            }
            else
            {
                forceX -= tresholdX * Time.deltaTime;
                forceY -= tresholdY * Time.deltaTime;

                powerIndicator.fillAmount = forceX / 6.5f;

                if (forceX <= 0)
                    isIncreasing = true;

                if (forceY <= 0)
                    forceY = 0;
            }
        }
    }

    public void SetPower(bool setPower)
    {
        this.setPower = setPower;

        if (!setPower)
        {
            Jump();
        }
    }

    private void Jump()
    {
        didJump = true;

        playerBody.velocity = new Vector2(forceX, forceY);
        forceX = 0f;
        forceY = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            if (didJump)
            {
                didJump = false;
                powerIndicator.fillAmount = 0f;
                PlatformSpawner.instance.CreatePlatformAndLerp(collision.transform.position.x);

                GameManager.instance.IncScore();
            }
        }
        else if (collision.tag == "DeadLine")
        {
            GameManager.instance.LoadEndScreen();
        }
    }
}