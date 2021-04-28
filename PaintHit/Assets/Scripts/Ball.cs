using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Color _color;

    public void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("COLLISION !");
    }
}