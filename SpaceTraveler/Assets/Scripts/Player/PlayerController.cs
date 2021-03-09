using SpaceTraveler.AudioSystem;
using SpaceTraveler.DamageSystem;
using SpaceTraveler.Enemy;
using SpaceTraveler.LevelSystem;
using SpaceTraveler.ManagerSystem;
using SpaceTraveler.PowerUPSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceTraveler.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController _instance;
        public static PlayerController Instance { get => _instance; }

        [Header("Player Properties")]
        [SerializeField] private PlayerProperties _playerProperties = null;

        [SerializeField] private GameObject _laserSpawnGO = null;
        [SerializeField] private LineRenderer _lineRenderer = null;

        // screen boundries
        private float _minX = 0, _maxX = 0, _minY = 0, _maxY = 0;
        private float _minXAndroid = 0, _maxXAndroid = 0, _minYAndroid = 0, _maxYAndroid = 0;

        private int _shotCounter = 1;
        private GameObject _currentProjectile = null;
        private int _playerCurrentLife = 1;
        private PlayerProperties.ShootingTypes _currentShootingType;
        private float _currentDamageMultiplier = 0f;
        private float _currentProjectileDamage = 0f;
        private float _currentLaserDamage = 0f;

        private bool _isAttacking = false;
        private bool _canPlayerMove = false;

        private GameManager _gameManager;
        private EnemySpawner _enemySpawner;
        private LevelController _levelController;
        private SoundController _soundController;

        private int _layerMask = 0;

        private void Awake()
        {
            MakeSingleton();
        }

        private void Start()
        {
            SetOtherInstances();
            SetUpMovementBoundaries();

            SetUpPlayer();

            _layerMask = LayerMask.GetMask("Enemy");      // for raycast laser
        }

        private void Update()
        {
            if (!_gameManager.IsPlayerAlive || _gameManager.IsPaused)
            {
                return;
            }
            if (!_canPlayerMove)
            {
                PlayerBotToTopLerp();
                return;
            }

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                KeyboardMovement();
            }
            else
            {
                TouchMovement();
            }

            //KeyboardMovement();
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
            }
        }

        private void SetUpPlayer()
        {
            _currentDamageMultiplier = _playerProperties.ShipDamage;

            _currentShootingType = _playerProperties.ShootingType;

            if (_currentShootingType == PlayerProperties.ShootingTypes.Laser)
            {
                SetUpLaser(null);
            }
            else if (_currentShootingType == PlayerProperties.ShootingTypes.Projectile)
            {
                SetUpProjectile(null);
            }

            _playerCurrentLife = _playerProperties.ShipLife;
            CheckPowerUp();

            _levelController.SetPlayerLife(_playerCurrentLife);

            _laserSpawnGO.SetActive(false);
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

            RaycastHit2D hit = Physics2D.Raycast(_laserSpawnGO.transform.position, Vector2.up, 15f, _layerMask);

            if (hit.collider != null)
            {
                Vector3 hitPos = hit.transform.position;
                target = hitPos.y - _laserSpawnGO.transform.position.y;    // length of enemy that get hits and laser transform y position

                if (hit.transform.GetComponent<Enemy.Enemy>() != null)
                {
                    hit.transform.GetComponent<Enemy.Enemy>().TakeDamage(_currentLaserDamage);
                }
            }
            else
            {
                if (transform.position.y < 0)
                {
                    target = 5f + (transform.position.y * -1);  // 5 = camera orthographicSize
                }
                else
                {
                    target = 5f - transform.position.y;
                }
            }

            // set particle position and line renderer y position
            _laserSpawnGO.transform.GetChild(1).transform.localPosition = new Vector3(_laserSpawnGO.transform.GetChild(1).transform.localPosition.x, target - 0.2f, _laserSpawnGO.transform.GetChild(1).transform.localPosition.z);
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
                    projectile.GetComponent<DamageDealer>().IncreaseDamage(_currentProjectileDamage);
                }
            }
            else
            {
                for (int i = 0; i < _shotCounter; i++)
                {
                    GameObject projectile = Instantiate(_currentProjectile, transform.position + _playerProperties.OddLaserPositions[i], Quaternion.identity);
                    projectile.GetComponent<Rigidbody2D>().velocity = _playerProperties.OddLaserVelocities[i];
                    projectile.GetComponent<DamageDealer>().IncreaseDamage(_currentProjectileDamage);
                }
            }

            yield return new WaitForSeconds(_playerProperties.ShipShootSpeed);
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
            _minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 0.1f - _playerProperties.YPadding;          // Because Player sprite pivot is bottom center
            _maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - 1.25f - _playerProperties.YPadding;         // Because Player sprite pivot is bottom center

            _minXAndroid = gameCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + _playerProperties.XPadding;
            _maxXAndroid = gameCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - _playerProperties.XPadding;
            _minYAndroid = gameCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.1f - _playerProperties.YPadding;                 // Because Player sprite pivot is bottom center
            _maxYAndroid = gameCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - 1.25f - _playerProperties.YPadding;    // Because Player sprite pivot is bottom center
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                // if shield is active
                if (transform.GetChild(1).gameObject.activeSelf)
                {
                    transform.GetChild(1).gameObject.SetActive(false);
                    return;
                }

                other.GetComponent<Enemy.Enemy>().TakeDamage(100f); // 100 testing purpose
                _levelController.DecreasePlayerLife();

                return;
            }

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
                return;
            }

            if (other.CompareTag("+1"))
            {
                IncrementShotCounter(1);
                Destroy(other.gameObject);
                return;
            }
            if (other.CompareTag("+2"))
            {
                IncrementShotCounter(2);
                Destroy(other.gameObject);
                return;
            }

            if (other.CompareTag("PowerUPProjectiles"))
            {
                PowerUP powerUp = other.GetComponent<PowerUP>();

                if (powerUp.ShootingTypes == PlayerProperties.ShootingTypes.Laser)
                {
                    SetUpLaser(powerUp);
                }
                else if (powerUp.ShootingTypes == PlayerProperties.ShootingTypes.Projectile)
                {
                    SetUpProjectile(powerUp);
                }

                Destroy(other.gameObject);
                return;
            }

            if (other.CompareTag("PowerUPShield"))
            {
                ShieldOn(true);
                Destroy(other.gameObject);
                return;
            }
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

                if (_currentShootingType == PlayerProperties.ShootingTypes.Laser)
                {
                    _laserSpawnGO.SetActive(true);
                }
            }
        }

        public float GetDamage()
        {
            return _playerProperties.ShipDamage;
        }

        public void IncrementShotCounter(int value)
        {
            if (_currentShootingType == PlayerProperties.ShootingTypes.Projectile)
            {
                _shotCounter += value;

                if (_shotCounter >= 5)
                    _shotCounter = 5;
            }
        }

        private void SetUpLaser(PowerUP powerUP)
        {
            if (powerUP == null)
            {
                _lineRenderer.material = _playerProperties.LaserMaterial;
            }
            else
            {
                _lineRenderer.material = powerUP.LaserMaterials[0];
            }

            _currentLaserDamage = _lineRenderer.material.GetFloat("Laser_Damage") * _currentDamageMultiplier;
            _currentShootingType = PlayerProperties.ShootingTypes.Laser;

            // set particle color based on laser material
            for (int i = 0; i < 2; i++)
            {
                var mainParticle = _laserSpawnGO.transform.GetChild(i).transform.GetChild(1).GetComponent<ParticleSystem>().main;
                mainParticle.startColor = _lineRenderer.material.GetColor("Laser_ParticleColor");
            }

            _laserSpawnGO.SetActive(true);
        }

        private void SetUpProjectile(PowerUP powerUP)
        {
            _laserSpawnGO.SetActive(false);

            if (powerUP == null)
            {
                _currentProjectile = _playerProperties.ProjectilePrefab;
                _shotCounter = 1;
            }
            else
            {
                _currentProjectile = powerUP.Projectile[0];
            }

            _currentProjectileDamage = _currentProjectile.GetComponent<DamageDealer>().Damage * _playerProperties.ShipDamage;
            _currentShootingType = PlayerProperties.ShootingTypes.Projectile;
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