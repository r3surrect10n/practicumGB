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

    [Header("Interaction tip")]
    [SerializeField] private GameObject _interactionTip;

    #region Interaction Properties
    private SolvableMuzzle _currentSolvableMuzzle;
    private IInteractable _currentInteractable;
    private ISolvable _currentSolvable;
    private IResetable _currentResetable;
    private Collider _lastCollider;    
    private Ray _playerLook;
    private RaycastHit _lookHit;
    private float _interactDistance = 2.5f;
    private bool _isInteract = false;
    #endregion

    private PlayerLook _mouseLook;

    private void Awake()
    {
        _mouseLook = GetComponent<PlayerLook>();
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
            StartInteraction();
        }
        else if (_isInteract)
        {
            EndInteraction();
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
        if (_currentSolvable != null)
        {
            _currentSolvable.OnMuzzleSolve();            

            PuzzleExit();
        }
    }

    private void StartInteraction()
    {
        if (_lookHit.collider == null)
            return;

        if (_lookHit.collider.TryGetComponent<IInteractable>(out var interactable))
        {
            _mouseLook.enabled = false;

            ClearHighlight();
            interactable.Interact();
            _currentInteractable = interactable;
            _isInteract = !_isInteract;

            if (_lookHit.collider.TryGetComponent<SolvableMuzzle>(out var solvableMuzzle))
            {
                _currentSolvableMuzzle = solvableMuzzle;
                _currentSolvableMuzzle.IsMuzzleSolved += PuzzleSolved;
            }

            if (_lookHit.collider.TryGetComponent<ISolvable>(out var solvable))
                _currentSolvable = solvable;

            if (_lookHit.collider.TryGetComponent<IResetable>(out var resetable))
                _currentResetable = resetable;

        }
    }

    private void EndInteraction()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.EndInteract();

            if (_currentResetable != null)
                _currentResetable.Reset();

            PuzzleExit();
        }
    }

    private void PuzzleExit()
    {
        _mouseLook.enabled = true;

        ClearHighlight();

        if (_currentSolvableMuzzle != null)
            _currentSolvableMuzzle.IsMuzzleSolved -= PuzzleSolved;

        _currentSolvableMuzzle = null;        
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
        
        ShowInteractionTip();
    }

    private void ClearHighlight()
    {
        if (_lastCollider)
        {
            _lastCollider.gameObject.layer = LayerMask.NameToLayer("Interaction");
            _lastCollider = null;
            
            ShowInteractionTip();
        }
    }

    private void ShowInteractionTip()
    {
        if (!_interactionTip.activeInHierarchy && !_isInteract)        
            _interactionTip.SetActive(true);
        else
            _interactionTip.SetActive(false);
        
    }
}
