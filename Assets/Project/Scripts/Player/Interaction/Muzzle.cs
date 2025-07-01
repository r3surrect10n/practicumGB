using Unity.Cinemachine;
using UnityEngine;

public class Muzzle : MonoBehaviour, IInteractable
{
    [SerializeField] private ViewManager _viewManager;

    [SerializeField] private CinemachineCamera _muzzleCamera;
    [SerializeField] private CinemachineCamera _playerCamera;


    public void Interact()
    {
        _viewManager.SetView(_playerCamera, _muzzleCamera);
    }

    public void EndInteract()
    {
        _viewManager.SetView(_muzzleCamera, _playerCamera);
    }
}
