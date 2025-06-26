using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerViewManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _currentStaticCamera;
    [SerializeField] private CinemachineCamera _firstLookCamera;

    [SerializeField] private GameObject _noiseImage;
    [SerializeField, Range(0, 5)] private float _noiseTime;

    private PlayerInput _playerInput;

    private Coroutine setCoroutine;

    private int _currentCameraPriority = 100;
    private int _otherCameraPriority = 11;

    public bool FirstPerson { get; private set; }

    public CinemachineCamera CurrentStaticCamera => _currentStaticCamera;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        SetView();
    }

    //public void OnLookSwitch(InputAction.CallbackContext callbackContext)   //Пока выключил, нужен ли нам вид от первого лица?
    //{
    //    if (callbackContext.phase != InputActionPhase.Started)
    //        return;

    //    //return;

    //    if (FirstPerson)
    //    {
    //        FirstPerson = !FirstPerson;

    //        _currentStaticCamera.Priority = _firstLookCamera.Priority;
    //        _firstLookCamera.Priority = _otherCameraPriority;
    //    }
    //    else
    //    {
    //        FirstPerson = !FirstPerson;

    //        _firstLookCamera.Priority = _currentStaticCamera.Priority;
    //        _currentStaticCamera.Priority = _otherCameraPriority;
    //    }
    //}

    public void SetNextCamera(CinemachineCamera nextStaticCamera)     // Переключение камер в помещениях
    {
        if (_currentStaticCamera != nextStaticCamera)
        {
            nextStaticCamera.Priority = _currentCameraPriority;
            _currentStaticCamera.Priority = _otherCameraPriority;
            _currentStaticCamera = nextStaticCamera;
        }
    }

    public void SetView()
    {
        if (_currentStaticCamera.Priority > _firstLookCamera.Priority)
            FirstPerson = false;
        else
            FirstPerson = true;
        
        _currentStaticCamera.Priority = _currentCameraPriority;
        _firstLookCamera.Priority = _otherCameraPriority;        
    }

    public void Set(CinemachineCamera nextCamera)
    {

        if (FirstPerson)
        {
            setCoroutine = StartCoroutine(CamerasPriority(nextCamera, _currentCameraPriority, _otherCameraPriority));
        }
        else
        {
            setCoroutine = StartCoroutine(CamerasPriority(_firstLookCamera, _otherCameraPriority, _currentCameraPriority));
        }

    }

    private IEnumerator CamerasPriority(CinemachineCamera nextCamera, int staticCamera, int firstPersonCamera)
    {
        _noiseImage.SetActive(true);
        _playerInput.enabled = false;

        FirstPerson = !FirstPerson;
        nextCamera.Priority = staticCamera;
        _firstLookCamera.Priority = firstPersonCamera;

        _currentStaticCamera = nextCamera;

        yield return new WaitForSeconds(_noiseTime);

        _noiseImage.SetActive(false);
        _playerInput.enabled = true;

        StopCoroutine(setCoroutine);
    }
}
