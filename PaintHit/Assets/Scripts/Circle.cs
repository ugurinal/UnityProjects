using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private Vector3 _targetPos;
    private float _speed = 5f;

    public void SetTargetPos(Vector3 target)
    {
        _targetPos = target;
    }

    private void Update()
    {
        if (Vector3.SqrMagnitude(transform.position - _targetPos) > 0.00001f)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * _speed);
        }

        transform.Rotate(Vector3.up * 0.1f);
    }
}