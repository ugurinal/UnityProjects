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
                    _emptySpace.transform.position = hit.transform.position;
                    hit.transform.position = lastEmptySpace;
                }
            }
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i < _elements.Length; i++)
        {
            int random = Random.Range(0, _elements.Length);

            Vector2 original = _elements[i].transform.position;
            _elements[i].transform.position = _elements[random].transform.position;
            _elements[random].transform.position = original;
        }
    }
}