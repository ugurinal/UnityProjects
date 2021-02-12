using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private GameObject[] _elements;
    [SerializeField] private GameObject _emptySpace;

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

                    _emptySpace.transform.position = hitTile.TargetPos;
                    hitTile.TargetPos = lastEmptySpace;
                }
            }
        }
    }
}