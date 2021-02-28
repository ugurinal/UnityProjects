using UnityEngine;

[CreateAssetMenu(menuName = "SpaceTraveler/PowerUP/PowerUP Items")]
public class PowerUpConfig : ScriptableObject
{
    [Header("Shield")]
    public GameObject ShieldPU = null;

    [Header("Assistant")]
    [Space(7)]
    public GameObject AssistantPU = null;

    [Header("Shot Increment")]
    [Space(7)]
    public GameObject ShotInc0 = null;
    public float ShotInc0Chance = 50f;

    public GameObject ShotInc1 = null;

    [Header("Projectiles")]
    [Space(7)]
    public GameObject Projectile0 = null;
    public float Proj0Chance = 0f;

    public GameObject Projectile1 = null;
    public float Proj1Chance = 0f;

    public GameObject Projectile2 = null;
    public float Proj2Chance = 0f;

    public GameObject Projectile3 = null;

    [Header("Damage Multiplier")]
    [Space(7)]
    public GameObject DamageMultiplier0 = null;
    public float DamageMultiplier0Chance = 0f;
    public GameObject DamageMultiplier1 = null;
}