
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{    
    [Header ("Player camera")]
    [SerializeField] private Camera _playerCamera;

    [Header ("Layer for interactables")]
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private LayerMask _highlightLayer;

    private IInteractable _currentInteractable;
    private Collider _lastCollider;    
    private Ray _playerLook;
    private RaycastHit _lookHit;
    private float _interactDistance = 1.5f;

    private bool _isInteract = false;


    [SerializeField] private ViewManager _viewManager;
    private void Awake()
    { 
        /*                                  */
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;        
    }

    private void Update()
    {
        PlayerLook();
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
            return;

        if (!_isInteract)
        {
            if (_lookHit.collider == null)
                return;

            if (_lookHit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                ClearHighlight();
                interactable.Interact();
                _currentInteractable = interactable;
                _isInteract = !_isInteract;

            }
        }
        else if (_isInteract)
        {
            if (_currentInteractable != null)
            {
                ClearHighlight();
                _currentInteractable.EndInteract();
                _currentInteractable = null;
                _isInteract = !_isInteract;
            }
        }
        else
            return;
    }
    
    public void OnInteractionClick(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
            return;

        if (_lookHit.collider == null)
            return;

        if (_lookHit.collider.TryGetComponent<IClickable>(out var clickable))
        {
            Debug.Log(_lookHit.collider.gameObject.name);
            clickable.SetValue();
        }
        else
            return;

    }

    public void OnExit(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
            return;
    }

    private void PlayerLook()
    {
        if (_viewManager.IsFirstPerson)
            _playerLook = _playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        else
            _playerLook = _playerCamera.ScreenPointToRay(Input.mousePosition);
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
