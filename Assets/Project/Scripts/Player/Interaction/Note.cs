using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource _canvasAudioSource;

    [SerializeField] private AudioClip _noteTakeClip;
    [SerializeField] private AudioClip _notePickClip;
    
    [SerializeField] private GameObject _note;
    [SerializeField] private GameObject _noteTip;

    [SerializeField] private GameObject _noteButton;

    public void Interact()
    {
        _note.SetActive(true);   
        _noteTip.SetActive(true);
        _canvasAudioSource.PlayOneShot(_noteTakeClip);
    }

    public void EndInteract()
    {
        _note.SetActive(false);
        _noteTip.SetActive(false);
        _canvasAudioSource.PlayOneShot(_notePickClip);
        _noteButton.SetActive(true);
        gameObject.SetActive(false);
    }    
}
