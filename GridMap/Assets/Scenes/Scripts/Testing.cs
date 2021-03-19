using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid _myGrid;

    private void Start()
    {
        _myGrid = new Grid(4, 2, 15f, new Vector3(-5f, 10f, 0f));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _myGrid.SetValue(GetMouseWorldPosition(Input.mousePosition, Camera.main), 5);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(_myGrid.GetValue(GetMouseWorldPosition(Input.mousePosition, Camera.main)));
        }
    }

    private Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(screenPosition);
        worldPos.z = 0;

        return worldPos;
    }
}