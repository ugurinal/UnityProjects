using UnityEngine;

namespace GameplayMechanics.Camera
{
    [CreateAssetMenu(menuName = "Gameplay Mechanics/Camera/CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        public float CameraRotationSpeed = 60f;
    }
}