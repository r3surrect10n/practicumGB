using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour, ITouchable
{
    [SerializeField] private RadioMuzzle _radio;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private GameObject _takingTip;
    [SerializeField] private Text _tipText;
    [SerializeField] private string _tipPhrase;
    [SerializeField] private float _tipTime;

    public void OnTouch()
    {
        _radio.BatteriesIsGetted();

        StartCoroutine(TellTime());
    }

    private IEnumerator TellTime()
    {
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
