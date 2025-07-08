using Unity.Cinemachine;
using UnityEngine;

public class PaintingsInteractable : MonoBehaviour, IInteractable
{
    [Header("Main camera view manager")]
    [SerializeField] private ViewManager _viewManager;

    [Header("Muzzle and player cameras")]
    [SerializeField] private CinemachineCamera _paintingCamera;
    [SerializeField] private CinemachineCamera _playerCamera;

    [SerializeField] private GameObject _paintingText;

    public void Interact()
    {
        _viewManager.SetView(_playerCamera, _paintingCamera);
        _paintingText.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("Default");       
    }

    public void EndInteract()
    {
        _viewManager.SetView(_paintingCamera, _playerCamera);
        _paintingText.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }    
}
