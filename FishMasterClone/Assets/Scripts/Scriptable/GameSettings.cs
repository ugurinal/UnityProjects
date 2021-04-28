using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FishMaster/Game/GameSettings")]
public class GameSettings : ScriptableObject
{
    public Vector3 CameraStartPosition = Vector3.zero;

    public float HookSpeed = 5f;
    public float HookLength = 60f;
    public float HookOffset = 1f;
}
