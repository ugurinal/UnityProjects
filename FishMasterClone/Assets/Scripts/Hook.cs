using System;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private GameSettings _gameSettings;

    private bool _isGameStarted = false;
    private bool _isGameFinished = false;
    private bool _canFish = false;

    private void Update()
    {
        if (_isGameFinished)
            return;

        if (!_isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isGameStarted = true;
                _canFish = true;
                _camera.transform.position = _gameSettings.CameraStartPosition;
                _collider.enabled = false;
            }

            return;
        }

        if (_canFish)
        {
            StartFishing();
        }
        else
        {
            StopFishing();
        }

        // HandleXMovement();
    }

    private void StartFishing()
    {
        if (!(transform.position.y >= -(_gameSettings.HookLength + _gameSettings.HookOffset)))
        {
            _canFish = false;
            _collider.enabled = true;
            return;
        }

        Vector3 pos = transform.position;
        pos.y -= 0.01f * _gameSettings.HookSpeed;
        transform.position = pos;

        CameraDown();
    }

    private void StopFishing()
    {
        if (transform.position.y >= -4.85f)
        {
            _isGameFinished = true;
            _camera.position = new Vector3(0f, 0f, -10f);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = transform.position;
            pos.y += 0.01f * _gameSettings.HookSpeed;
            transform.position = pos;

            CameraUp();
        }
    }

    private void CameraDown()
    {
        Vector3 pos = _camera.position;
        pos.y -= 0.01f * _gameSettings.HookSpeed;
        _camera.position = pos;
    }

    private void CameraUp()
    {
        Vector3 pos = _camera.position;
        pos.y += 0.01f * _gameSettings.HookSpeed;
        _camera.position = pos;
    }

    // private void HandleXMovement()
    // {
    //     var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     mousePos.y = transform.position.y;
    //     mousePos.z = transform.position.z;
    //
    //     transform.position = mousePos;
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TRIGGER !");
    }
}