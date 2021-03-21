using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Tower
{
    [CreateAssetMenu(menuName = "Tower Defense/Tower/Tower Config")]
    public class TowerConfig : ScriptableObject
    {
        [Header("Tower Settings")]
        public float TowerRange;
        public float RotationSpeed;
        public Vector3 TowerInstantiateOffset;

        [Header("Projectile Settings")]
        [Space(10f)]
        public GameObject ProjectilePrefab;
        public float FireSpeed;
    }
}