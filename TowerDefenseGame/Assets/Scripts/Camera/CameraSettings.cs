using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Camera
{
    [CreateAssetMenu(menuName = "Tower Defense/Camera/CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        public float PanSpeed = 20f;
        public float PanBorderThickness = 10f;

        public float ScrollSpeed = 5f;
        public float CameraMinY = 20f;
        public float CameraMaxY = 70f;
    }
}