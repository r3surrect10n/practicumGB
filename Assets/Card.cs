using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, ITouchable, INotes
{
    [SerializeField] private string _id;

    [SerializeField] private CardReaderPanel _cardPanel;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private GameObject _takingTip;
    [SerializeField] private Text _tipText;
    [SerializeField] private string _tipPhrase;
    [SerializeField] private float _tipTime;
    [SerializeField] private GameObject _cardNotebook;

    public string ID => _id;

    public void ClearNote()
    {
        _cardPanel.CardGetted();

        _cardNotebook.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnTouch()
    {
        _cardPanel.CardGetted();        

        StartCoroutine(TellTime());
    }

    private IEnumerator TellTime()
    {
        SaveSystem.Instance.MarkNoteReaded(this);

        _cardNotebook.SetActive(true);

        _tipText.text = _tipPhrase;
        _takingTip.SetActive(true);
        _audioSource.PlayOneShot(_audioClip);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;


        yield return new WaitForSeconds(_tipTime);

        _takingTip.SetActive(false);
        gameObject.SetActive(false);
    }
}
