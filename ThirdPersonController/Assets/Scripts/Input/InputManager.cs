using UnityEngine;

namespace ThirdPlayer.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputData[] _inputData;

        private void Update()
        {
            for (int i = 0; i < _inputData.Length; i++)
            {
                _inputData[i].ProcessInput();
            }
        }
    }
}