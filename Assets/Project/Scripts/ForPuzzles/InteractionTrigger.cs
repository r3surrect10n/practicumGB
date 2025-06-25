using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _playerInteraction;

    [SerializeField] private string _currentInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>())
        {
            Debug.Log("Press E");
            _playerInteraction.CurrentInteractionSelector(_currentInteraction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInteraction>())
        {            
            _playerInteraction.CurrentInteractionSelector("");
        }
    }
}
