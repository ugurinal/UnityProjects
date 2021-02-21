using UnityEngine;

namespace TankShooter.PlayerControls
{
    [CreateAssetMenu(menuName = "TankShooter/Tower/TowerRotationSettings")]
    public class TowerRotationSettings : ScriptableObject
    {
        public float TowerRotationSpeed = 5f;
    }
}