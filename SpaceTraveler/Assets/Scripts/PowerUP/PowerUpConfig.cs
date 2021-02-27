using UnityEngine;

[CreateAssetMenu(menuName = "PowerUP Config")]
public class PowerUpConfig : ScriptableObject
{
    [Header("Shield")]
    [SerializeField] public GameObject shield;

    [Header("Assistant")]
    [SerializeField] public GameObject assistant;

    [Header("Shot Increment")]
    [SerializeField] public GameObject shotInc0;
    [SerializeField] public float shotInc0Chance = 50f;

    [SerializeField] public GameObject shotInc1;

    [Header("Projectiles")]
    [SerializeField] public GameObject projectile0;
    [SerializeField] public float proj0Chance = 0f;

    [SerializeField] public GameObject projectile1;
    [SerializeField] public float proj1Chance = 0f;

    [SerializeField] public GameObject projectile2;
    [SerializeField] public float proj2Chance = 0f;

    [SerializeField] public GameObject projectile3;


    [Header("Damage Multiplier")]
    [SerializeField] public GameObject damageMultiplier0;
    [SerializeField] public float damageMultiplier0Chance = 0f;

    [SerializeField] public GameObject damageMultiplier1;

}