using UnityEngine;

public class GamePlay : MonoBehaviour
{
    private static GamePlay _instance;
    public static GamePlay Instance { get => _instance; }

    [SerializeField] private Tile[] _elements;

    [SerializeField] private GameObject _emptySpace;
    private int emptySpaceIndex;

    private int correctTileCounter = 0;

    private bool IsGameEnded = false;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (_instance != this && _instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Shuffle();

        emptySpaceIndex = FindTile(null);
    }

    private void Update()
    {
        if (!IsGameEnded)
            CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                if (Vector2.Distance(hit.transform.position, _emptySpace.transform.position) < 2)
                {
                    Vector2 lastEmptySpace = _emptySpace.transform.position;

                    Tile hitTile = hit.transform.GetComponent<Tile>();

                    _emptySpace.transform.position = hitTile.TargetPos;
                    hitTile.TargetPos = lastEmptySpace;

                    int tileIndex = FindTile(hitTile);
                    SwapTiles(tileIndex, emptySpaceIndex);
                    emptySpaceIndex = tileIndex;
                }
            }
        }
    }

    private void Shuffle()
    {
        do
        {
            for (int i = 0; i < _elements.Length - 1; i++) // -1 is for empty space
            {
                int random = Random.Range(0, _elements.Length - 2);

                Vector2 tempPos = _elements[i].TargetPos;
                _elements[i].TargetPos = _elements[random].TargetPos;
                _elements[random].TargetPos = tempPos;

                SwapTiles(i, random);
            }
        } while (!IsSolvable());
    }

    private void SwapTiles(int first, int second)
    {
        Tile temp = _elements[first];
        _elements[first] = _elements[second];
        _elements[second] = temp;
    }

    public int FindTile(Tile ts)
    {
        for (int i = 0; i < _elements.Length; i++)
        {
            if (_elements[i] == ts)
            {
                return i;
            }
        }

        return -1;  // not found
    }

    private bool IsSolvable()
    {
        int sumOfAllInversion = 0;
        for (int i = 0; i < _elements.Length - 1; i++)
        {
            int inversion = 0;
            for (int j = i; j < _elements.Length - 1; j++)
            {
                if (_elements[j] != null)
                {
                    if (int.Parse(_elements[i].name) > int.Parse(_elements[j].name))
                    {
                        inversion++;
                    }
                }
            }
            sumOfAllInversion += inversion;
        }

        return sumOfAllInversion % 2 == 0;
    }

    public void IncCorrectTile(int num)
    {
        correctTileCounter += num;
        if (correctTileCounter >= _elements.Length - 1)
            IsGameEnded = true;
    }
} // GamePlay