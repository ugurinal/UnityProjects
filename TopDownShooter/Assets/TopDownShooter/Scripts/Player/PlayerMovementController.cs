using TankShooter.PlayerInput;
using UnityEngine;

namespace TankShooter.PlayerMovement
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private InputData _inputData;
        [SerializeField] private PlayerMovementSettings _playerMovementSettings;

        [SerializeField] private Rigidbody _rigidbody;

        private void Update()
        {
            PlayerMovement();
        }

        private void PlayerMovement()
        {
            _rigidbody.MovePosition(_rigidbody.position + (_rigidbody.transform.forward * _inputData.Vertical * _playerMovementSettings.VerticalSpeed));

            _rigidbody.MovePosition(_rigidbody.position + (_rigidbody.transform.right * _inputData.Horizontal * _playerMovementSettings.HorizontalSpeed));
        }
    }
}