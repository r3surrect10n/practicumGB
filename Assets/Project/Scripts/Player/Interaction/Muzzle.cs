using Unity.Cinemachine;
using UnityEngine;

public class Muzzle : MonoBehaviour, IInteractable, ISolvable
{ 
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

    public void OnMuzzleSolve()
    {
        EndInteract();
        SolvedCondition();
    }

    public bool IsActive()
    {
        return true;
    }

    public void SolvedCondition()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        enabled = false;
    }
}
