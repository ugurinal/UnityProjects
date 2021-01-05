using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    private GameObject ball;

    private float minX = -4.7f;
    private float maxX = 8f;
    private float minY = -2.5f;
    private float maxY = 1.5f;

    // Start is called before the first frame update
    private void Start()
    {
        float ballX = Random.Range(minX, maxX);
        float ballY = Random.Range(minY, maxY);

        ball = Instantiate(GameManager.Instance.Balls[GameManager.Instance.SelectedBallIndex], new Vector3(ballX, ballY, 0), Quaternion.identity);

        ball.GetComponent<Rigidbody2D>().isKinematic = true;
    }
}