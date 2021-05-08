using UnityEngine;

namespace SpeedBallClone.Player
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _lerpSpeed;

        [SerializeField] private float _speedZ = 5f;
        [SerializeField] private float _speedX = 5f;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            //GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 5f);
        }

        private void Update()
        {
            //Movement();
            HandleMovementTest();
        }

        private void HandleMovementTest()
        {
            Vector3 movement = new Vector3(0f, 0f, _speedZ);

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    movement.x = touch.deltaPosition.x * Time.deltaTime * _speedX;
                }
            }

            movement = movement.normalized * Time.deltaTime * _speedX;

            _rigidbody.MovePosition(_rigidbody.position + movement);

            // Debug.Log(pos);
        }

        private void Movement()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    GetComponent<Rigidbody>()
                        .MovePosition(new Vector3(-0.4f, transform.position.y, transform.position.z));
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    GetComponent<Rigidbody>()
                        .MovePosition(new Vector3(0.4f, transform.position.y, transform.position.z));
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    if (transform.position.x == 0.4f)
                    {
                        GetComponent<Rigidbody>()
                            .MovePosition(new Vector3(-0.4f, transform.position.y, transform.position.z));
                    }
                    else
                    {
                        GetComponent<Rigidbody>()
                            .MovePosition(new Vector3(0.4f, transform.position.y, transform.position.z));
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
                        GetComponent<Rigidbody>()
                            .MovePosition(new Vector3(-0.4f, transform.position.y, transform.position.z));
                    }
                    else
                    {
                        GetComponent<Rigidbody>()
                            .MovePosition(new Vector3(0.4f, transform.position.y, transform.position.z));
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