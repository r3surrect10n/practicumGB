using UnityEngine;

public class Locker : MonoBehaviour, IMuzzles
{
    [SerializeField] private string _id;

    private Animator _anim;
    private AudioSource _audioSource;

    private bool _isOpen = false;

    public string ID => _id;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OpenLocker()
    {
        SaveSystem.Instance.MarkMuzzleSolved(this);

        _isOpen = true;

        _anim.SetBool("IsOpen", _isOpen);
    }

    public void PlayOpenSound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void Solve()
    {
        _audioSource.mute = true;

        _isOpen = true;

        _anim.SetBool("IsOpen", _isOpen);

        _anim.speed = 1000f;        
    }
}
