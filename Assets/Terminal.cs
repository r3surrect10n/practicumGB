using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour, IInteractable, ISolvable
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

    [Header("Main door settings")]
    [SerializeField] private AutomaticDoor _door;

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
            _passField.text = "";
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
        _terminalScreen.material = _greenMaterial;
        _door.SetDoorStatusOpen();

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
