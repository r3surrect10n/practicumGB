using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource _canvasAudioSource;

    [SerializeField] private AudioClip _bookTakeClip;
    [SerializeField] private AudioClip _bookPickClip;
    
    [SerializeField] private GameObject _book;
    [SerializeField] private GameObject _bookTip;


    [SerializeField] private GameObject _takingTell;
    [SerializeField] private Text _tellText;
    [SerializeField] private string _tellPhrase;
    [SerializeField] private float _tellTime;

    [SerializeField] private bool _isTellable;

    public void Interact()
    {
        _book.SetActive(true);
        _bookTip.SetActive(true);
        _canvasAudioSource.PlayOneShot(_bookTakeClip);
    }

    public void EndInteract()
    {
        _book.SetActive(false);
        _bookTip.SetActive(false);
        _canvasAudioSource.PlayOneShot(_bookPickClip);

        if (_isTellable)
            StartCoroutine(TellTime());
    }

    private IEnumerator TellTime()
    {
        _tellText.text = _tellPhrase;
        _takingTell.SetActive(true);

        yield return new WaitForSeconds(_tellTime);

        _takingTell.SetActive(false);

        _isTellable = false;

        StopCoroutine(TellTime());
    }

    public bool IsActive()
    {
        return true;
    }
}
