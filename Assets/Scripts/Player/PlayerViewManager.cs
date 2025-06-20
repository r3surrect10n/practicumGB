using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerViewManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _currentStaticCamera;
    [SerializeField] private CinemachineCamera _firstLookCamera;    

    private int _currentCameraPriority = 100;
    private int _otherCameraPriority = 11;

    public bool FirstPerson { get; private set; }

    private void Start()
    {
        SetView();
    }

    public void OnLookSwitch(InputAction.CallbackContext callbackContext)
    {        
        if (callbackContext.phase != InputActionPhase.Started)
            return;
        
        if (FirstPerson)
        {
            FirstPerson = !FirstPerson;

            _currentStaticCamera.Priority = _firstLookCamera.Priority;
            _firstLookCamera.Priority = _otherCameraPriority;
        }
        else
        {
            FirstPerson = !FirstPerson;

            _firstLookCamera.Priority = _currentStaticCamera.Priority;
            _currentStaticCamera.Priority = _otherCameraPriority;
        }


    }

    public void SetNextStaticCamera(CinemachineCamera nextStaticCamera)
    {
        if (_currentStaticCamera != nextStaticCamera)
        {
            nextStaticCamera.Priority = _currentCameraPriority;
            _currentStaticCamera.Priority = _otherCameraPriority;
            _currentStaticCamera = nextStaticCamera;
        }
    }

    private void SetView()
    {
        if (_currentStaticCamera.Priority > _firstLookCamera.Priority)
            FirstPerson = false;
        else
            FirstPerson = true;
        Debug.Log("View set");
        _currentStaticCamera.Priority = _currentCameraPriority;
        _firstLookCamera.Priority = _otherCameraPriority;        
    }
}
