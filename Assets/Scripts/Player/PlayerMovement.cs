using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _movementSpeed;

    private CharacterController _playerController;

    private Vector3 _moveInput; 

    private const float Gravity = -9.81f;        

    private void Awake()
    {
        _playerController = GetComponent<CharacterController>();        
    }

    private void Update()
    {        
        Vector3 movement = _moveInput.x * transform.right + _moveInput.y * transform.forward + Gravity * transform.up;  

        _playerController.Move(movement *  _movementSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }
}
