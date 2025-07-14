using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class TabletInteraction : MonoBehaviour, IInteractable, ISolvable
{
    [Header("Main camera view manager")]
    [SerializeField] private ViewManager _viewManager;

    [Header("Muzzle and player cameras")]
    [SerializeField] private CinemachineCamera _muzzleCamera;
    [SerializeField] private CinemachineCamera _playerCamera;

    [Header("Muzzle interactables objects")]
    [SerializeField] private GameObject[] _interactableObjects;

    [Header("Tablet settings")]
    [SerializeField] private InputField _passField;
    [SerializeField] private string _password;

    [Header("Tablet wing settings")]
    [SerializeField] private Renderer _wing;
    [SerializeField] private Material _wingEnabledMaterial;

    [Header("Main door settings")]
    [SerializeField] private MainDoor _mainDoor;

    private SolvableMuzzle _solvableMuzzle;

    private void Awake()
    {
        _solvableMuzzle = GetComponent<SolvableMuzzle>();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckPassword();
        }
    }

    public void Interact()
    {
        _viewManager.SetView(_playerCamera, _muzzleCamera);

        _passField.ActivateInputField();

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void EndInteract()
    {
        _viewManager.SetView(_muzzleCamera, _playerCamera);

        if (!IsPassCorrect())
        {
            CheckPassword();
        }

        _passField.DeactivateInputField();

        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }


    public void CheckPassword()
    {
        if (IsPassCorrect())
        {
            _passField.enabled = false;
            _solvableMuzzle.OnPlayerInvoke();
        }
        else
        {
            _passField.text = "";
            _passField.ActivateInputField();
        }
    }    

    public void OnMuzzleSolve()
    {
        _wing.material = _wingEnabledMaterial;
        _mainDoor.CheckPasswords();

        EndInteract();
        gameObject.layer = LayerMask.NameToLayer("Default");
        enabled = false;
    }

    public bool IsPassCorrect()
    {
        if (_passField.text.ToUpper().Trim() == _password)
            return true;
        else
            return false;
    }
}
