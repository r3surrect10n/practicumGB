using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("ViewManager")]
    [SerializeField] private ViewManager _viewManager;

    [Header("Components properties")]
    [SerializeField] private Transform _playerCameraPoint;

    [Header("Mouse properties")]
    [SerializeField] private float _mouseSensivityX = 1f;
    [SerializeField] private float _mouseSensivityY = 1f;

    [Header("Camera properties")]
    [SerializeField] private float _maxCameraPitch = 85f;

    private Vector2 _mouseInput;
    private float _cameraPitch = 0f;

    private void Start()
    {
        if (SettingsManager.Instance != null)
        {
            SetSensivity();
        }
        
    }

    private void Update()
    {
        if (_viewManager.IsFirstPerson)
        {
            float yaw = _mouseSensivityX * _mouseInput.x;
            transform.Rotate(Vector3.up * yaw);

            float pitch = _mouseSensivityY * _mouseInput.y;
            _cameraPitch -= pitch;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -_maxCameraPitch, _maxCameraPitch);
            _playerCameraPoint.localEulerAngles = new Vector3(_cameraPitch, 0f, 0f);
        }
    }

    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        _mouseInput = callbackContext.ReadValue<Vector2>();
    }

    public void SetSensivity()
    {
        float sens = SettingsManager.Instance.MouseSensitivity;
        SetSensitivity(sens);
    }

    // Публичный метод для обновления чувствительности извне (например, из SettingsUI)
    public void SetSensitivity(float newSensitivity)
    {
        _mouseSensivityX = newSensitivity;
        _mouseSensivityY = newSensitivity;        
    }
}