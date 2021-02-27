using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.Enemy
{
    #region DESCRIPTION

    /// <summary>
    /// This is the enemy pathing class that take care of enemy movement
    /// </summary>

    #endregion DESCRIPTION

    public class EnemyPathing : MonoBehaviour
    {
        #region FIELDS

        private WaveConfig _waveConfig;      // this variable just used for getting waypoints
                                             // it will be assigned by enemy spawner script

        private List<Transform> _waypoints;  // after wave config is assigned
                                             // this will be assigned with data from waveconfig

        private int _waypointIndex = 0;          // where are we

        private float _movementByFrame = 0f;     // movement speed by frame. get movement speed from wave config * deltatime
        private Vector3 _targetPos = Vector3.zero;   // target position

        #endregion FIELDS

        /// <summary>
        /// this function will be called first after awake function
        /// this is called before start() from enemy spawner
        /// enemy spawner class will instantiate an enemy and call this function for the enemy.
        /// </summary>
        /// <param name="waveConfig"></param>
        public void SetWaveConfig(WaveConfig waveConfig)
        {
            _waveConfig = waveConfig;
        }

        private void Start()
        {
            _waypoints = _waveConfig.GetWaypoints();

            transform.position = _waypoints[_waypointIndex].transform.position;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_waypointIndex < _waveConfig.GetWaypoints().Count)
            {
                _targetPos = _waypoints[_waypointIndex].transform.position;
                _movementByFrame = _waveConfig.MovementSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, _targetPos, _movementByFrame);

                // if its in position go next
                if (Vector3.Distance(_targetPos, transform.position) <= 0.05f)       // 0.1 is good
                {
                    _waypointIndex++;
                }
            }
            else
            {
                _waypointIndex = 0;
            }
        }
    }
}