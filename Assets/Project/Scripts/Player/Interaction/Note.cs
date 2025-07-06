using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource _canvasAudioSource;

    [SerializeField] private AudioClip _noteTakeClip;
    [SerializeField] private AudioClip _notePickClip;
    
    [SerializeField] private GameObject _note;

    public void Interact()
    {
        _note.SetActive(true);        
        _canvasAudioSource.PlayOneShot(_noteTakeClip);
    }

    public void EndInteract()
    {
        _note.SetActive(false);
        _canvasAudioSource.PlayOneShot(_notePickClip);
        gameObject.SetActive(false);
    }

    public void OnMuzzleSolve()
    {
        throw new System.NotImplementedException();
    }
}
