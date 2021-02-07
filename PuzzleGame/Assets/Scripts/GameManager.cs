using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    private GameObject[] _puzzlePieces;
    private Sprite[] _puzzleImages;

    private Vector3 _screenPosToAnimate;
    private PuzzlePiece _pieceToAnimate;

    private int _toAnimateRow;
    private int _toAnimateColumn;

    private float _animationSpeed;
    private int _puzzleIndex;
    private GameState _gameState;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SelectPuzzleIndex(int puzzleIndex)
    {
        _puzzleIndex = puzzleIndex;
    }

    public void LoadPuzzle()
    {
        _puzzleImages = Resources.LoadAll<Sprite>("Sprites/Puzzle " + _puzzleIndex);

        _puzzlePieces = GameObject.Find("PuzzleHolder").GetComponent<PuzzleHolder>().PuzzlePiece;
    }
}