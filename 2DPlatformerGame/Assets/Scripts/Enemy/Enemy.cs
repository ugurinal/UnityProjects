using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _myRb;

    private float _speed = 5f;

    private void Start()
    {
        _myRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _myRb.velocity = new Vector2(_speed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        _speed *= -1;
    }
}