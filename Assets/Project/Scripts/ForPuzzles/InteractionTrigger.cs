using Unity.Cinemachine;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _playerInteraction;

    [SerializeField] private CinemachineCamera _interactionCamera;
    [SerializeField] private CinemachineCamera _currentCamera;

    [SerializeField] private string _currentInteraction;

    private bool _canInteract = false;

    public bool CanInteract => _canInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>())
        {
            Debug.Log("Press E");
            _canInteract = true;
            _playerInteraction.CurrentInteractionSelector(_currentInteraction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>())
        {
            _canInteract = false;
            _playerInteraction.CurrentInteractionSelector("");
        }
    }


}
