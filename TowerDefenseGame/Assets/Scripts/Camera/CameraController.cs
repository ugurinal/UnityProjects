using UnityEngine;

namespace TowerDefense.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraSettings _cameraSettings;

        private void Update()
        {
            HandleCameraPosition();
            UpdateCameraHeight();
        }

        private void HandleCameraPosition()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - _cameraSettings.PanBorderThickness)
            {
                transform.Translate(Vector3.forward * _cameraSettings.PanSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= _cameraSettings.PanBorderThickness)
            {
                transform.Translate(Vector3.back * _cameraSettings.PanSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - _cameraSettings.PanBorderThickness)
            {
                transform.Translate(Vector3.right * _cameraSettings.PanSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= _cameraSettings.PanBorderThickness)
            {
                transform.Translate(Vector3.left * _cameraSettings.PanSpeed * Time.deltaTime, Space.World);
            }
        }

        private void UpdateCameraHeight()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Vector3 targetPos = new Vector3(0f, -1 * scroll * _cameraSettings.ScrollSpeed * Time.deltaTime * 500f, 0f);
            Vector3 nextPos = Vector3.Lerp(transform.position, transform.position - targetPos, 1f);
            nextPos.y = Mathf.Clamp(nextPos.y, _cameraSettings.CameraMinY, _cameraSettings.CameraMaxY);
            transform.position = nextPos;
        }
    }
}