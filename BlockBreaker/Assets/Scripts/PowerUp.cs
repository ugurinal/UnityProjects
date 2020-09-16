using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float waitTime;

    //Cached reference
    private Level _level;
    private Ball _ball;
    private Paddle _paddle;

    private void Start()
    {
        _level = FindObjectOfType<Level>();
        _ball = FindObjectOfType<Ball>();
        _paddle = FindObjectOfType<Paddle>();
        Vector2 velocity = new Vector2(0f, -1.125f);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void Update()
    {
        if (transform.position.y > -10) return;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Paddle")) return;

        var objectTag = tag;

        switch (objectTag)
        {
            case "x2":
                StartCoroutine(ScoreBonus());
                break;
            case "Increase":
                StartCoroutine(IncreasePaddle());
                break;
            case "Decrease":
                StartCoroutine(DecreasePaddle());
                break;
            case "WallPower":
                StartCoroutine(PaddleWall());
                break;
            case "FireBall":
                StartCoroutine(FireBall());
                break;
            default:
                Debug.Log("PowerUp not found");
                break;
        }
    }

    IEnumerator ScoreBonus()
    {
        _level.ScoreBonusOn();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        _level.ScoreBonusOff();
    }

    IEnumerator IncreasePaddle()
    {
        _level.IncreasePaddle();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        _level.DecreasePaddle();
    }

    IEnumerator DecreasePaddle()
    {
        _level.DecreasePaddle();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        _level.IncreasePaddle();
    }

    IEnumerator PaddleWall()
    {
        _level.PaddleWallOn();
        _ball.setBallTransform(_paddle.GetPaddleXPos());
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        _level.PaddleWallOff();
    }

    IEnumerator FireBall()
    {
        TriggerSetter(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        TriggerSetter(false);
    }

    public void TriggerSetter(bool isTrigger)
    {
        var blockObjects = Resources.FindObjectsOfTypeAll<Block>();

        foreach (var block in blockObjects)
        {
            if (block.GetComponent<BoxCollider2D>())
            {
                block.GetComponent<BoxCollider2D>().isTrigger = isTrigger;
            }
            else if (block.GetComponent<EdgeCollider2D>())
            {
                block.GetComponent<EdgeCollider2D>().isTrigger = isTrigger;
            }

        }
    }
}