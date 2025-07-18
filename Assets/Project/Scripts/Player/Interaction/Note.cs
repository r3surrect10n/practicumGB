using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource _canvasAudioSource;

    [SerializeField] private AudioClip _noteTakeClip;
    [SerializeField] private AudioClip _notePickClip;

    [SerializeField] private GameObject _note;
    [SerializeField] private GameObject _noteTip;

    [SerializeField] private GameObject _noteButton;

    [SerializeField] private GameObject _takingTip;
    [SerializeField] private Text _tipText;
    [SerializeField] private string _tipPhrase;
    [SerializeField] private float _tipTime;

    [SerializeField] private GameObject _takingTell;
    [SerializeField] private Text _tellText;
    [SerializeField] private string _tellPhrase;
    [SerializeField] private float _tellTime;

    [SerializeField] private bool _isTellable;

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
        
         StartCoroutine(TipTime());        
    }    

    private IEnumerator TipTime()
    {
        _tipText.text = _tipPhrase;
        _takingTip.SetActive(true);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;


        yield return new WaitForSeconds(_tipTime);

        _takingTip.SetActive(false);

        if (_isTellable)
        {
            _tellText.text = _tellPhrase;
            _takingTell.SetActive(true);

            yield return new WaitForSeconds(_tellTime);

            _takingTell.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
