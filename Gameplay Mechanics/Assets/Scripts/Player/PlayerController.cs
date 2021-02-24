using UnityEngine;
using GameplayMechanics.InputSystem;

namespace GameplayMechanics.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputData _inputData;
        [SerializeField] private PlayerSettings _playerSettings;

        [SerializeField] private Rigidbody myBody;

        private void FixedUpdate()
        {
            myBody.AddForce(Vector3.forward * _inputData.KeyboardVertical * _playerSettings.PlayerSpeed * Time.deltaTime);
            myBody.AddForce(Vector3.right * _inputData.KeyboardHorizontal * _playerSettings.PlayerSpeed * Time.deltaTime);
        }
    }
}