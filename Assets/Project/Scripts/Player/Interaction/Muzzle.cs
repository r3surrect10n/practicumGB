using Unity.Cinemachine;
using UnityEngine;

public class Muzzle : MonoBehaviour, IInteractable
{
    [SerializeField] private ViewManager _viewManager;

    [SerializeField] private CinemachineCamera _muzzleCamera;
    [SerializeField] private CinemachineCamera _playerCamera;

    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject[] _interactableObjects;


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
}
