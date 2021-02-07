using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }

    private GameObject[] _puzzlePieces;
    private Sprite[] _puzzleImages;

    private PuzzlePiece[,] matrix = new PuzzlePiece[GameVariables.MaxRows, GameVariables.MaxColumns];

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

    private void Start()
    {
        _puzzleIndex = -1;
        SceneManager.sceneLoaded += OnLevelLoad;
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

    private void GameStarted()
    {
        int index = Random.Range(0, GameVariables.MaxSize);

        _puzzlePieces[index].SetActive(false);

        for (int row = 0; row < GameVariables.MaxRows; row++)
        {
            for (int column = 0; column < GameVariables.MaxColumns; column++)
            {
                if (_puzzlePieces[row * GameVariables.MaxColumns + column].activeInHierarchy)
                {
                    Vector3 point = GetScreenCoordinatesFromViewPort(row, column);
                    _puzzlePieces[row * GameVariables.MaxColumns + column].transform.position = point;

                    matrix[row, column] = new PuzzlePiece();
                    matrix[row, column].GameObject = _puzzlePieces[row * GameVariables.MaxColumns + column];

                    matrix[row, column].OriginalRow = row;
                    matrix[row, column].OriginalColumn = column;
                }
                else
                {
                    matrix[row, column] = null;
                }
            }
        }
    }

    private Vector3 GetScreenCoordinatesFromViewPort(int row, int column)
    {
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0.225f * row, 1 - 0.223f * column, 0));
        point.z = 0;

        return point;
    }

    private void OnLevelLoad(Scene scene, LoadSceneMode sceneMode)
    {
        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            if (_puzzleIndex > 0)
            {
                LoadPuzzle();
                GameStarted();
            }
        }
    }

    private void LoadPuzzle()
    {
        _puzzleImages = Resources.LoadAll<Sprite>("Sprites/Puzzle " + _puzzleIndex);

        _puzzlePieces = GameObject.Find("PuzzleHolder").GetComponent<PuzzleHolder>().PuzzlePiece;

        for (int i = 0; i < _puzzlePieces.Length; i++)
        {
            _puzzlePieces[i].GetComponent<SpriteRenderer>().sprite = _puzzleImages[i];
        }
    }
}