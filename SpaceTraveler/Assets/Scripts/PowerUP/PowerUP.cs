using System.Collections.Generic;
using UnityEngine;
using SpaceTraveler.Player;

namespace SpaceTraveler.PowerUPSystem
{
    public class PowerUP : MonoBehaviour
    {
        public List<GameObject> Projectile = null;
        public PlayerProperties.ShootingTypes ShootingTypes;
        public List<Material> LaserMaterials = null;
    }
}