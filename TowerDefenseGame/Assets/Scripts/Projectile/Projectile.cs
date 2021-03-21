﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;
    private Vector3 _target;

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _target, _projectileSpeed * Time.deltaTime);

        if (transform.position == _target)
        {
            Debug.Log("DESTROY");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER !!!");
        Destroy(gameObject);
        return;
    }
}