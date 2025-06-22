using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement and rotation speeds")]
    [SerializeField, Range(0, 10)] private float _movementSpeed;    //�������� ������������ ���������
    [SerializeField, Range(0, 20)] private float _rotationSmoothness = 10f;    // ����������� ��������� ��������

    private const float Gravity = -2f;  //�������� ���-����� �������, ����� �� ��������
    
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
        if (_playerViewManager.FirstPerson)
            MoveCharacter(FirstPersonMovement());
        else
            MoveCharacter(SideViewMovement());
    }
        
    public void OnMove(InputAction.CallbackContext callbackContext) //����� ������
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }

    private void MoveCharacter(Vector3 movement)    //KISS
    {
        _playerController.Move(movement * _movementSpeed * Time.deltaTime);
    }

    private Vector3 FirstPersonMovement()   //�������� �� ������� ����
    {
        Vector3 movement = _moveInput.x * transform.right + _moveInput.y * transform.forward + Gravity * transform.up;
        return movement;
        
    }

    private Vector3 SideViewMovement()      //�������� � ����� �����/������/����� ������
    {
        // ��������������� ����������� ��������
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        //moveDirection.Normalize();

        // ������� ������� � ������� ��������
        if (moveDirection != Vector3.zero && !_playerViewManager.FirstPerson)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _playerController.transform.rotation = Quaternion.Slerp(_playerController.transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
        }

        //Vector3 movement = moveDirection + Gravity * transform.up;
        Vector3 movement = _moveInput.x * _playerViewManager.CurrentStaticCamera.transform.right + _moveInput.y * _playerViewManager.CurrentStaticCamera.transform.forward 
            + Gravity * transform.up;   // ���������� �� ���������� ������

        return movement;       
    }
}
