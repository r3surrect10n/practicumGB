using System.Text.RegularExpressions;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TabletInteraction : MonoBehaviour, IInteractable, ISolvable
{
    [Header("Main camera view manager")]
    [SerializeField] private ViewManager _viewManager;

    [Header("Muzzle and player cameras")]
    [SerializeField] private CinemachineCamera _muzzleCamera;
    [SerializeField] private CinemachineCamera _playerCamera;    

    [Header("Tablet settings")]
    [SerializeField] private InputField _passField;
    [SerializeField] private GameObject _tip;
    [SerializeField] private string _password;

    [Header("Tablet wing settings")]
    [SerializeField] private Renderer _wing;
    [SerializeField] private Material _wingEnabledMaterial;

    [Header("Main door settings")]
    [SerializeField] private MainDoor _mainDoor;

    [Header("Tablet sounds")]
    [SerializeField] private AudioClip _confirmSound;
    [SerializeField] private AudioClip _deniedSound;

    private AudioSource _audioSource;
    private SolvableMuzzle _solvableMuzzle;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _solvableMuzzle = GetComponent<SolvableMuzzle>();        
    }

    private void Start()
    {
        _passField.enabled = false;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && _passField.enabled)
        {
            CheckPassword();
        }
    }

    public void Interact()
    {
        _viewManager.SetView(_playerCamera, _muzzleCamera);

        _passField.enabled = true;
        _passField.ActivateInputField();

        _tip.SetActive(true);

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

        _tip.SetActive(false);

        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }


    public void CheckPassword()
    {
        if (IsPassCorrect())
        {
            _audioSource.PlayOneShot(_confirmSound);
            _solvableMuzzle.OnPlayerInvoke();
            return;
        }
        else
        {            
            _audioSource.PlayOneShot(_deniedSound);
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
        if (_passField.text == _password)
            return true;
        else
            return false;
    }

    public void SetToUpper()
    {        
        string filteredText = Regex.Replace(_passField.text, "[^�-��-�]", "");
        
        filteredText = filteredText.ToUpper();
        
        if (filteredText != _passField.text)        
            _passField.text = filteredText;        
    }

    public bool IsActive()
    {
        return true;
    }
}
