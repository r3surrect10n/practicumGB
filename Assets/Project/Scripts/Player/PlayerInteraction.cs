using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{    
    [Header ("Player camera")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private ViewManager _viewManager;    

    [Header ("Layer for interactables")]
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private LayerMask _highlightLayer;

    #region Interaction Properties
    private IInteractable _currentInteractable;
    private IResetable _currentResetable;
    private Muzzle _currentMuzzle;
    private Collider _lastCollider;    
    private Ray _playerLook;
    private RaycastHit _lookHit;
    private float _interactDistance = 1.5f;
    private bool _isInteract = false;
    #endregion

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

                if (_lookHit.collider.TryGetComponent<Muzzle>(out var muzzle))
                {
                    _currentMuzzle = muzzle;
                    _currentMuzzle.IsMuzzleSolved += PuzzleSolved;
                }

                if (_lookHit.collider.TryGetComponent<IResetable>(out var resetable))
                    _currentResetable = resetable;
            }
        }
        else if (_isInteract)
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.EndInteract();
                _currentResetable.Reset();

                PuzzleExit();
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
            clickable.SetValue();

        else
            return;

    }    

    public void PuzzleSolved()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.OnMuzzleSolve();            

            PuzzleExit();
        }
    }

    private void PuzzleExit()
    {
        ClearHighlight();

        _currentMuzzle.IsMuzzleSolved -= PuzzleSolved;

        _currentMuzzle = null;        
        _currentResetable = null;
        _currentInteractable = null;

        _isInteract = !_isInteract;
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
