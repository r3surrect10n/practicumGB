using System.Collections;
using UnityEngine;

public class MovementTip : MonoBehaviour, IClearable
{
    [SerializeField] private string _id;

    [SerializeField] private GameObject _movementTip;
    [SerializeField] private GameObject _sprintTip;
    [SerializeField] private GameObject _hereTip;

    [SerializeField] private float _tipDuration;

    private Coroutine _tipCoroutine;

    private bool _isWalked = false;

    public string ID => _id;

    public void Clear()
    {
        _movementTip.SetActive(false);
        _sprintTip.SetActive(false);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() && !_isWalked)
            _tipCoroutine = StartCoroutine(TipTime());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
            SaveSystem.Instance.MarkClearable(this);        
    }

    private IEnumerator TipTime()
    {
        _isWalked = true;

        yield return new WaitForSeconds(1f);

        _movementTip.SetActive(true);        

        yield return new WaitForSeconds(_tipDuration);        

        _sprintTip.SetActive(true);

        yield return new WaitForSeconds(_tipDuration);

        Clear();
    }    
}
