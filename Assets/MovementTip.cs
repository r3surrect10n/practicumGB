using System.Collections;
using UnityEngine;

public class MovementTip : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerLook _playerLook;
    [SerializeField] private PlayerInteraction _playerInteraction;

    [SerializeField] private GameObject _movementTip;
    [SerializeField] private GameObject _hereTip;

    [SerializeField] private float _tipDuration;

    private Coroutine _tipCoroutine;

    private bool _isWalked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            _playerInteraction.enabled = true;
            _playerLook.enabled = true;
            _playerLook.enabled = true;           
        }

        if (other.GetComponent<PlayerMovement>() && !_isWalked)
            _tipCoroutine = StartCoroutine(TipTime());
    }

    private IEnumerator TipTime()
    {
        _isWalked = true;

        _movementTip.SetActive(true);        

        yield return new WaitForSeconds(_tipDuration);


        _movementTip.SetActive(false);

        _hereTip.SetActive(true);

        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
            _hereTip.SetActive(true);
    }
}
