using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class RadioMuzzle : MonoBehaviour, IInteractable, ISolvable
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

    private AudioSource _source;
    private Coroutine _coroutine;

    private bool _isActive = false;


    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (!_isActive && _coroutine == null)
            _coroutine = StartCoroutine(TellTime());
        else if (_isActive)
        {
            Debug.Log("asdasd");

            _viewManager.SetView(_playerCamera, _muzzleCamera);

            gameObject.layer = LayerMask.NameToLayer("Default");

            foreach (var obj in _interactableObjects)
            {
                obj.layer = LayerMask.NameToLayer("Interaction");
            }
        }
        else return;
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
        _isActive = false;
    }

    public void RadioOn()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        
        _radioScreen.SetActive(true);
        
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
}
