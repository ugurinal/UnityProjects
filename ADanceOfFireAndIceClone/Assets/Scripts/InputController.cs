using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameObject[] _path;

    [SerializeField] private GameObject _fire;
    [SerializeField] private GameObject _ice;

    [SerializeField] private float _rotationSpeed = 150f;

    private bool _isFire = false;

    private bool _isEarly = false;

    private bool _isOnTrigger = false;

    private int _currentBlock = 0;

    //private int _earlyCounter = 0;
    //private int _lateCounter = 0;

    private void Start()
    {
        _isFire = false;
        _isEarly = true;

        _isOnTrigger = false;
    }

    private void Update()
    {
        CheckInput();

        if (_isFire)
        {
            _fire.transform.RotateAround(_ice.transform.position, Vector3.back, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _ice.transform.RotateAround(_fire.transform.position, Vector3.back, _rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckInput()
    {
        if (_isOnTrigger && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            if (_isFire)
            {
                Test(_fire);
            }
            else
            {
                Test(_ice);
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                if (_isEarly)
                {
                    Debug.Log("EARLY !");
                }
                else
                {
                    Debug.Log("LATE !");
                }
            }
        }
    }

    private void Test(GameObject rotatingObj)
    {
        _isFire = !_isFire;

        rotatingObj.transform.position = _path[_currentBlock].transform.position;
        rotatingObj.transform.position = new Vector3(rotatingObj.transform.position.x, rotatingObj.transform.position.y, -1f);

        Debug.Log(_currentBlock);

        if (_currentBlock < _path.Length - 1)
            _currentBlock++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _path[_currentBlock])
        {
            _isOnTrigger = true;
            _isEarly = false;
        }
        else if (collision.gameObject == _path[_currentBlock - 1] || collision.gameObject == _path[_currentBlock - 2] || collision.gameObject == _path[_currentBlock - 3])
        {
            _isEarly = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _path[_currentBlock])
        {
            _isOnTrigger = false;
        }
        else if (collision.gameObject == _path[_currentBlock - 2])
        {
            _isEarly = true;
        }
    }
}   // Input Controller