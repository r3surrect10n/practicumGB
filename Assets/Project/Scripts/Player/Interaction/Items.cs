using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour, ITouchable
{
    [SerializeField] private GameObject _tellWindow;

    [SerializeField] private string _tellPhrase;
    [SerializeField] private string _tellPhraseAfterTouch;

    [SerializeField] private Text _tellText;

    [SerializeField] private float _tellDuration;
    [SerializeField] private float _tellDurationAfterTouch;

    private Coroutine _tellCoroutine;

    private bool _isTouching = false;    

    public void OnTouch()
    {
        if (_tellCoroutine == null)
            _tellCoroutine = StartCoroutine(TellTime());
    }

    private IEnumerator TellTime()
    {
        if (!_isTouching)
            _tellText.text = _tellPhrase;
        else
            _tellText.text = _tellPhraseAfterTouch;

        _tellWindow.SetActive(true);

        if (!_isTouching)
            yield return new WaitForSeconds(_tellDuration);
        else
            yield return new WaitForSeconds(_tellDurationAfterTouch);

        _tellWindow.SetActive(false);

        _isTouching = true;

        StopCoroutine(_tellCoroutine);

        _tellCoroutine = null;
    }
}
