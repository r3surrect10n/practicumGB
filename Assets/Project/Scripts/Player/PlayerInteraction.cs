using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private TogglePuzzle _togglePuzzle;

    private PlayerViewManager _playerViewManager;
    private string _currentInteraction;

    private void Awake()
    {
        _playerViewManager = GetComponent<PlayerViewManager>();
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
