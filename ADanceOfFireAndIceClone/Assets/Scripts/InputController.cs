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

    private int _currentBlock = 0;

    private void Start()
    {
        _isFire = false;
        //_isFire = Random.value >= 0.5;
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
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (_isFire)
            {
                Debug.Log("Fire distance to block : " + Vector2.Distance(_fire.transform.position, _path[_currentBlock].transform.position));
                if (Vector2.Distance(_fire.transform.position, _path[_currentBlock].transform.position) < 0.3f)
                {
                    Debug.Log("FIRE!");
                    _fire.transform.position = _path[_currentBlock].transform.position;
                    _currentBlock++;
                    _isFire = !_isFire;
                }
            }
            else
            {
                Debug.Log("Ice distance to block : " + Vector2.Distance(_ice.transform.position, _path[_currentBlock].transform.position));

                if (Vector2.Distance(_ice.transform.position, _path[_currentBlock].transform.position) < 0.3f)
                {
                    Debug.Log("ICE!");
                    _ice.transform.position = _path[_currentBlock].transform.position;
                    _currentBlock++;
                    _isFire = !_isFire;
                }
            }

            //_isFire = !_isFire;
        }
    }
}   // Input Controller