using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInteraction : MonoBehaviour
{
    public static Action PauseGame;

    [Header ("Player camera")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private ViewManager _viewManager;    

    [Header ("Layers for interactables")]    
    [SerializeField] private LayerMask _raycastBlockingLayers;

    [Header("Interaction tip")]
    [SerializeField] private GameObject _interactionTip;

    #region Interaction Properties
    private SolvableMuzzle _currentSolvableMuzzle;
    private IInteractable _currentInteractable;
    private ISolvable _currentSolvable;
    private IResetable _currentResetable;
    private IReadable _currentReadable;
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

        if (_lookHit.collider == null)
            return;

        if (_lookHit.collider.TryGetComponent<ITouchable>(out var touchable))
        {
            ClearHighlight();
            touchable.OnTouch();
            return;
        }        

        if (_lookHit.collider.TryGetComponent<IInteractable>(out var interactable))
        {
            StartInteraction(interactable);
        }        
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

    public void OnEndInteract(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase != InputActionPhase.Started)
            return;

        if (_isInteract)
        {
            EndInteraction();
            return;
        }
        else
            PauseGame?.Invoke();
    }

    public void PuzzleSolved()
    {        
        if (_currentSolvable != null)
        {            
            ClearHighlight();

            _currentSolvable.OnMuzzleSolve();            

            PuzzleExit();
        }
    }

    public void ClearPreviousHighlight()
    {
        ClearHighlight();
    }

    private void StartInteraction(IInteractable interactable)
    {
        _mouseLook.enabled = false;

        ClearHighlight();
        interactable.Interact();
        _currentInteractable = interactable;
        Debug.Log(_currentInteractable);
        _isInteract = !_isInteract;

        if (_lookHit.collider.TryGetComponent<SolvableMuzzle>(out var solvableMuzzle))
        {            
            _currentSolvableMuzzle = solvableMuzzle;
            _currentSolvableMuzzle.IsMuzzleSolved += PuzzleSolved;
        }

        if (_lookHit.collider.TryGetComponent<ISolvable>(out var solvable))
        {
            _currentSolvable = solvable;            
        }

        if (_lookHit.collider.TryGetComponent<IResetable>(out var resetable))
            _currentResetable = resetable;
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

        if (Physics.Raycast(_playerLook, out _lookHit, _interactDistance, _raycastBlockingLayers))
        {
            if (_lookHit.collider.gameObject.layer != LayerMask.NameToLayer("BlockRaycast"))
                TryHighlight(_lookHit.collider);
            else
                ClearHighlight();
        }
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
        
        if (interactor.TryGetComponent<IReadable>(out var readable))        
            _currentReadable = readable;

        ShowInteractionTip();
    }

    private void ClearHighlight()
    {
        if (_lastCollider)
        {
            _lastCollider.gameObject.layer = LayerMask.NameToLayer("Interaction");
            _lastCollider = null;
            
            if (_currentReadable != null)
            {
                _currentReadable.HideName();
                _currentReadable = null;
            }

            ShowInteractionTip();
        }
    }

    private void ShowInteractionTip()
    {
        if (_currentReadable != null)
            _currentReadable.ShowName();        

        if (!_interactionTip.activeInHierarchy && !_isInteract)
            _interactionTip.SetActive(true);
        else
            _interactionTip.SetActive(false);
    }
}
