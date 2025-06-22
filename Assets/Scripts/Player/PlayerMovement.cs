using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement and rotation speeds")]
    [SerializeField, Range(0, 10)] private float _movementSpeed;    //Скорость передвижения персонажа
    [SerializeField, Range(0, 20)] private float _rotationSmoothness = 10f;    // Коэффициент плавности поворота

    private const float Gravity = -2f;  //Поставил все-такие двоечку, чтобы не ракетило
    
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
        
    public void OnMove(InputAction.CallbackContext callbackContext) //Прием инпута
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }

    private void MoveCharacter(Vector3 movement)    //KISS
    {
        _playerController.Move(movement * _movementSpeed * Time.deltaTime);
    }

    private Vector3 FirstPersonMovement()   //Движение от первого лица
    {
        Vector3 movement = _moveInput.x * transform.right + _moveInput.y * transform.forward + Gravity * transform.up;
        return movement;
        
    }

    private Vector3 SideViewMovement()      //Движение с видом сбоку/сверху/везде короче
    {
        // Нормализованное направление движения
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        //moveDirection.Normalize();

        // Плавный поворот в сторону движения
        if (moveDirection != Vector3.zero && !_playerViewManager.FirstPerson)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _playerController.transform.rotation = Quaternion.Slerp(_playerController.transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
        }

        //Vector3 movement = moveDirection + Gravity * transform.up;
        Vector3 movement = _moveInput.x * _playerViewManager.CurrentStaticCamera.transform.right + _moveInput.y * _playerViewManager.CurrentStaticCamera.transform.forward 
            + Gravity * transform.up;   // Управление от трансформа камеры

        return movement;       
    }
}
