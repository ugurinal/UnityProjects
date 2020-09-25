using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBall : MonoBehaviour
{

    Ball _ball;

    // Start is called before the first frame update
    void Start()
    {
        _ball = GameObject.FindObjectOfType<Ball>().GetComponent<Ball>();
    }


    public void LaunchBallButton(){

        _ball.LaunchBallButton();

        GameObject.Find("LaunchBallButton").SetActive(false);
    }

}
