using UnityEngine;

namespace Platformer2D.PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputData _playerInput;

        private void Update()
        {
            _playerInput.ProcessInput();
        }
    }
}