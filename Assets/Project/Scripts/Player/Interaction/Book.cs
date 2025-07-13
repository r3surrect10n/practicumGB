using UnityEngine;

public class Book : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource _canvasAudioSource;

    [SerializeField] private AudioClip _bookTakeClip;
    [SerializeField] private AudioClip _bookPickClip;
    
    [SerializeField] private GameObject _book;
    [SerializeField] private GameObject _bookTip;

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
    }    
}
