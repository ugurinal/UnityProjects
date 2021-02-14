using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpeedBall/Camera/CameraSettings")]
public class CameraSettings : ScriptableObject
{
    public Vector3 PositionOffset;
    public float PositionLerpSpeed;
}