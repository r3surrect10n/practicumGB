using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    [SerializeField] private PlayerViewManager _playerViewManager;

    [SerializeField] private CinemachineCamera _nextCamera;
    [SerializeField] private GameObject _previousRoomEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            _playerViewManager.Set(_nextCamera);            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {            
            _previousRoomEnter.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
