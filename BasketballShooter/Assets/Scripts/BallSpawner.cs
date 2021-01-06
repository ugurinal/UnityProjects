using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    private GameObject ball;

    private float minX = -3f;
    private float maxX = 6f;
    private float minY = -3f;
    private float maxY = 3f;

    // Start is called before the first frame update
    private void Start()
    {
        float ballX = Random.Range(minX, maxX);
        float ballY = Random.Range(minY, maxY);

        ball = Instantiate(GameManager.Instance.Balls[GameManager.Instance.SelectedBallIndex], new Vector3(ballX, ballY, 1), Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().isKinematic = true;
    }
}