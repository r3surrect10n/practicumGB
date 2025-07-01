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

    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;

    private Coroutine setCoroutine;

    private int _currentCameraPriority = 100;
    private int _otherCameraPriority = 10;

    public bool FirstPerson { get; private set; }

    public CinemachineCamera CurrentStaticCamera => _currentStaticCamera;

    private void Awake()
    {        
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        //SetView();
        SetView2();
    }

    //public void SetNextCamera(CinemachineCamera nextStaticCamera)     // Переключение камер в помещениях
    //{
    //    if (_currentStaticCamera != nextStaticCamera)
    //    {
    //        nextStaticCamera.Priority = _currentCameraPriority;
    //        _currentStaticCamera.Priority = _otherCameraPriority;
    //        _currentStaticCamera = nextStaticCamera;
    //    }
    //}

    public void SetInteractionCamera(CinemachineCamera interactionCamera)
    {
        if (FirstPerson)
            CamerasPriority(interactionCamera, _currentCameraPriority, _otherCameraPriority);
        else
            CamerasPriority(_firstLookCamera, _otherCameraPriority, _currentCameraPriority);
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

    public void SetView2()
    {
        FirstPerson = true;

        
        _firstLookCamera.Priority = _currentCameraPriority;
    }

    //public void Set(CinemachineCamera nextCamera)
    //{

    //    if (FirstPerson)
    //    {
    //        setCoroutine = StartCoroutine(CamerasPriority(nextCamera, _currentCameraPriority, _otherCameraPriority));
    //    }
    //    else
    //    {
    //        setCoroutine = StartCoroutine(CamerasPriority(_firstLookCamera, _otherCameraPriority, _currentCameraPriority));
    //    }

    //}

    private void CamerasPriority(CinemachineCamera nextCamera, int interactionCameraPriority, int fpCameraPriority)
    {
        FirstPerson = !FirstPerson;
        _playerMovement.enabled = FirstPerson;
        nextCamera.Priority = interactionCameraPriority;
        _firstLookCamera.Priority= fpCameraPriority;
    }

    //private IEnumerator CamerasPriority(CinemachineCamera nextCamera, int staticCamera, int firstPersonCamera)
    //{
    //    _noiseImage.SetActive(true);
    //    _playerInput.enabled = false;

    //    FirstPerson = !FirstPerson;
    //    nextCamera.Priority = staticCamera;
    //    _firstLookCamera.Priority = firstPersonCamera;

    //    _currentStaticCamera = nextCamera;

    //    yield return new WaitForSeconds(_noiseTime);

    //    _noiseImage.SetActive(false);
    //    _playerInput.enabled = true;

    //    StopCoroutine(setCoroutine);
    //}
}
