using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayMechanics.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemySettings _enemySettings;

        [SerializeField] private Rigidbody _myBody;

        private Transform _player;

        private void Start()
        {
            _player = GameObject.Find("Player").transform;
        }

        private void FixedUpdate()
        {
            Vector3 lookDirection = (_player.position - transform.position).normalized;
            _myBody.AddForce(lookDirection * _enemySettings.EnemySpeed * Time.deltaTime);
        }
    }
}