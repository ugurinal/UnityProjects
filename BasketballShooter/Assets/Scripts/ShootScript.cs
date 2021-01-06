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
        myCollider.enabled = false;

        startPos = transform.position;

        projectilesPath = dotParent.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);

        for (int i = 0; i < projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = false;
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
        {
            return;
        }

        if (Input.GetAxis("Fire1") == 1)
        {
            if (!aiming)
            {
                aiming = true;
                startPos = Input.mousePosition;
                CalculatePath();
                ShowPath();
            }
            else
            {
                CalculatePath();
            }
        }
        else if (aiming && !shoot)
        {
            if (InReleaseZone(Input.mousePosition) || InDeadZone(Input.mousePosition))
            {
                aiming = false;
                HidePath();
                return;
            }

            myBody.isKinematic = false;
            myCollider.enabled = true;

            shoot = true;
            aiming = false;

            Debug.Log("Inside add force");

            myBody.AddForce(GetForce(Input.mousePosition));
            HidePath();
        }
    }

    private bool InDeadZone(Vector2 mousePos)
    {
        if (Mathf.Abs(startPos.x - mousePos.x) <= deadSense && Mathf.Abs(startPos.y - mousePos.y) <= deadSense)
        {
            return true;
        }
        return false;
    }

    private bool InReleaseZone(Vector2 mousePos)
    {
        if (mousePos.x <= 70)
        {
            return true;
        }
        return false;
    }

    private void CalculatePath()
    {
        Vector2 velocity = GetForce(Input.mousePosition) * Time.fixedDeltaTime / myBody.mass;

        for (int i = 0; i < projectilesPath.Count; i++)
        {
            projectilesPath[i].GetComponent<Renderer>().enabled = true;

            float time = i / (float)dots; // dots is 30

            Vector3 point = PathPoint(transform.position, velocity, time);
            point.z = 1.0f;

            projectilesPath[i].transform.position = point;
        }
    }

    private Vector2 GetForce(Vector3 mouse)
    {
        Vector2 temp = new Vector2(startPos.x, startPos.y) - new Vector2(mouse.x, mouse.y);
        return temp * power;
    }

    private Vector2 PathPoint(Vector2 startPos, Vector2 startVel, float t)
    {
        return startPos + startVel * t + 0.5f * Physics2D.gravity * t * t;
    }

    private void ShowPath()
    {
        foreach (GameObject child in projectilesPath)
        {
            child.GetComponent<Renderer>().enabled = true;
        }
    }

    private void HidePath()
    {
        foreach (GameObject child in projectilesPath)
        {
            child.GetComponent<Renderer>().enabled = false;
        }
    }
}