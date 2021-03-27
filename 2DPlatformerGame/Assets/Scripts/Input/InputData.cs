using UnityEngine;

namespace Platformer2D.PlayerInput
{
    [CreateAssetMenu(menuName = "Platformer2D/Input/Input Data")]
    public class InputData : ScriptableObject
    {
        public float HorizontalAxis;
        public float VerticalAxis;
        public float JumpAxis;

        public void ProcessInput()
        {
            HorizontalAxis = Input.GetAxisRaw("Horizontal");
            VerticalAxis = Input.GetAxisRaw("Vertical");
            JumpAxis = Input.GetAxis("Jump");
        }
    }
}