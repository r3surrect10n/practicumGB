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
            _playerViewManager.SetNextStaticCamera(_nextCamera);
            _previousRoomEnter.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
