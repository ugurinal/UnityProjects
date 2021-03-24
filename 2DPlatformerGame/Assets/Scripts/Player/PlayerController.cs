using UnityEngine;
using Platformer2D.PlayerInput;

namespace Platformer2D.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private InputData _playerInput;

        private Rigidbody2D _myBody;
        private Animator _anim;

        private void Start()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            FlipCharacter();
        }

        private void MoveCharacter()
        {
            Vector2 speed = new Vector2(_playerInput.HorizontalMovement * _playerSettings.MovementSpeed * Time.deltaTime, _myBody.velocity.y);
            _myBody.velocity = speed;
        }

        private void FlipCharacter()
        {
            bool isSpeedZero = Mathf.Abs(_playerInput.HorizontalMovement) > Mathf.Epsilon ? false : true;

            if (!isSpeedZero)
            {
                transform.localScale = new Vector3(Mathf.Sign(_playerInput.HorizontalMovement) * 4f, 4f, 4f);
                _anim.SetBool("Running", true);

                //float direction = Mathf.Sign(_playerInput.HorizontalMovement);

                //if (direction == -1f)
                //{
                //    GetComponent<SpriteRenderer>().flipX = true;
                //}
                //else
                //{
                //    GetComponent<SpriteRenderer>().flipX = false;
                //}
            }
            else
            {
                _anim.SetBool("Running", false);
            }
        }
    }
}