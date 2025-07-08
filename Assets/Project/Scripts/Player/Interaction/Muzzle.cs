using System;
using Unity.Cinemachine;
using UnityEngine;

public class Muzzle : MonoBehaviour, IInteractable, ISolvable
{
    //public event Action IsMuzzleSolved;

    [Header("Main camera view manager")]
    [SerializeField] private ViewManager _viewManager;    

    [Header("Muzzle and player cameras")]
    [SerializeField] private CinemachineCamera _muzzleCamera;
    [SerializeField] private CinemachineCamera _playerCamera;

    [Header("Muzzle interactables objects")]
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

    public void OnPlayerInvoke()
    {
        throw new NotImplementedException();
    }

    public void OnMuzzleSolve()
    {
        EndInteract();
        gameObject.layer = LayerMask.NameToLayer("Default");
        enabled = false;
    }

    //public void OnPlayerInvoke()
    //{
    //    IsMuzzleSolved?.Invoke();
    //}

    //public void OnMuzzleSolve()
    //{
    //    EndInteract();
    //    gameObject.layer = LayerMask.NameToLayer("Default");
    //    Destroy(this);
    //}

}
