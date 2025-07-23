using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TellableNote : MonoBehaviour, IInteractable, INotes
{
    [SerializeField] private string _id;

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

    public string ID => _id;

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

        SaveSystem.Instance.MarkNoteReaded(this);

        StartCoroutine(TipTime());
        StartCoroutine(TellTime());
    }

    private IEnumerator TipTime()
    {
        _tipText.text = _tipPhrase;
        _takingTip.SetActive(true);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(_tipTime);

        _takingTip.SetActive(false);       
    }

    private IEnumerator TellTime()
    {
        _tellText.text = _tellPhrase;
        _takingTell.SetActive(true);

        yield return new WaitForSeconds(_tellTime);

        _takingTell.SetActive(false);

        gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return true;
    }

    public void ClearNote()
    {
        _noteButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
