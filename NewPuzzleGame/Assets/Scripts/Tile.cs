using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 TargetPos { get; set; }

    private float _lerpSpeed = 5.0f;

    private void Start()
    {
        TargetPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * _lerpSpeed);
    }
}