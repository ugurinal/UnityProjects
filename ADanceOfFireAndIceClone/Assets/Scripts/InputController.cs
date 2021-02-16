using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private GameObject _ice;

    [SerializeField] private float _rotationSpeed = 150f;

    private bool _isFire = false;

    // Start is called before the first frame update
    private void Start()
    {
        _isFire = Random.value >= 0.5;

        Debug.Log(_isFire);
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();

        if (_isFire)
        {
            _fire.transform.RotateAround(_ice.transform.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _ice.transform.RotateAround(_fire.transform.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _isFire = !_isFire;
        }
    }
}   // Input Controller