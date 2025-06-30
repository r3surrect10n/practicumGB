using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private TogglePuzzle _togglePuzzle;

    private PlayerViewManager _playerViewManager;
    private string _currentInteraction;

    private Ray _playerLook;
    private RaycastHit _lookHit;
    private int _interactDistance = 5;

    private Cursor _cursor;

    private void Awake()
    {
        _playerViewManager = GetComponent<PlayerViewManager>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _playerLook = _playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(_playerLook.origin, _playerLook.direction * _interactDistance, Color.red);
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
            return;


    }

    public void OnScreenClick(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
            return;

        if (_playerViewManager.FirstPerson)
        {
            Ray playerTouch = _playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(playerTouch, out RaycastHit hit))
            {
                switch (_currentInteraction)
                {
                    case "TogglePuzzle":
                        TogglePuzzle(playerTouch, hit);
                        break;

                    case "SafePuzzle":
                        return;

                    default:
                        break;
                }
            }
            
        }
        else
            return;
    }

    public void CurrentInteractionSelector(string currentInteraction)
    {
        _currentInteraction = currentInteraction;
    }

    private void TogglePuzzle(Ray playerTouch, RaycastHit hit)
    {
        if (hit.collider.GetComponent<Toggle>())
        {
            _togglePuzzle.ToggleSwitchPuzzle(hit.collider.GetComponent<Toggle>().ToggleNumber);
        }
    }
}
