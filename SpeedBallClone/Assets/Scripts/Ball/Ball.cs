using UnityEngine;

namespace SpeedBallClone.Player
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _lerpSpeed;

        private void Start()
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 5f);
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    GetComponent<Rigidbody>().MovePosition(new Vector3(-0.4f, transform.position.y, transform.position.z));
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    GetComponent<Rigidbody>().MovePosition(new Vector3(0.4f, transform.position.y, transform.position.z));
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    if (transform.position.x == 0.4f)
                    {
                        GetComponent<Rigidbody>().MovePosition(new Vector3(-0.4f, transform.position.y, transform.position.z));
                    }
                    else
                    {
                        GetComponent<Rigidbody>().MovePosition(new Vector3(0.4f, transform.position.y, transform.position.z));
                    }
                }
            }
            else
            {
                // screen touch
                if (Input.GetMouseButtonDown(0))
                {
                    if (transform.position.x == 0.4f)
                    {
                        GetComponent<Rigidbody>().MovePosition(new Vector3(-0.4f, transform.position.y, transform.position.z));
                    }
                    else
                    {
                        GetComponent<Rigidbody>().MovePosition(new Vector3(0.4f, transform.position.y, transform.position.z));
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Obstacle"))
            {
                Debug.Log("GameOver!");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Boost"))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0f, 300f, 0f));
            }
        }
    }
}