using UnityEngine;
using TankShooter.PlayerInput;

namespace TankShooter.PlayerControls
{
    public class TowerRotationController : MonoBehaviour
    {
        [Header("Tower Transform")]
        [SerializeField] private Transform _towerTransform;

        [SerializeField] private InputData _rotationInputData;
        [SerializeField] private TowerRotationSettings _towerRotationSettings;

        private void Update()
        {
            _towerTransform.Rotate(0f, _rotationInputData.Horizontal * _towerRotationSettings.TowerRotationSpeed, 0f, Space.Self);
        }
    }
}