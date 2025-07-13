using Unity.Cinemachine;
using UnityEngine;

public class ElectricShield : MonoBehaviour, IInteractable, IResetable
{
    [Header("Main camera view manager")]
    [SerializeField] private ViewManager _viewManager;

    [Header("Muzzle and player cameras")]
    [SerializeField] private CinemachineCamera _muzzleCamera;
    [SerializeField] private CinemachineCamera _playerCamera;

    [Header("Muzzle interactables objects")]
    [SerializeField] private GameObject[] _interactableObjects;
    [SerializeField] private ToggleSwitcher[] _toggleSwitchers;

    [SerializeField] private Animator _anim;

    [SerializeField] private Material _material;
    [SerializeField] private GameObject _light;

    private bool _isSolved = false;

    public void Interact()
    {
        _viewManager.SetView(_playerCamera, _muzzleCamera);

        _anim.SetBool("IsOpen", true);

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

        if (!_isSolved)
        {
            foreach (var toggle in _toggleSwitchers)
                toggle.ResetToggle();
        }
        else
        {
            Destroy(this);
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        _anim.SetBool("IsOpen", false);
    }

    public void ToggleSwitch(int toggleNum)
    {
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
            _light.SetActive(allToggles);
            _material.EnableKeyword("_EMISSION");
            _isSolved = true;
        }
    }

    public void Reset()
    {
        
    }
}
