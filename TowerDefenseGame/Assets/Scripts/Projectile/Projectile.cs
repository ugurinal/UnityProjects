using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;
    private Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _projectileSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, _target.position) < 0.01f)
        {
            Debug.Log("HIT!");
            Destroy(gameObject);
        }
    }
}