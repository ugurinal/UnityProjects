using UnityEngine;

namespace Platformer2D.PlayerInput
{
    [CreateAssetMenu(menuName = "Platformer2D/Input/Input Data")]
    public class InputData : ScriptableObject
    {
        public float HorizontalMovement;

        public void ProcessInput()
        {
            HorizontalMovement = Input.GetAxis("Horizontal");
        }
    }
}