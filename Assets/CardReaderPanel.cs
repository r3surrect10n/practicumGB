using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardReaderPanel : MonoBehaviour, ITouchable
{
    [SerializeField] private GameObject _tellWindow;
    [SerializeField] private string _tellPhrase;
    [SerializeField] private Text _tellText;
    [SerializeField] private float _tellDuration;
    
    [SerializeField] private AutomaticDoor _automaticDoor;
    [SerializeField] private GameObject _accessScreen;
    [SerializeField] private AudioClip _accessAudioClip;

    private AudioSource _source;
    private Coroutine _coroutine;

    private bool _isClosed = true;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void OnTouch()
    {
        if (_isClosed && _coroutine == null)
            _coroutine = StartCoroutine(TellTime());
        else if (!_isClosed)
        {
            OpenDoor();
        }
    }

    public void CardGetted()
    {
        _isClosed = false;
    }

    public void OpenDoor()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");

        _source.PlayOneShot(_accessAudioClip);
        _accessScreen.SetActive(true);
        _automaticDoor.SetDoorStatusOpen();
    }

    private IEnumerator TellTime()
    {        
        _tellText.text = _tellPhrase;        

        _tellWindow.SetActive(true);
       
            yield return new WaitForSeconds(_tellDuration);       

        _tellWindow.SetActive(false);       

        StopCoroutine(_coroutine);

        _coroutine = null;
    }
}
