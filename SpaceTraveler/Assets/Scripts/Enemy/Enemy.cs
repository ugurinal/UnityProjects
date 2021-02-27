using UnityEngine;
using SpaceTraveler.DamageSystem;
using SpaceTraveler.LevelSystem;

namespace SpaceTraveler.Enemy
{
    #region DESCRIPTION

    /// <summary>
    /// This is the enemy class that stores enemies properties like health,speed etc
    /// This script also used for shooting and spawning power ups.
    /// This script doesn't change the transform (position) of the enemy.
    /// </summary>

    #endregion DESCRIPTION

    public class Enemy : MonoBehaviour
    {
        #region FIELDS

        // power up
        [Header("Power UP Properties")]
        [SerializeField] private PowerUpConfig _powerUps = null;    // store power ups informations

        // enemy properties
        [Header("Enemy Properties")]
        [SerializeField] private EnemyProperties _enemyProperties;
        private float _curHealth = 0f;
        private float _shotTimeCounter = 0f;            // this gets random value within the range of mintime and max time
        private bool _isAlive = true;                                    // state of enemy, true by default

        private Animator _animator = null;                               // animator component of gameobject that this script is attached to

        private LevelController _levelController = null;                 // we need this script in order the decrease the number of enemies in the scene
        private float _powerUpChance = 0;                                // this will be assigned from level controller
                                                                         // power up chance is unique in every level
                                                                         // power up chance is based on level not enemy

        #endregion FIELDS

        private void Start()
        {
            _curHealth = _enemyProperties.MaxHealth;

            _animator = GetComponent<Animator>();
            _levelController = LevelController.Instance;
            _powerUpChance = _levelController.PowerUpChance;

            _shotTimeCounter = Random.Range(_enemyProperties.MinTimeBetweenShots, _enemyProperties.MaxTimeBetweenShots);
        }

        private void Update()
        {
            if (!_isAlive) return;

            _shotTimeCounter -= Time.deltaTime;

            if (_shotTimeCounter <= 0f)
            {
                Shoot();
                _shotTimeCounter = Random.Range(_enemyProperties.MinTimeBetweenShots, _enemyProperties.MaxTimeBetweenShots);
            }
        }

        private void Shoot()
        {
            GameObject enemyShot = Instantiate(_enemyProperties.Projectile, transform.position, Quaternion.identity);
            enemyShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -_enemyProperties.ShotSpeed);   // shot speed in minus because it goes downward
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isAlive) return;

            if (other.CompareTag("Player"))
            {
                _levelController.DecreasePlayerLife();
                KillThisEnemy();

                return;
            }

            if (other.CompareTag("Shield"))
            {
                KillThisEnemy();
                // damage dealer script that is attached to this game object will take care of shield
                // so we don't need to do anything in here
                return;
            }

            if (other.CompareTag("PlayerProjectile"))
            {
                DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

                _curHealth -= damageDealer.Damage;

                other.GetComponent<Animator>().enabled = true;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

                if (_curHealth <= 0)
                {
                    KillThisEnemy();
                }

                return;
            }
        }

        private void KillThisEnemy()
        {
            _isAlive = false;
            _levelController.DestroyEnemy(_enemyProperties.ScoreToAdd, _enemyProperties.CoinToEarn);
            SpawnPowerUP();

            _animator.enabled = true;    // // this will destroy this (enemy) game object when animation ends
                                         // so no need do destroy in here
            Destroy(gameObject.GetComponent<PolygonCollider2D>());  // destroy collider just in case
        }

        // spawn power up when enemy dies
        private void SpawnPowerUP()
        {
            float chance = Random.Range(0f, 100f);

            if (chance <= _powerUpChance)
            {
                int whichPowerUp = (int)Random.Range(0f, 5f);

                // if powerup == 0 instantiate a shield
                // if powerup == 1 instantiate an assistant (a minigun that is attached to ship for example)
                // if powerup == 2 instantiate shot increment
                // if powerup == 3 instantiate a projectile
                // if powerup == 4 instantiate a damage multiplier
                // if powerup == 5 instabtiate a score multiplier

                switch (whichPowerUp)
                {
                    case 0:
                        Instantiate(_powerUps.shield, transform.position, Quaternion.identity);
                        break;

                    case 1:
                        Debug.Log("Instantiate an assistant");
                        break;

                    case 2:
                        int whichInc = (int)Random.Range(0f, 100);
                        if (whichInc <= _powerUps.shotInc0Chance)
                        {
                            Instantiate(_powerUps.shotInc1, transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(_powerUps.shotInc0, transform.position, Quaternion.identity);
                        }
                        break;

                    case 3:
                        int whichProjectile = (int)Random.Range(0f, 100);
                        if (whichProjectile <= _powerUps.proj0Chance)
                        {
                            Instantiate(_powerUps.projectile0, transform.position, Quaternion.identity);
                        }
                        else if (whichProjectile <= _powerUps.proj1Chance)
                        {
                            Instantiate(_powerUps.projectile1, transform.position, Quaternion.identity);
                        }
                        else if (whichProjectile <= _powerUps.proj2Chance)
                        {
                            Instantiate(_powerUps.projectile2, transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(_powerUps.projectile3, transform.position, Quaternion.identity);
                        }
                        break;

                    case 4:
                        Debug.Log("Instantiate a damage multiplier");
                        break;

                    case 5:
                        Debug.Log("Instantiate a score multiplier");
                        break;

                    default:
                        Debug.Log("Default power up");
                        break;
                }
            }
        }
    }
}