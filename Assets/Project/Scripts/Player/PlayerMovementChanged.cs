using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovementChanged : MonoBehaviour
{
    [Header("Movement and rotation speeds")]
    [SerializeField, Range(0, 10)] private float _movementSpeed;    //�������� ������������ ���������   
    [SerializeField, Range(0, 20)] private float _rotationSmoothness;    // ����������� ��������� ��������

    private const float Gravity = -2f;  
    
    private CharacterController _playerController;
    private PlayerViewManager _playerViewManager;

    private Vector3 _moveInput;    

    private void Awake()
    {
        _playerController = GetComponent<CharacterController>();     
        _playerViewManager = GetComponent<PlayerViewManager>();
    }

    private void Update()
    {
        Vector3 motion = CalculateMovement() * _movementSpeed;
        motion += Gravity * Vector3.up;

        _playerController.Move(motion * Time.deltaTime);
    }
        
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveInput = callbackContext.ReadValue<Vector2>();        
    }

    private Vector3 CalculateMovement()
    {
        if (_playerViewManager.FirstPerson)
           return FirstPersonMovement();
        else
            return SideViewMovement();
    }    

    private Vector3 FirstPersonMovement()   //�������� �� ������� ����
    {
        Vector3 movement = _moveInput.x * transform.right + _moveInput.y * transform.forward;
        return movement;
        
    }

    private Vector3 SideViewMovement()      //�������� � ����� �����/������/����� ������
    {
        Transform currentCameraTransform = _playerViewManager.CurrentStaticCamera.transform;

        Vector3 cameraForward = Vector3.ProjectOnPlane(currentCameraTransform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(currentCameraTransform.right, Vector3.up).normalized;
        
        Vector3 movement = _moveInput.x * cameraRight + _moveInput.y * cameraForward;
        movement.Normalize();

        // ������� ������� � ������� ��������
        if (movement != Vector3.zero)
        {            
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
        }

        return movement;       
    }
}
