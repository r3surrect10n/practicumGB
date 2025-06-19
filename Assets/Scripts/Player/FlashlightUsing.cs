using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightUsing : MonoBehaviour
{
    [SerializeField] private GameObject _flashlight;
    [SerializeField] private GameObject _light;

    [SerializeField] private CinemachineCamera _camera;

    private bool _isFlashing;

    private void Start()
    {
        _isFlashing = false;
        _light.SetActive(_isFlashing);
    }

    private void Update()
    {
        _flashlight.transform.localRotation = _camera.transform.localRotation;
    }

    public void OnFlashlight(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
            return;

        _isFlashing = !_isFlashing;
        _light.SetActive(_isFlashing);
    }
}
