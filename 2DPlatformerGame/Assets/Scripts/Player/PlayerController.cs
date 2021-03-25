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
        private Collider2D _myCollider;

        private void Start()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _myCollider = GetComponent<Collider2D>();
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            Climb();
            Jump();
            FlipCharacter();
        }

        private void MoveCharacter()
        {
            Vector2 speed = new Vector2(_playerInput.HorizontalAxis * _playerSettings.MovementSpeed, _myBody.velocity.y);
            _myBody.velocity = speed;
        }

        private void FlipCharacter()
        {
            bool playerHasSpeed = Mathf.Abs(_playerInput.HorizontalAxis) > Mathf.Epsilon;

            _anim.SetBool("Running", playerHasSpeed);

            if (playerHasSpeed)
            {
                transform.localScale = new Vector3(Mathf.Sign(_playerInput.HorizontalAxis) * 1f, 1f, 1f);
            }
        }

        private void Jump()
        {
            if (!_myCollider.IsTouchingLayers())    // if its not touching ground or layer
                return;
            Vector2 jumpVelecity = new Vector2(0f, _playerInput.JumpAxis * _playerSettings.JumpSpeed);
            _myBody.velocity += jumpVelecity;
        }

        private void Climb()
        {
            if (!_myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                _anim.SetBool("Climbing", false);
                return;
            }

            Vector2 speed = new Vector2(_myBody.velocity.x, _playerInput.VerticalAxis * _playerSettings.MovementSpeed);
            _myBody.velocity = speed;

            bool playerHasSpeed = Mathf.Abs(_playerInput.VerticalAxis) > Mathf.Epsilon;
            if (playerHasSpeed)
                _anim.SetBool("Climbing", true);
        }
    }
}