using Unity.Cinemachine;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private CinemachineCamera _interactView;

    private PlayerViewManager _playerViewManager;

    public void Interact()
    {
        SetView();
    } 
    
    public void ExitInteract()
    {
        SetView();
    }

    private void SetView()
    {
        _playerViewManager.Set(_interactView);
    }
}
