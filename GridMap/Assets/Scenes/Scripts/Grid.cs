using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;
    private int[,] _gridArray;
    private TextMesh[,] _textMeshArray; // Debug purpose

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _gridArray = new int[width, height];
        _textMeshArray = new TextMesh[width, height];

        DrawGrid();
    }

    private void DrawGrid()
    {
        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                _textMeshArray[i, j] = CreateWorldText("0", null, XYToWorldPosition(i, j) + new Vector3(_cellSize, _cellSize, 0f) * 0.5f, 50, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center, 0);

                Debug.DrawLine(XYToWorldPosition(i, j), XYToWorldPosition(i, j + 1), Color.white, 100f);
                Debug.DrawLine(XYToWorldPosition(i, j), XYToWorldPosition(i + 1, j), Color.white, 100f);
            }
        }

        Debug.DrawLine(XYToWorldPosition(0, _height), XYToWorldPosition(_width, _height), Color.white, 100f);
        Debug.DrawLine(XYToWorldPosition(_width, 0), XYToWorldPosition(_width, _height), Color.white, 100f);
    }

    private TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;

        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        return textMesh;
    }

    private Vector3 XYToWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    private void WorldPositionToXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x = 0;
        int y = 0;

        WorldPositionToXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    private void SetValue(int x, int y, int value)
    {
        if (x < 0 || y < 0 || x >= _width || y >= _height)
        {
            Debug.Log("Invalid parameters.");
            return;
        }

        _gridArray[x, y] = value;
        _textMeshArray[x, y].text = "" + value;
    }

    public int GetValue(Vector3 worldPos)
    {
        int x = 0;
        int y = 0;

        WorldPositionToXY(worldPos, out x, out y);

        return GetValue(x, y);
    }

    private int GetValue(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _width || y >= _height)
        {
            Debug.Log("Invalid parameters.");
            return -1;
        }
        else
        {
            return _gridArray[x, y];
        }
    }
}