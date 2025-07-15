using UnityEngine;

public class SwitchStations : MonoBehaviour, IClickable
{
    [SerializeField] private Radio _radio;

    [SerializeField] private int _buttonIndex;

    [SerializeField] private AudioClip[] _buttonClips;

    private AudioSource _audioSource;
    private Animator _anim;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
    }

    public void SetValue()
    {
        _anim.SetTrigger("IsPush");
        
        int buttonClip = Random.Range(0, _buttonClips.Length);

        _audioSource.PlayOneShot(_buttonClips[buttonClip]);

        _radio.SwitchStations(_buttonIndex);
    }
}
