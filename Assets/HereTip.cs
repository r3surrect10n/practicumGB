using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HereTip : MonoBehaviour
{
    [SerializeField] private GameObject _tellWindow;
    [SerializeField] private Text _tellText;

    [SerializeField] private float _tellDuration;
    [SerializeField] private string _tellPhrase;

    private Coroutine _tellCoroutine;

    private bool _isWalked = false;

    public void OnTouch()
    {
        if (_tellCoroutine == null)
            _tellCoroutine = StartCoroutine(TellTime());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() && !_isWalked)
            _tellCoroutine = StartCoroutine(TellTime());

    }

    private IEnumerator TellTime()
    {
        _isWalked = true;

        _tellText.text = _tellPhrase;        

        _tellWindow.SetActive(true);
        
        yield return new WaitForSeconds(_tellDuration);       

        _tellWindow.SetActive(false);

        Destroy(gameObject);
    }
}
