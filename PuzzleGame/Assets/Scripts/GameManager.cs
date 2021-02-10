using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get => _instance; }

    private GameObject[] _puzzlePieces;
    private Sprite[] _puzzleSprites;
    private PuzzlePiece[,] matrix = new PuzzlePiece[GameVariables.MaxRows, GameVariables.MaxColumns];

    private int _selectedPuzzleIndex;

    private void Awake()
    {
        MakeSingleton();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
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

    private void Start()
    {
        _selectedPuzzleIndex = -1;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlay" && _selectedPuzzleIndex > 0)
        {
            LoadPuzzleSprites();
            StartGame();
        }
    }

    private void StartGame()
    {
        int indexToHide = Random.Range(0, GameVariables.MaxSize);
        _puzzlePieces[indexToHide].SetActive(false);

        for (int row = 0; row < GameVariables.MaxRows; row++)
        {
            for (int column = 0; column < GameVariables.MaxColumns; column++)
            {
                if (_puzzlePieces[row * GameVariables.MaxColumns + column].activeInHierarchy)
                {
                    _puzzlePieces[row * GameVariables.MaxColumns + column].transform.position = GetScreenPointFromViewPort(row, column) + new Vector3(0.5f, -0.5f, 0f);

                    matrix[row, column] = new PuzzlePiece();
                    matrix[row, column].GameObject = _puzzlePieces[row * GameVariables.MaxColumns + column].gameObject;
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

    private Vector3 GetScreenPointFromViewPort(int row, int column)
    {
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0.2f * row, 1 - 0.2f * column, 0));
        point.z = 0;
        return point;
    }

    private void LoadPuzzleSprites()
    {
        _puzzleSprites = Resources.LoadAll<Sprite>("Sprites/Puzzle " + _selectedPuzzleIndex);
        _puzzlePieces = GameObject.Find("PuzzleHolder").GetComponent<PuzzleHolder>().PuzzlePiece;

        for (int i = 0; i < _puzzlePieces.Length; i++)
        {
            _puzzlePieces[i].GetComponent<SpriteRenderer>().sprite = _puzzleSprites[i];
        }
    }

    public void SelectPuzzle(int index)
    {
        _selectedPuzzleIndex = index;
    }
}