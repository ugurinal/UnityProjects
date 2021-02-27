using UnityEngine;
using SpaceTraveler.DamageSystem;

namespace SpaceTraveler.Enemy
{
    public class Enemy : MonoBehaviour
    {
        #region DESCRIPTION

        //  *********************************************************************************************
        //  * This is the enemy class that stores enemies properties like health,speed etc              *
        //  * This script also used for shooting and spawning power ups.                                *
        //  * This script doesn't change the transform (position) of the enemy.                         *
        //  *********************************************************************************************

        #endregion DESCRIPTION

        #region FIELDS

        // power up
        [Header("Power UPs")]
        [SerializeField] private PowerUpConfig powerUps = null;    // store power ups informations

        // enemy properties
        [Header("Enemy Properties")]
        [Space(10)]
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float curHealth = 0f;
        [SerializeField] private int scoreToAdd = 0;        // when this enemy die how much score will player get
        [SerializeField] private int coinToEarn = 0;        // when this enemy die how much coin will player earn

        // player pro
        [Header("Projectile Properties")]
        [Space(10)]
        [SerializeField] private GameObject projectile = null;          // projectile prefab that enemy shoots
        [SerializeField] private float shotTimeCounter = 0f;            // this gets random value within the range of mintime and max time
        [SerializeField] private float minTimeBetweenShots = 0.2f;      // min time to shoot
        [SerializeField] private float maxTimeBetweenShots = 4f;        // max time to shoot
        [SerializeField] private float shotSpeed = 0f;                  // speed of projectile

        private Animator animator = null;                               // animator component of gameobject that this script is attached to
        private bool isAlive = true;                                    // state of enemy, true by default

        private LevelController levelController = null;                 // we need this script in order the decrease the number of enemies in the scene
        private float powerUpChance = 0;                                // this will be assigned from level controller
                                                                        // power up chance is unique in every level

        #endregion FIELDS

        private void Start()
        {
            curHealth = maxHealth;

            animator = GetComponent<Animator>();
            levelController = LevelController.instance;
            powerUpChance = levelController.powerUpChance;

            shotTimeCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }

        private void Update()
        {
            if (!isAlive) return;

            shotTimeCounter -= Time.deltaTime;

            if (shotTimeCounter <= 0f)
            {
                Shoot();
                shotTimeCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
        }

        private void Shoot()
        {
            GameObject enemyShot = Instantiate(projectile, transform.position, Quaternion.identity);
            enemyShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -shotSpeed);   // shot speed in minus because it goes downward
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!isAlive) return;

            string otherTag = other.tag;

            if (otherTag == "Player")
            {
                levelController.DecreasePlayerLife();
                KillThisEnemy();

                return;
            }

            if (otherTag == "Shield")
            {
                KillThisEnemy();
                // damage dealer script that is attached to this game object will take care of shield
                // so we don't need to do anything in here
                return;
            }

            if (otherTag == "PlayerProjectile")
            {
                DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

                curHealth -= damageDealer.Damage;

                other.GetComponent<Animator>().enabled = true;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

                if (curHealth <= 0)
                {
                    KillThisEnemy();
                }

                return;
            }
        }

        private void KillThisEnemy()
        {
            isAlive = false;
            levelController.DestroyEnemy(scoreToAdd, coinToEarn);
            SpawnPowerUP();

            animator.enabled = true;    // // this will destroy this (enemy) game object when animation ends
                                        // so no need do destroy in here
            Destroy(gameObject.GetComponent<PolygonCollider2D>());  // destroy collider just in case
        }

        // spawn power up when enemy dies
        private void SpawnPowerUP()
        {
            float chance = Random.Range(0f, 100f);

            if (chance <= powerUpChance)
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
                        Instantiate(powerUps.shield, transform.position, Quaternion.identity);
                        break;

                    case 1:
                        Debug.Log("Instantiate an assistant");
                        break;

                    case 2:
                        int whichInc = (int)Random.Range(0f, 100);
                        if (whichInc <= powerUps.shotInc0Chance)
                        {
                            Instantiate(powerUps.shotInc1, transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(powerUps.shotInc0, transform.position, Quaternion.identity);
                        }
                        break;

                    case 3:
                        int whichProjectile = (int)Random.Range(0f, 100);
                        if (whichProjectile <= powerUps.proj0Chance)
                        {
                            Instantiate(powerUps.projectile0, transform.position, Quaternion.identity);
                        }
                        else if (whichProjectile <= powerUps.proj1Chance)
                        {
                            Instantiate(powerUps.projectile1, transform.position, Quaternion.identity);
                        }
                        else if (whichProjectile <= powerUps.proj2Chance)
                        {
                            Instantiate(powerUps.projectile2, transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(powerUps.projectile3, transform.position, Quaternion.identity);
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