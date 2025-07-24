using Unity.Cinemachine;
using UnityEngine;

public class ElectricShield : MonoBehaviour, IInteractable, IResetable, ISolvable, IMuzzles
{
    [SerializeField] private string _id;

    [Header("Main camera view manager")]
    [SerializeField] private ViewManager _viewManager;

    [Header("Muzzle and player cameras")]
    [SerializeField] private CinemachineCamera _muzzleCamera;
    [SerializeField] private CinemachineCamera _playerCamera;

    [Header("Muzzle interactables objects")]
    [SerializeField] private GameObject[] _interactableObjects;
    [SerializeField] private ToggleSwitcher[] _toggleSwitchers;

    [Header("Interactable objects")]
    [SerializeField] private AudioSource _pcAudioSource;
    [SerializeField] private GameObject[] _roomObjects;
    [SerializeField] private GameObject[] _pcMonitors;

    [Header("Lights")]
    [SerializeField] private GameObject _volume;
    [SerializeField] private Material _monitorMaterial;
    [SerializeField] private Material _lampMaterial;
    [SerializeField] private Material _paintingLightsMaterial;
    [SerializeField] private GameObject[] _lights;
    [SerializeField] private GameObject[] _paintingLights;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] _toggleSounds;

    private AudioSource _audioSource;
    private Animator _anim;
    private SolvableMuzzle _solvableMuzzle;

    public string ID => _id;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _solvableMuzzle = GetComponent<SolvableMuzzle>();

        _lampMaterial.DisableKeyword("_EMISSION");
        _paintingLightsMaterial.DisableKeyword("_EMISSION");
        _monitorMaterial.DisableKeyword("_EMISSION");
    }

    public void Interact()
    {
        _viewManager.SetView(_playerCamera, _muzzleCamera);

        _anim.SetBool("IsOpen", true);

        gameObject.layer = LayerMask.NameToLayer("Default");

        foreach (var obj in _interactableObjects)
            obj.layer = LayerMask.NameToLayer("Interaction");                
    }

    public void EndInteract()
    {
        _viewManager.SetView(_muzzleCamera, _playerCamera);

        foreach (var obj in _interactableObjects)
        {            
            obj.layer = LayerMask.NameToLayer("Default");
        }

        gameObject.layer = LayerMask.NameToLayer("Interaction");        

        _anim.SetBool("IsOpen", false);
    }

    public void ToggleSwitch(int toggleNum)
    {
        int sound = Random.Range(0, _toggleSounds.Length);

        _audioSource.PlayOneShot(_toggleSounds[sound]);

        _toggleSwitchers[toggleNum].SwitchToggleStatus();

        if (toggleNum == 0)
            _toggleSwitchers[toggleNum + 1].SwitchToggleStatus();
        else if (toggleNum == _toggleSwitchers.Length - 1)
            _toggleSwitchers[toggleNum - 1].SwitchToggleStatus();
        else
        {
            _toggleSwitchers[toggleNum - 1].SwitchToggleStatus();
            _toggleSwitchers[toggleNum + 1].SwitchToggleStatus();
        }

        CheckElectricity();
    }

    public void PlayElectricShieldSound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void Reset()
    {
        foreach (var toggle in _toggleSwitchers)
            toggle.ResetToggle();
    }

    public void OnMuzzleSolve()
    {
        SaveSystem.Instance.MarkMuzzleSolved(this);

        EndInteract();
        gameObject.layer = LayerMask.NameToLayer("Default");
        enabled = false;

        foreach (var rObject in _roomObjects)
            rObject.layer = LayerMask.NameToLayer("Interaction");

        SaveSystem.Instance.SaveGame();
    }

    private void CheckElectricity()
    {
        bool allToggles = true;

        foreach (var toggle in _toggleSwitchers)
        {
            if (!toggle.Condition)
            {
                allToggles = false;
                break;
            }
        }

        if (allToggles)
        {
            foreach (var light in _lights)
                light.SetActive(allToggles);            
            
            _lampMaterial.EnableKeyword("_EMISSION");
            _paintingLightsMaterial.EnableKeyword("_EMISSION");
            _monitorMaterial.EnableKeyword("_EMISSION");

            foreach (var light in _paintingLights)
                light.SetActive(allToggles);

            _pcAudioSource.Play();

            foreach (var monitor in _pcMonitors)
                monitor.SetActive(allToggles);

            //_volume.SetActive(!allToggles);

            _solvableMuzzle.OnPlayerInvoke();
        }
    }

    public bool IsActive()
    {
        return true;
    }

    public void Solve()
    {
        foreach (var light in _lights)
            light.SetActive(true);

        _lampMaterial.EnableKeyword("_EMISSION");
        _paintingLightsMaterial.EnableKeyword("_EMISSION");
        _monitorMaterial.EnableKeyword("_EMISSION");

        foreach (var light in _paintingLights)
            light.SetActive(true);

        foreach (var monitor in _pcMonitors)
            monitor.SetActive(true);

        gameObject.layer = LayerMask.NameToLayer("Default");
        enabled = false;

        foreach (var rObject in _roomObjects)
            rObject.layer = LayerMask.NameToLayer("Interaction");
        
    }    
}
