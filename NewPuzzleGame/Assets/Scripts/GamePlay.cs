using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private GameObject[] _elements;
    [SerializeField] private GameObject _emptySpace;

    private void Start()
    {
        Shuffle();
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

            if (hit)
            {
                if (Vector2.Distance(hit.transform.position, _emptySpace.transform.position) < 2)
                {
                    Vector2 lastEmptySpace = _emptySpace.transform.position;

                    Tile hitTile = hit.transform.GetComponent<Tile>();

                    _emptySpace.GetComponent<Tile>().TargetPos = hitTile.TargetPos;
                    hitTile.TargetPos = lastEmptySpace;
                }
            }
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i <= 14; i++)
        {
            int random = Random.Range(0, 14);

            Vector2 temp = _elements[i].GetComponent<Tile>().TargetPos;

            _elements[i].GetComponent<Tile>().TargetPos = (_elements[random].GetComponent<Tile>().TargetPos);
            _elements[random].GetComponent<Tile>().TargetPos = (temp);

            GameObject tile = _elements[i];
            _elements[i] = _elements[random];
            _elements[random] = tile;
        }
    }
}