using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Enemy
{
    public class EnemyPathing : MonoBehaviour
    {
        #region DESCRIPTION

        //  *********************************************************************************************
        //  * This is the enemy pathing class that take care of enemy movement                          *
        //  *********************************************************************************************

        #endregion DESCRIPTION

        #region FIELDS

        private WaveConfig waveConfig;      // this variable just used for getting waypoints
                                            // it will be assigned by enemy spawner script

        private List<Transform> waypoints = new List<Transform>();  // after wave config is assigned
                                                                    // this will be assigned with data from waveconfig

        private int waypointIndex = 0;          // where are we

        private float movementByFrame = 0f;     // movement speed by frame. get movement speed from wave config * deltatime
        private Vector3 targetPos = Vector3.zero;   // target position

        #endregion FIELDS

        // this function will be called first after awake function
        // this is called before start from enemy spawner
        // enemy spawner class will instantiate an enemy and call this function for the enemy.
        public void SetWaveConfig(WaveConfig waveConfig)
        {
            this.waveConfig = waveConfig;
        }

        private void Start()
        {
            waypoints = waveConfig.GetWaypoints();

            transform.position = waypoints[waypointIndex].transform.position;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (waypointIndex < waveConfig.GetWaypoints().Count)
            {
                targetPos = waypoints[waypointIndex].transform.position;
                movementByFrame = waveConfig.MoveSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, targetPos, movementByFrame);

                // if its in position go next
                if (Vector3.Distance(targetPos, transform.position) <= 0.05f)       // 0.1 is good
                {
                    waypointIndex++;
                }
            }
            else
            {
                waypointIndex = 0;
            }
        }
    }
}