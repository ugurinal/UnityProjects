using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialX;
    [SerializeField] private float initialY;
    [SerializeField] private float xSpeed; // Launch speed
    [SerializeField] private float ySpeed;

    //cached references
    private Paddle _paddle;
    private Level _level;

    // global variables
    private Vector2 _diffBallToPaddle;
    private bool _hasStarted;

    private void Start()
    {
        _level = FindObjectOfType<Level>();
        _paddle = FindObjectOfType<Paddle>();
        _hasStarted = false;

        var transformVar = transform;
        transformVar.position = new Vector2(initialX, initialY);
        _diffBallToPaddle = transformVar.position - _paddle.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_hasStarted) return;
        StickBallToPaddle();
        //LaunchTheBall();
    }

    private void LaunchTheBall()
    {
        if (Input.GetMouseButton(0) && !_level.GetIsPaused())
        {
            var mousePosX = Input.mousePosition.x / 100;
            var mousePosY = Input.mousePosition.y / 100;
            Vector2 mousePos = new Vector2(mousePosX, mousePosY);

            float absX = Math.Abs(transform.position.x - mousePosX);
            float absY = Math.Abs(transform.position.y - mousePosY);

            if (absX < 0.5 && absY < 0.5)
            {
                _hasStarted = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);
            }
        }
    }

    public void LaunchBallButton(){
        

        GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, ySpeed);
        _hasStarted = true;
        
    }

    private void StickBallToPaddle()
    {
        Vector2 padPos = new Vector2(_paddle.transform.position.x, _paddle.transform.position.y);
        transform.position = padPos + _diffBallToPaddle;
    }

    public bool GetHasStarted()
    {
        return _hasStarted;
    }

    public void setBallTransform(float xPos)
    {
        Vector2 pos = new Vector2(xPos, transform.position.y);
        transform.position = pos;

    }
}