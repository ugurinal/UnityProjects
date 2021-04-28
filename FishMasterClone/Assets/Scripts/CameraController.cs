using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool _isGameStarted = false;
    private Vector2 _targetPosition = Vector2.zero;
    private float _hookSpeed = 0f;


    public bool IsGameStarted
    {
        get => _isGameStarted;
        set { _isGameStarted = value; }
    }

    public void SetTargetPosition(Vector2 targetPosition, float hookSpeed)
    {
        _targetPosition = targetPosition;
        _hookSpeed = hookSpeed;
    }

    private void Start()
    {
        _isGameStarted = false;
    }

    private void Update()
    {
        if (!_isGameStarted) return;

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (transform.position.y - _targetPosition.y < 0.0001f)
        {
            return;
        }

        if (transform.position.y > _targetPosition.y)
        {
            Vector3 pos = transform.position;
            pos.y -= 0.01f * _hookSpeed;
            transform.position = pos;
        }
        else
        {
            Vector3 pos = transform.position;
            pos.y += 0.01f * _hookSpeed;
            transform.position = pos;
        }
    }

    public void Test(Vector3 pos)
    {
        transform.position = pos;
    }
}