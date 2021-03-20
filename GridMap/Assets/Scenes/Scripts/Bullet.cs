using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Start()
    {
    }

    private void Update()
    {
        if (_target == null)
            return;
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        Vector3 direction = _target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 50f);
    }
}