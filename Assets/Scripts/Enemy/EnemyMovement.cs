using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement and rotation speeds")]
    [SerializeField, Range(0, 10)] private float _movementSpeed;    //Скорость передвижения персонажа
    [SerializeField, Range(0, 180)] private float _rotationSmoothness = 10f;    // Коэффициент плавности поворота

    private const float Gravity = -2f;  //Поставил все-такие двоечку, чтобы не ракетило
    private Vector3 _moveInput;
    private CharacterController _playerController;
    private Rigidbody _rb;
    private PlayerViewManager _playerViewManager;

    private void Awake()
    {
        //_rb = GetComponent<Rigidbody>();
        _playerController = GetComponent<CharacterController>();
        _playerViewManager = GetComponent<PlayerViewManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = GameManager.Instance.currentPlayer.currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (_playerViewManager.FirstPerson)
            MoveCharacter(FirstPersonMovement());
        else*/
            MoveCharacter(SideViewMovement());
    }

    public void OnMove(InputAction.CallbackContext callbackContext) //Прием инпута
    {
        _moveInput = callbackContext.ReadValue<Vector2>();
    }
    private void MoveCharacter(Vector3 movement)    //KISS
    {
        _playerController.Move(movement * _movementSpeed * Time.deltaTime);
        //_rb.AddForce(movement * _movementSpeed);
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
        float dx = _moveInput.x;
        //if (_moveInput.y < 0) dx += _moveInput.y;

        // Плавный поворот в сторону движения
        if (moveDirection != Vector3.zero && !_playerViewManager.FirstPerson)
        {
            transform.Rotate(Vector3.up, dx * _rotationSmoothness * Time.deltaTime);
            //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            //_playerController.transform.rotation = Quaternion.Slerp(_playerController.transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
        }

        //Vector3 movement = transform.forward * _moveInput.y + transform.right * _moveInput.x + Gravity * transform.up;
        Vector3 movement = transform.forward * _moveInput.y + Gravity * transform.up;
        //Vector3 movement = moveDirection + Gravity * transform.up;
        return movement;
    }

    public void SavePosition()
    {
        GameManager.Instance.currentPlayer.currentPosition = transform.position;
    }
}
