using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rollSpeed = 0.5f;
    private bool isRolling = false;

    private void Update()
    {
        if (isRolling)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            isRolling = true;
            StartCoroutine(Rotate(transform.position + new Vector3(0f, -0.5f, 0.5f), Vector3.right));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            isRolling = true;
            StartCoroutine(Rotate(transform.position + new Vector3(0f, -0.5f, -0.5f), Vector3.left));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            isRolling = true;
            StartCoroutine(Rotate(transform.position + new Vector3(-0.5f, -0.5f, 0f), Vector3.forward));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isRolling = true;
            StartCoroutine(Rotate(transform.position + new Vector3(0.5f, -0.5f, 0f), Vector3.back));
        }
    }

    private IEnumerator Rotate(Vector3 pivot, Vector3 direction)
    {
        for (int i = 1; i <= 90 / _rollSpeed; i++)
        {
            transform.RotateAround(pivot, direction, _rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

        isRolling = false;
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision ENTER");
        GetComponent<Rigidbody>().drag = float.MaxValue;
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision STAY");

        GetComponent<Rigidbody>().drag = float.MaxValue;
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision EXIT");

        GetComponent<Rigidbody>().drag = 0f;
    }*/
}