using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _movementSpeed;
    [SerializeField, Range(0, 20)] private float rotationSmoothness = 10f;    // Коэффициент плавности поворота
    private CharacterController _playerController;

    private Vector3 _moveInput; 

    private const float Gravity = -9.81f;        

    private void Awake()
    {
        _playerController = GetComponent<CharacterController>();        
    }

    private void Update()
    {
        // Нормализованное направление движения
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        moveDirection.Normalize();

        // Плавный поворот в сторону движения
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _playerController.transform.rotation = Quaternion.Slerp(_playerController.transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
        }
        //Vector3 movement = _moveInput.x * transform.right + _moveInput.y * transform.forward + Gravity * transform.up;
        Vector3 movement = moveDirection + Gravity * transform.up;

        _playerController.Move(movement *  _movementSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }
}
