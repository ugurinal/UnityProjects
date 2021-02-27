using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SpaceTraveler.AudioSystem;
using SpaceTraveler.DamageSystem;
using SpaceTraveler.Enemy;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Player")]
    [SerializeField] private int playerLife = 0;

    [SerializeField] private float playerDmg = 0;

    [Header("Player Movement")]
    [SerializeField] private float MovementSpeedHorizontal = 6f;

    [SerializeField] private float MovementSpeedVertical = 6f;
    [SerializeField] private float MovementSpeedAndroid = 2f;
    [SerializeField] private float xPadding = 0.4f;

    private float minX = 0, maxX = 0, minY = 0, maxY = 0;
    private float minXAndroid = 0, maxXAndroid = 0, minYAndroid = 0, maxYAndroid = 0;

    private bool isAttacking = false;
    private bool canPlayerMove = false;

    [Header("Projectile")]
    [SerializeField] private GameObject projectile;

    [SerializeField] private int shotCounter = 1;
    [SerializeField] private float fireSpeed = 0.18f;

    private Vector3[] oddLaserPositions = new[] {
        new Vector3(0, 1.5f, 0),
        new Vector3(-0.2f, 1.5f, 0),
        new Vector3(0.2f, 1.5f, 0),
        new Vector3(-0.4f, 1.5f, 0),
        new Vector3(0.4f, 1.5f, 0)
    };

    private Vector2[] oddLaserVelocities = new[] {
        new Vector2(0, 10),
        new Vector2(-0.5f, 10),
        new Vector2(0.5f, 10),
        new Vector2(-1, 10),
        new Vector2(1, 10)
    };

    private Vector3[] evenLaserPositions = new[] {
        new Vector3(-0.125f, 1.5f, 0),
        new Vector3(0.125f, 1.5f, 0),
        new Vector3(-0.4f, 1.5f, 0),
        new Vector3(0.4f, 1.5f, 0),
    };

    private Vector2[] evenLaserVelocities = new[] {
        new Vector2(-0.125f, 10),
        new Vector2(0.125f, 10),
        new Vector2(-0.5f, 10),
        new Vector2(0.5f, 10),
    };

    private Vector3 playerLerpPos = new Vector3(0f, -3.5f, 0f);

    private GameManager gameManager;
    private EnemySpawner enemySpawner;
    private LevelController levelController;
    private SoundController soundController;

    private void Awake()
    {
        SetInstance();
    }

    private void Start()
    {
        SetOtherInstances();
        SetUpMovementBoundaries();

        CheckPowerUp();

        levelController.SetPlayerLife(playerLife);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameManager.IsPlayerAlive || gameManager.IsPaused) return;
        if (!canPlayerMove)
        {
            PlayerBotToTopLerp();
            return;
        }

        //if (Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //    KeyboardMovement();
        //}
        //else
        //{
        //    TouchMovement();
        //}

        KeyboardMovement();
        //TouchMovement();

        if (!isAttacking)
        {
            soundController.PlaySFX("projectile");
            Fire();
        }
    }

    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void SetOtherInstances()
    {
        gameManager = GameManager.instance;
        enemySpawner = EnemySpawner.instance;
        levelController = LevelController.instance;
        soundController = SoundController.instance;
    }

    private void Fire()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        isAttacking = true;
        GameObject fireball = null;

        if (shotCounter % 2 == 0)
        {
            for (int i = 0; i < shotCounter; i++)
            {
                fireball = Instantiate(projectile, transform.position + evenLaserPositions[i], Quaternion.identity);
                fireball.GetComponent<Rigidbody2D>().velocity = evenLaserVelocities[i];
            }
        }
        else
        {
            for (int i = 0; i < shotCounter; i++)
            {
                fireball = Instantiate(projectile, transform.position + oddLaserPositions[i], Quaternion.identity);
                fireball.GetComponent<Rigidbody2D>().velocity = oddLaserVelocities[i];
            }
        }

        yield return new WaitForSeconds(fireSpeed);
        isAttacking = false;
    }

    private void TouchMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (isTouchingUI(touch.position))
            {
                return;
            }

            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            touchPos.z = 0f;

            touchPos.x = Mathf.Clamp(touchPos.x, minXAndroid, maxXAndroid);
            touchPos.y = Mathf.Clamp(touchPos.y, minYAndroid, maxYAndroid);

            transform.position = Vector3.Lerp(transform.position, touchPos, 0.1f * MovementSpeedAndroid);
        }
    }

    private void KeyboardMovement()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * MovementSpeedHorizontal;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * MovementSpeedVertical;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position = Vector3.Lerp(transform.position, new Vector3(newXPos, newYPos, -1), 1.0f);
    }

    private void SetUpMovementBoundaries()
    {
        Camera gameCamera = Camera.main;

        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 0.1f;          // Because Player sprite pivot is bottom center
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1.25f;         // Because Player sprite pivot is bottom center

        minXAndroid = gameCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        maxXAndroid = gameCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - xPadding;
        minYAndroid = gameCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.1f;                 // Because Player sprite pivot is bottom center
        maxYAndroid = gameCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - 1.25f;    // Because Player sprite pivot is bottom center
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyProjectile")
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            damageDealer.Hit();

            levelController.DecreasePlayerLife();
        }

        /*

        if (other.tag == "Enemy" || other.tag == "EnemyProjectile")
        {
            playerLife--;
            if (playerLife <= 0)
            {
                levelController.ShowLoseGUI();
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<Animator>().enabled = true;
            }
        }
        */

        //levelController.ShowLoseGUI();
        // when player dies show losegui

        // When animation ends the player object will be deleted. Because in the player animation, animation destroyer script is attached to it.
        // Animation destroyer deletes the game object.
        //Invoke("DestroyGameObject", 1);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private bool isTouchingUI(Vector2 position)
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = position;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            if (raycastResults[0].gameObject.name != "Panel")
            {
                return true;
            }
        }
        return false;
    }

    private void PlayerBotToTopLerp()
    {
        transform.position = Vector3.Lerp(transform.position, playerLerpPos, 3f * Time.deltaTime);
        if (Vector3.Distance(transform.position, playerLerpPos) <= 0.3f)
        {
            canPlayerMove = true;
            enemySpawner.StartEnemySpawn();     //// when player lerp ends start spawning enemies.
        }
    }

    public float GetDamage()
    {
        return playerDmg;
    }

    public void IncrementShotCounter(int value)
    {
        if (shotCounter <= 3)
        {
            shotCounter += value;
        }
        else if (shotCounter == 4)
        {
            shotCounter = 5;
        }
    }

    public void ChangeProjectile(GameObject projectile)
    {
        this.projectile = projectile;
    }

    public void ShieldOn(bool isOn)
    {
        if (!transform.GetChild(1).gameObject.activeSelf)
            transform.GetChild(1).gameObject.SetActive(isOn);
    }

    private void CheckPowerUp()
    {
        if (gameManager.OneShotPU)
        {
            IncrementShotCounter(1);
            gameManager.OneShotPU = false;
        }
        if (gameManager.TwoShotPU)
        {
            IncrementShotCounter(2);
            gameManager.TwoShotPU = false;
        }
        if (gameManager.LifePU)
        {
            playerLife++;
            gameManager.LifePU = false;
        }

        gameManager.SaveData();
    }
}