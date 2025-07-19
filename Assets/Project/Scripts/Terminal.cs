using System.Text.RegularExpressions;
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
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private string _password;
    [SerializeField] private GameObject _enterImage;

    [Header("Main door settings")]
    [SerializeField] private AutomaticDoor _door;

    [SerializeField] private AudioClip _confirm;
    [SerializeField] private AudioClip _denied;

    private SolvableMuzzle _solvableMuzzle;
    private AudioSource _audioSource;

    private void Awake()
    {
        _solvableMuzzle = GetComponent<SolvableMuzzle>();
        _audioSource = GetComponent<AudioSource>();
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

        _enterImage.SetActive(true);

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

        _enterImage.SetActive(false);

        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }


    public void CheckPassword()
    {
        if (IsPassCorrect())
        {
            _audioSource.PlayOneShot(_confirm);
            _solvableMuzzle.OnPlayerInvoke();
            return;
        }
        else
        {
            _audioSource.PlayOneShot(_denied);
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

    public void SetToUpper()
    {
        string filteredText = Regex.Replace(_passField.text, "[^à-ÿÀ-ß]", "");

        filteredText = filteredText.ToUpper();

        if (filteredText != _passField.text)
            _passField.text = filteredText;
    }
}
