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
        private CapsuleCollider2D _capsuleCollider; //
        private BoxCollider2D _boxCollider; // for jump

        private float _originalGravityScale;

        private void Start()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
            _boxCollider = GetComponent<BoxCollider2D>();

            _originalGravityScale = _myBody.gravityScale;
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            FlipCharacter();
            JumpNew();

            //Jump();
            Climb();
        }

        private void JumpNew()
        {
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y * 0.6f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(transform.position, Vector2.down * transform.localScale.y * 0.6f, Color.green);

            if (hit2D.collider != null)
            {
                Debug.Log(hit2D.transform.name);
                Vector2 jumpVelecity = new Vector2(0f, _playerInput.JumpAxis * _playerSettings.JumpSpeed);
                _myBody.velocity += jumpVelecity;
            }
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
                transform.localScale = new Vector3(Mathf.Sign(_playerInput.HorizontalAxis) * 1f, transform.localScale.y, transform.localScale.z);
            }
        }

        private void Jump()
        {
            if (!_boxCollider.IsTouchingLayers())    // if its not touching ground or layer
                return;
            Vector2 jumpVelecity = new Vector2(0f, _playerInput.JumpAxis * _playerSettings.JumpSpeed);
            _myBody.velocity += jumpVelecity;
        }

        private void Climb()
        {
            if (!_boxCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                _anim.SetBool("Climbing", false);
                _myBody.gravityScale = _originalGravityScale;
                return;
            }

            Vector2 speed = new Vector2(_myBody.velocity.x, _playerInput.VerticalAxis * _playerSettings.MovementSpeed);
            _myBody.velocity = speed;

            _myBody.gravityScale = 0f;

            bool playerHasSpeed = Mathf.Abs(_playerInput.VerticalAxis) > Mathf.Epsilon;
            _anim.SetBool("Climbing", playerHasSpeed);
        }
    }
}