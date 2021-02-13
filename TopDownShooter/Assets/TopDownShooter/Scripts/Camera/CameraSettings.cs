using UnityEngine;

namespace TankShooter.Camera
{
    [CreateAssetMenu(menuName = "TankShooter/Camera/CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        [Header("Rotation")]
        [SerializeField] private float _rotationLerpSpeed = 1f;

        public float RotationLerpSpeed { get => _rotationLerpSpeed; }

        [Header("Position")]
        [SerializeField] private Vector3 _positionOffset = Vector3.zero;

        public Vector3 PositionOffset { get => _positionOffset; }

        [SerializeField] private float _positionLerpSpeed = 1f;
        public float PositionLerpSpeed { get => _positionLerpSpeed; }
    }
}