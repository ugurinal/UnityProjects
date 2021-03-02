using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SpaceTraveler.AudioSystem;
using SpaceTraveler.DamageSystem;
using SpaceTraveler.Enemy;
using SpaceTraveler.LevelSystem;
using SpaceTraveler.PowerUPSystem;
using SpaceTraveler.ManagerSystem;

namespace SpaceTraveler.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public static PlayerController Instance { get => _instance; }

        [Header("Player Properties")]
        [SerializeField] private PlayerProperties _playerProperties = null;

        // screen boundries
        private float _minX = 0, _maxX = 0, _minY = 0, _maxY = 0;
        private float _minXAndroid = 0, _maxXAndroid = 0, _minYAndroid = 0, _maxYAndroid = 0;

        private int _shotCounter = 1;
        private GameObject _currentProjectile = null;
        private int _playerCurrentLife = 1;
        private PlayerProperties.ShootingTypes _currentShootingType;

        private bool _isAttacking = false;
        private bool _canPlayerMove = false;

        private GameManager _gameManager;
        private EnemySpawner _enemySpawner;
        private LevelController _levelController;
        private SoundController _soundController;

        private int _layerMask = 0;

        private Camera _mainCamera = null;
        private LineRenderer _lineRenderer = null;

        private void Awake()
        {
            MakeSingleton();
        }

        private void Start()
        {
            SetOtherInstances();
            SetUpMovementBoundaries();

            SetUpPlayer();

            _mainCamera = Camera.main;

            _lineRenderer = GetComponent<LineRenderer>();

            _layerMask = LayerMask.GetMask("Enemy");      // for raycast laser
            _currentShootingType = _playerProperties.ShootingType;
        }

        private void Update()
        {
            if (!_gameManager.IsPlayerAlive || _gameManager.IsPaused) return;
            if (!_canPlayerMove)
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

            if (_currentShootingType == PlayerProperties.ShootingTypes.Projectile)
            {
                if (!_isAttacking)
                {
                    _soundController.PlaySFX("projectile");
                    Shoot();
                }
            }
            else if (_currentShootingType == PlayerProperties.ShootingTypes.Laser)
            {
                EnableLaser();

                /*
                RaycastHit2D hit = Physics2D.Raycast(transform.forward, Vector2.up, 50.0f, _layerMask);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * hit.distance, Color.red, 2f);
                    Debug.Log("Hit");
                }
                else
                {
                    Debug.Log("Not hit");
                }
                //Debug.Log("LASER BEAM!!!");*/
            }
        }

        private void SetUpPlayer()
        {
            _currentProjectile = _playerProperties.ProjectilePrefab;
            _shotCounter = 1;
            _playerCurrentLife = 1;
            CheckPowerUp();

            _levelController.SetPlayerLife(_playerCurrentLife);
        }

        private void MakeSingleton()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void SetOtherInstances()
        {
            _gameManager = GameManager.Instance;
            _enemySpawner = EnemySpawner.Instance;
            _levelController = LevelController.Instance;
            _soundController = SoundController.Instance;

            if (_gameManager == null)
            {
                Debug.Log("GameManager is not initialized!");
            }
            if (_enemySpawner == null)
            {
                Debug.Log("EnemySpawner is not initialized!");
            }
            if (_levelController == null)
            {
                Debug.Log("LevelController is not initialized!");
            }
            if (_soundController == null)
            {
                Debug.Log("SoundController is not initialized!");
            }
        }

        private void EnableLaser()
        {
            float target;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 15f, _layerMask);

            if (hit.collider != null)
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);

                Vector3 hitPos = hit.transform.position;
                target = hitPos.y - transform.position.y;
            }
            else
            {
                Debug.Log("Not Hit!");

                if (transform.position.y < 0)
                {
                    target = _mainCamera.orthographicSize + (transform.position.y * -1);
                }
                else
                {
                    target = _mainCamera.orthographicSize - transform.position.y;
                }
            }

            _lineRenderer.SetPosition(1, new Vector3(0f, target, 0f));
        }

        private void Shoot()
        {
            StartCoroutine(ShootCoroutine());
        }

        private IEnumerator ShootCoroutine()
        {
            _isAttacking = true;

            if (_shotCounter % 2 == 0)
            {
                for (int i = 0; i < _shotCounter; i++)
                {
                    GameObject projectile = Instantiate(_currentProjectile, transform.position + _playerProperties.EvenLaserPositions[i], Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity = _playerProperties.EvenLaserVelocities[i];
                }
            }
            else
            {
                for (int i = 0; i < _shotCounter; i++)
                {
                    GameObject projectile = Instantiate(_currentProjectile, transform.position + _playerProperties.OddLaserPositions[i], Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity = _playerProperties.OddLaserVelocities[i];
                }
            }

            yield return new WaitForSeconds(_playerProperties.ShootSpeed);
            _isAttacking = false;
        }

        private void TouchMovement()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (IsTouchingUI(touch.position))
                {
                    return;
                }

                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                touchPos.z = 0f;

                touchPos.x = Mathf.Clamp(touchPos.x, _minXAndroid, _maxXAndroid);
                touchPos.y = Mathf.Clamp(touchPos.y, _minYAndroid, _maxYAndroid);

                transform.position = Vector3.Lerp(transform.position, touchPos, 0.1f * _playerProperties.MovementSpeedAndroid);
            }
        }

        private void KeyboardMovement()
        {
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _playerProperties.MovementSpeedHorizontal;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _playerProperties.MovementSpeedVertical;

            var newXPos = Mathf.Clamp(transform.position.x + deltaX, _minX, _maxX);
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, _minY, _maxY);

            transform.position = Vector3.Lerp(transform.position, new Vector3(newXPos, newYPos, -1), 1.0f);
        }

        private void SetUpMovementBoundaries()
        {
            Camera gameCamera = Camera.main;

            _minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + _playerProperties.XPadding;
            _maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - _playerProperties.XPadding;
            _minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 0.1f;          // Because Player sprite pivot is bottom center
            _maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1.25f;         // Because Player sprite pivot is bottom center

            _minXAndroid = gameCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + _playerProperties.XPadding;
            _maxXAndroid = gameCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - _playerProperties.XPadding;
            _minYAndroid = gameCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.1f;                 // Because Player sprite pivot is bottom center
            _maxYAndroid = gameCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - 1.25f;    // Because Player sprite pivot is bottom center
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.name);

            if (other.CompareTag("EnemyProjectile"))
            {
                // if shield is active
                if (transform.GetChild(1).gameObject.activeSelf)
                {
                    Debug.Log("Shield is active");
                    transform.GetChild(1).gameObject.SetActive(false);
                    other.GetComponent<DamageDealer>().Hit();
                    return;
                }

                DamageDealer damageDealer = other.GetComponent<DamageDealer>();
                damageDealer.Hit();

                _levelController.DecreasePlayerLife();
            }

            if (other.CompareTag("+1"))
            {
                IncrementShotCounter(1);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("+2"))
            {
                IncrementShotCounter(2);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("PowerUPProjectiles"))
            {
                ChangeProjectile(other.GetComponent<PowerUP>().Projectile[0]);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("PowerUPShield"))
            {
                ShieldOn(true);
                Destroy(other.gameObject);
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

        private bool IsTouchingUI(Vector2 position)
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
            transform.position = Vector3.Lerp(transform.position, _playerProperties.PlayerLerpPos, 3f * Time.deltaTime);
            if (Vector3.Distance(transform.position, _playerProperties.PlayerLerpPos) <= 0.3f)
            {
                _canPlayerMove = true;
                _enemySpawner.StartEnemySpawn();     //// when player lerp ends start spawning enemies.

                if (_lineRenderer != null)
                {
                    _lineRenderer.enabled = true;
                    _lineRenderer.SetPosition(0, new Vector3(0f, 1.25f, 0f));
                }
            }
        }

        public float GetDamage()
        {
            return _playerProperties.PlayerDmg;
        }

        public void IncrementShotCounter(int value)
        {
            _shotCounter += value;

            if (_shotCounter >= 5)
                _shotCounter = 5;
        }

        public void ChangeProjectile(GameObject projectile)
        {
            _currentProjectile = projectile;
        }

        public void ShieldOn(bool isOn)
        {
            if (!transform.GetChild(1).gameObject.activeSelf)
                transform.GetChild(1).gameObject.SetActive(isOn);
        }

        private void CheckPowerUp()
        {
            if (_gameManager.OneShotPU)
            {
                IncrementShotCounter(1);
                _gameManager.OneShotPU = false;
            }
            if (_gameManager.TwoShotPU)
            {
                IncrementShotCounter(2);
                _gameManager.TwoShotPU = false;
            }
            if (_gameManager.LifePU)
            {
                _playerCurrentLife++;
                _gameManager.LifePU = false;
            }

            _gameManager.SaveData();
        }
    }
}