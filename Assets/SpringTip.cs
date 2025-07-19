using System.Collections;
using UnityEngine;

public class SpringTip : MonoBehaviour
{
    [SerializeField] private GameObject _sprintTip;
    [SerializeField] private float _tipDuration;

    private Coroutine _tipCoroutine;
    private bool _isWalked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() && !_isWalked)
            _tipCoroutine = StartCoroutine(TipTime());
    }

    private IEnumerator TipTime()
    {
        _isWalked = true;

        _sprintTip.SetActive(true);

        yield return new WaitForSeconds(_tipDuration);

        _sprintTip.SetActive(false);        

        Destroy(gameObject);
    }
}
