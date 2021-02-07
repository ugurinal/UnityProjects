using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece
{
    private int _currentRow;
    private int _currentColumn;

    private int _originalRow;
    private int _originalColumn;

    public int CurrentRow { get => _currentRow; }
    public int CurrentColumn { get => _currentColumn; }
    public int OriginalRow { get => _originalRow; }
    public int OriginalColumn { get => _originalColumn; }

    private GameObject _gameObject;
    public GameObject GameObject { get => _gameObject; }
}