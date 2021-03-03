using UnityEngine;
using SpaceTraveler.Player;

namespace SpaceTraveler.DamageSystem
{
    #region DESCRIPTION

    /// <summary>
    /// This is the main class for dealing damage to enemies or player
    /// This script is attached to gameobjects that should deal damage like enemy projectile,
    /// player projectile, enemy itself and player itself
    /// </summary>

    #endregion DESCRIPTION

    public class DamageDealer : MonoBehaviour
    {
        #region FIELDS

        [Header("Damage Properties")]
        [SerializeField] private float _damage = 100;

        public float Damage { get => _damage; }

        #endregion FIELDS

        public void Hit()
        {
            Destroy(gameObject);    // this function is called from others scripts ontrigger functions
        }

        public void IncreaseDamage(float multiplier)
        {
            _damage *= multiplier;
        }
    }
}