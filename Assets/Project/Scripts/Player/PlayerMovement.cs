using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private ViewManager _viewManager;

    [Header("Movement and rotation speeds")]
    [SerializeField, Range(0, 10)] private float _movementSpeed;    //Скорость передвижения персонажа   
    [SerializeField, Range(0, 20)] private float _rotationSmoothness;    // Коэффициент плавности поворота

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _footstepSounds;
    [SerializeField, Range(0, 1)] private float _stepInterval;
    private float _stepTimer = 0f;    

    private const float Gravity = -2f;  
    
    private CharacterController _playerController;

    private Vector3 _moveInput;

    private float _currentSpeed;
    private float _currentStepInterval;

    private void Awake()
    {
        _playerController = GetComponent<CharacterController>();        
    }

    private void Start()
    {
        _currentSpeed = _movementSpeed;
        _currentStepInterval = _stepInterval;
    }

    private void Update()
    {
        Vector3 motion = CalculateMovement() * _movementSpeed;
        motion += Gravity * Vector3.up;

        _playerController.Move(motion * Time.deltaTime);

        HandleFootsteps();
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            _movementSpeed = _currentSpeed * 2f;
            _stepInterval = _currentStepInterval * 0.5f;
        }

        else if (callbackContext.phase == InputActionPhase.Canceled)
        {
            _movementSpeed = _currentSpeed;
            _stepInterval = _currentStepInterval;

            return;
        }
        else 
            return;        
    }

    private Vector3 CalculateMovement()
    {
        if (_viewManager.IsFirstPerson)
           return FirstPersonMovement();
        else
            return /*SideViewMovement()*/ new Vector3(0, 0, 0);
    }
    

    private Vector3 FirstPersonMovement()   //Движение от первого лица
    {
        Vector3 movement = _moveInput.x * transform.right + _moveInput.y * transform.forward;
        return movement;
        
    }

    private Vector3 SideViewMovement()      //Движение с видом сбоку/сверху/везде короче
    {
        Transform currentCameraTransform = _viewManager.CurrentStaticCam.transform;

        Vector3 cameraForward = Vector3.ProjectOnPlane(currentCameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(currentCameraTransform.right, Vector3.up).normalized;
        
        Vector3 movement = _moveInput.x * cameraRight + _moveInput.y * cameraForward;
        movement.Normalize();

        // Плавный поворот в сторону движения
        if (movement != Vector3.zero)
        {            
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
        }

        return movement;       
    }

    private void HandleFootsteps()
    {        
        if (!_playerController.isGrounded || _moveInput == Vector3.zero)
        {
            _stepTimer = _stepInterval;
            return;
        }        

        _stepTimer -= Time.deltaTime;
        if (_stepTimer <= 0f)
        {
            PlayFootstepSound();
            _stepTimer = _stepInterval;
        }
    }

    private void PlayFootstepSound()
    {
        int index = Random.Range(0, _footstepSounds.Length);
        _audioSource.PlayOneShot(_footstepSounds[index]);
    }
}
