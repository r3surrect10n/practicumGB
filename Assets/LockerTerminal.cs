using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class LockerTerminal : MonoBehaviour, IInteractable, ISolvable
{
    [Header("Main camera view manager")]
    [SerializeField] private ViewManager _viewManager;

    [Header("Muzzle and player cameras")]
    [SerializeField] private CinemachineCamera _muzzleCamera;
    [SerializeField] private CinemachineCamera _playerCamera;

    [Header("Tablet settings")]
    [SerializeField] private Image _terminalScreen;
    [SerializeField] private InputField _passField;
    [SerializeField] private string _password;
    [SerializeField] private Material _greenMaterial;

    [Header("Locker door settings")]
    [SerializeField] private Locker _locker;

    private SolvableMuzzle _solvableMuzzle;

    private void Awake()
    {
        _solvableMuzzle = GetComponent<SolvableMuzzle>();
    }

    private void Start()
    {
        _passField.enabled = false;
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

        _passField.enabled = true;
        _passField.ActivateInputField();

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void EndInteract()
    {
        _viewManager.SetView(_muzzleCamera, _playerCamera);

        if (!IsPassCorrect())
        {
            _passField.text = "";
        }

        _passField.DeactivateInputField();
        _passField.enabled = false;

        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }


    public void CheckPassword()
    {
        if (IsPassCorrect())        
            _solvableMuzzle.OnPlayerInvoke();
        else
        {
            _passField.text = "";
            _passField.ActivateInputField();
        }
    }

    public void OnMuzzleSolve()
    {
        _terminalScreen.material = _greenMaterial;
        _locker.OpenLocker();

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
