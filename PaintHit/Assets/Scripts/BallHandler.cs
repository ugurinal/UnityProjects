using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _circlePrefab;
    [SerializeField] private float _initialSpeed = 100f;

    [SerializeField] private List<int> _targetSizes;
    [SerializeField] private List<Color> _colors;

    [SerializeField] private float _circleYSize = 2.94f;
    [SerializeField] private Vector3 _initialCirclePosition;

    private int _currentCircle;
    private int _currentBall;
    private List<GameObject> _balls;
    private List<GameObject> _circles;

    private void Start()
    {
        _balls = new List<GameObject>();
        _circles = new List<GameObject>();

        InstantiateNewBalls(_targetSizes[_currentCircle]);
        InstantiateCircle();
    }

    private void Update()
    {
        if (_balls.Count <= 0) return;
        if (Input.GetMouseButtonDown(0))
        {
            ShootBall();
        }
    }

    private void ShootBall()
    {
        _balls[0].GetComponent<Rigidbody>().AddForce(Vector3.forward * _initialSpeed, ForceMode.Impulse);
        _balls.RemoveAt(0);

        _currentBall++;

        if (_balls.Count > 0)
        {
            _balls[0].SetActive(true);
        }

        if (_targetSizes.Count > _currentCircle + 1)
        {
            if (_currentBall >= _targetSizes[_currentCircle])
            {
                _currentCircle++;
                InstantiateNewBalls(_targetSizes[_currentCircle]);
                InstantiateCircle();
            }
        }
    }

    private void InstantiateNewBalls(int size)
    {
        Debug.Log("NEW BALLS!");

        _currentBall = 0;

        for (int i = 0; i < size; i++)
        {
            GameObject ball = Instantiate(_ballPrefab, new Vector3(0f, -5f, 2.5f), Quaternion.identity);
            ball.GetComponent<Ball>().SetColor(_colors[0]);
            ball.SetActive(false);
            _balls.Add(ball);
        }

        _balls[_currentBall].SetActive(true);
    }

    private void InstantiateCircle()
    {
        for (int i = 0; i < _circles.Count; i++)
        {
            Debug.Log("DENEME !");


            Vector3 newPos = new Vector3(_initialCirclePosition.x,
                _initialCirclePosition.y - ((_circles.Count - i) * _circleYSize),
                _initialCirclePosition.z);

            _circles[i].GetComponent<Circle>().SetTargetPos(newPos);
        }

        GameObject circle = Instantiate(_circlePrefab, _initialCirclePosition + new Vector3(3f, 3f, 0f),
            Quaternion.identity);
        circle.GetComponent<Circle>().SetTargetPos(_initialCirclePosition);

        _circles.Add(circle);
    }
}