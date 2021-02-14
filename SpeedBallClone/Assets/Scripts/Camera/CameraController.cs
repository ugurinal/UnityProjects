using UnityEngine;

namespace SpeedBallClone.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private CameraSettings _cameraSettings;

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position + _cameraSettings.PositionOffset, Time.deltaTime * _cameraSettings.PositionLerpSpeed);
        }
    }
}