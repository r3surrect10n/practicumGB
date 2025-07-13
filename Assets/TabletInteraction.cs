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

    [SerializeField] private InputField _passField;
    [SerializeField] private string _password;

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

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void EndInteract()
    {
        _viewManager.SetView(_muzzleCamera, _playerCamera);        

        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }


    public void CheckPassword()
    {
        if (_passField.text == _password)
        {
            _passField.enabled = false;
            _solvableMuzzle.OnPlayerInvoke();
        }
        else
        {
            _passField.text = "";
        }
    }

    public void OnPlayerInvoke()
    {
        throw new System.NotImplementedException();
    }

    public void OnMuzzleSolve()
    {
        EndInteract();
        gameObject.layer = LayerMask.NameToLayer("Default");
        enabled = false;
    }
}
