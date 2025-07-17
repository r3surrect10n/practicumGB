using UnityEngine;

public class PaintingTouching : MonoBehaviour, ITouchable
{
    private Animator _anim;
    private AudioSource _source;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    public void OnTouch()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        _anim.SetBool("IsTouched", true);
    }

    public void PlaySound(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
}
