using UnityEngine;

public class Locker : MonoBehaviour
{
    private Animator _anim;
    private AudioSource _audioSource;

    private bool _isOpen = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OpenLocker()
    {
        _isOpen = true;

        _anim.SetBool("IsOpen", _isOpen);
    }

    public void PlayOpenSound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
