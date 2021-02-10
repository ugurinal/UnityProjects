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

    private GameState _gameState;

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

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
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
                    _puzzlePieces[row * GameVariables.MaxColumns + column].transform.position = GetScreenPointFromViewPort(row, column);

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
        Shuffle();
        _gameState = GameState.Playing;
    }

    private void Shuffle()
    {
        for (int row = 0; row < GameVariables.MaxRows; row++)
        {
            for (int column = 0; column < GameVariables.MaxColumns; column++)
            {
                if (matrix[row, column] == null)
                    continue;

                int randomRow = Random.Range(0, GameVariables.MaxRows);
                int randomColumn = Random.Range(0, GameVariables.MaxColumns);

                SwapElements(row, column, randomRow, randomColumn);
            }
        }
    }

    private void SwapElements(int row, int column, int randomRow, int randomColumn)
    {
        PuzzlePiece temp = matrix[row, column];

        matrix[row, column] = matrix[randomRow, randomColumn];
        matrix[randomRow, randomColumn] = temp;

        if (matrix[row, column] != null)
        {
            matrix[row, column].GameObject.transform.position = GetScreenPointFromViewPort(row, column);

            matrix[row, column].CurrentRow = row;
            matrix[row, column].CurrentColumn = column;
        }

        matrix[randomRow, randomColumn].GameObject.transform.position = GetScreenPointFromViewPort(randomRow, randomColumn);
        matrix[randomRow, randomColumn].CurrentRow = randomRow;
        matrix[randomRow, randomColumn].CurrentColumn = randomColumn;
    }

    private Vector3 GetScreenPointFromViewPort(int row, int column)
    {
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(0.1913f * row, 1 - 0.1913f * column, 0)) + new Vector3(1f, -0.5f, 0f);
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