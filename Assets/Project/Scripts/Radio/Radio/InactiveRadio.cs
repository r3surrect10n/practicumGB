using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class RadioMuzzle : MonoBehaviour, IInteractable, ISolvable, ITouchable
{
    [Header("Main camera view manager")]
    [SerializeField] private ViewManager _viewManager;

    [Header("Muzzle and player cameras")]
    [SerializeField] private CinemachineCamera _muzzleCamera;
    [SerializeField] private CinemachineCamera _playerCamera;

    [Header("Muzzle interactables objects")]
    [SerializeField] private GameObject[] _interactableObjects;

    [Header("Tell tip settings")]
    [SerializeField] private GameObject _tellWindow;
    [SerializeField] private string _tellPhrase;
    [SerializeField] private Text _tellText;
    [SerializeField] private float _tellDuration;
        
    [SerializeField] private GameObject _radioScreen;

    [SerializeField] private AudioSource _canvasAudioSource;
    [SerializeField] private AudioClip _batteryInSound;
    [SerializeField] private GameObject _batteryNotebook;

    private Radio _radio;
    private AudioSource _source;
    private Coroutine _coroutine;

    private bool _battery = false;
    private bool _isActive = false;


    private void Awake()
    {
        _radio = GetComponent<Radio>();
        _source = GetComponent<AudioSource>();
        _source.enabled = false;
    }

    public void Interact()
    {
        _viewManager.SetView(_playerCamera, _muzzleCamera);

        gameObject.layer = LayerMask.NameToLayer("Default");

        foreach (var obj in _interactableObjects)
        {
            obj.layer = LayerMask.NameToLayer("Interaction");
        }
    }

    public void EndInteract()
    {
        _viewManager.SetView(_muzzleCamera, _playerCamera);

        foreach (var obj in _interactableObjects)
        {
            obj.layer = LayerMask.NameToLayer("Default");
        }

        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }

    public void OnMuzzleSolve()
    {
        EndInteract();
        gameObject.layer = LayerMask.NameToLayer("Default");
        enabled = false;
    }    

    public void BatteriesIsGetted()
    {
        _battery = true;
    }

    public void RadioOn()
    {
        _coroutine = null;

        _coroutine = StartCoroutine(TurnRadioOn());        
    }
    public bool IsActive()
    {
        return _isActive;
    }

    public void OnTouch()
    {
        if (!_isActive)
        {
            if (!_battery && _coroutine == null)
                _coroutine = StartCoroutine(TellTime());
            else if (_battery)
                RadioOn();
        }
        else
            return;
    }

    private IEnumerator TellTime()
    {
        _tellText.text = _tellPhrase;

        _tellWindow.SetActive(true);

        yield return new WaitForSeconds(_tellDuration);

        _tellWindow.SetActive(false);

        StopCoroutine(_coroutine);

        _coroutine = null;
    }

    private IEnumerator TurnRadioOn()
    {
        _batteryNotebook.SetActive(false);

        _canvasAudioSource.PlayOneShot(_batteryInSound);

        yield return new WaitForSeconds(1f);

        _radioScreen.SetActive(true);
        _source.enabled = true;
        _radio.UpdateDisplayAnother();
        _isActive = true;        
    }
}
