using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;

    private PlayerViewManager _playerViewManager;
    [SerializeField] private TogglePuzzle _togglePuzzle;

    private void Awake()
    {
        _playerViewManager = GetComponent<PlayerViewManager>();
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
                if (hit.collider.GetComponent<Toggle>())
                {
                    _togglePuzzle.ToggleSwitchPuzzle(hit.collider.GetComponent<Toggle>().ToggleNumber);
                }
            }
        }
        else
            return;
    }
}
