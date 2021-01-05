using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShootScript : MonoBehaviour
{
    public float power = 2.0f;
    public float life = 1.0f;
    public float deadSense = 25f;

    public int dots = 30;

    private Vector2 startPos;

    private bool shoot = false;
    private bool aiming = false;
    private bool hitGround = false;

    private GameObject dotParent;
    private List<GameObject> projectilesPath;

    private Rigidbody2D myBody;
    private Collider2D myCollider;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        dotParent = GameObject.Find("DotParent");

        myBody.isKinematic = true;
        myCollider.enabled = true;

        startPos = transform.position;

        projectilesPath = dotParent.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);

        foreach (GameObject child in projectilesPath)
        {
            child.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        if (shoot)
            return;

        if (Input.GetAxis("Fire1") == 1)
        {
            if (!aiming)
            {
                aiming = true;
                startPos = Input.mousePosition;
                CalculatePath();
            }
        }
    }

    private void CalculatePath()
    {
        Vector2 velocity = GetForce(Input.mousePosition) * Time.fixedDeltaTime / myBody.mass;
        Debug.Log("Velocity = " + velocity);
    }

    private Vector2 GetForce(Vector3 mouse)
    {
        Vector2 temp = new Vector2(startPos.x, startPos.y) - new Vector2(mouse.x, mouse.y);
        return temp * power;
    }
}