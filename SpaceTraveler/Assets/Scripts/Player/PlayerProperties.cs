using UnityEngine;

namespace SpaceTraveler.Player
{
    [CreateAssetMenu(menuName = "SpaceTraveler/Player/PlayerProperties")]
    public class PlayerProperties : ScriptableObject
    {
        [Header("Player Properties")]
        public int PlayerLife = 0;
        public float PlayerDmg = 0;
        public float ShootSpeed = 0.18f;

        [Header("Player Movement")]
        [Space(7)]
        public Vector3 PlayerInstantiatePos = new Vector3(0f, -8f - 0f);
        public Vector3 PlayerLerpPos = new Vector3(0f, -3.5f, 0f);

        public float MovementSpeedHorizontal = 6f;
        public float MovementSpeedVertical = 6f;
        public float MovementSpeedAndroid = 2f;
        public float XPadding = 0.4f;

        [Header("Projectile Properties")]
        [Space(7)]
        public GameObject ProjectilePrefab = null;

        [Header("Projectile Directions")]
        [Space(7)]
        public Vector3[] OddLaserPositions = new[] {
        new Vector3(0, 1.5f, 0),
        new Vector3(-0.2f, 1.5f, 0),
        new Vector3(0.2f, 1.5f, 0),
        new Vector3(-0.4f, 1.5f, 0),
        new Vector3(0.4f, 1.5f, 0)
    };

        public Vector2[] OddLaserVelocities = new[] {
        new Vector2(0, 10),
        new Vector2(-0.5f, 10),
        new Vector2(0.5f, 10),
        new Vector2(-1, 10),
        new Vector2(1, 10)
    };

        public Vector3[] EvenLaserPositions = new[] {
        new Vector3(-0.125f, 1.5f, 0),
        new Vector3(0.125f, 1.5f, 0),
        new Vector3(-0.4f, 1.5f, 0),
        new Vector3(0.4f, 1.5f, 0),
    };

        public Vector2[] EvenLaserVelocities = new[] {
        new Vector2(-0.125f, 10),
        new Vector2(0.125f, 10),
        new Vector2(-0.5f, 10),
        new Vector2(0.5f, 10),
    };
    }
}