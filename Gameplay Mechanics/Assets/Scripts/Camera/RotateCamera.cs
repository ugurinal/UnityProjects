using UnityEngine;
using GameplayMechanics.InputSystem;

namespace GameplayMechanics.Camera
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField] private InputData _inputData;
        [SerializeField] private CameraSettings _cameraSettings;

        private void Update()
        {
            transform.Rotate(Vector3.up, _inputData.MouseX * _cameraSettings.CameraRotationSpeed * Time.deltaTime);
        }
    }
}