using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables
{
    private static int _maxRows = 4;
    private static int _maxColumns = 4;
    private static int _maxSize = _maxRows * _maxColumns;

    public static int MaxRows { get => _maxRows; }
    public static int MaxColumns { get => _maxColumns; }
}// GameVariables

public enum GameState
{
    Playing,
    Animating,
    End
}