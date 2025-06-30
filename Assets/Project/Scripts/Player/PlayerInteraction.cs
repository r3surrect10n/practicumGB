using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private TogglePuzzle _togglePuzzle;

    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private LayerMask _highlightLayer;

    private PlayerInput playerInput;

    private Ray _playerLook;
    private RaycastHit _lookHit;
    private float _interactDistance = 1.5f;
    private Collider _lastCollider;    

    private void Awake()
    { 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        PlayerLook();
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
            return;

        if (_lookHit.collider == null)
            return;

        IInteractable interactable = _lookHit.collider.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;        
        playerInput.enabled = false;
        interactable.Interact();
    }

    //public void Interact()
    //{
    //    IInteractable interactable = _lookHit.collider.GetComponent<IInteractable>();

    //    if (interactable == null)
    //        return;

    //    interactable.Interact();
    //    Time.timeScale = 0;
    //}

    public void OnExit(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
            return;
    }

    private void PlayerLook()
    {
        _playerLook = _playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(_playerLook.origin, _playerLook.direction * _interactDistance, Color.red);

        if (Physics.Raycast(_playerLook, out _lookHit, _interactDistance, _interactionLayer | _highlightLayer))
            TryHighlight(_lookHit.collider);
        else
            ClearHighlight();
    }

    private void TryHighlight(Collider interactor)
    {
        if (_lastCollider == interactor)
            return;

        ClearHighlight();
        interactor.gameObject.layer = LayerMask.NameToLayer("Outline");
        _lastCollider = interactor;        
    }

    private void ClearHighlight()
    {
        if (_lastCollider)
        {
            _lastCollider.gameObject.layer = LayerMask.NameToLayer("Interaction");
            _lastCollider = null;
        }
    }  
}
