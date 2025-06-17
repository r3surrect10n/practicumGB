using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement and rotation speeds")]
    [SerializeField, Range(0, 10)] private float _movementSpeed;    //�������� ������������ ���������
    [SerializeField, Range(0, 20)] private float _rotationSmoothness = 10f;    // ����������� ��������� ��������

    private const float Gravity = -9.81f;  
    
    private CharacterController _playerController;

    private Vector3 _moveInput; 

    private void Awake()
    {
        _playerController = GetComponent<CharacterController>();        
    }

    private void Update()
    {
        // ��������������� ����������� ��������
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        moveDirection.Normalize();

        // ������� ������� � ������� ��������
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _playerController.transform.rotation = Quaternion.Slerp(_playerController.transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
        }
        
        Vector3 movement = moveDirection + Gravity * transform.up;
        _playerController.Move(movement *  _movementSpeed * Time.deltaTime);
    }
        
    public void OnMove(InputAction.CallbackContext callbackContext) //����� ������
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }
}
