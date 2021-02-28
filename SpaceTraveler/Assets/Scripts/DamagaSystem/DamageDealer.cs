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

        private void Start()
        {
            // checks if this script attached to a player projectile or not,
            // if it is, it updates the damage by player ship
            // because every player ship has its own damage multiplier

            if (tag == "PlayerProjectile")
            {
                _damage *= PlayerController.Instance.GetDamage();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // if laser hit shield, deactivate it
            // enemy can trigger this function too

            if (other.tag == "Shield")
            {
                other.gameObject.SetActive(false);
                Hit();
            }
        }

        public void Hit()
        {
            Destroy(gameObject);    // this function is called from others scripts ontrigger functions
        }
    }
}