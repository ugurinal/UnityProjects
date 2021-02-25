using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPlayer.InputSystem;

namespace ThirdPlayer.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private InputData _playerInput;
        [SerializeField] private PlayerProperties _playerProperties;

        [SerializeField] private CharacterController _characterController;

        private float _turnSmoothVelocity;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector3 direction = new Vector3(_playerInput.Horizontal, 0f, _playerInput.Vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _playerProperties.turnSmoothtime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _characterController.Move(moveDir.normalized * Time.deltaTime * _playerProperties.MovementSpeed);
            }
        }
    }
}