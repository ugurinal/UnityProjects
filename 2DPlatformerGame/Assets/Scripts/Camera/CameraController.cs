using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        Vector3 targetPos = _target.position + _offset;
        targetPos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, targetPos, _speed * Time.deltaTime);
    }
}