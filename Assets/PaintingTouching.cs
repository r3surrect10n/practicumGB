using UnityEngine;

public class PaintingTouching : MonoBehaviour, ITouchable, IMuzzles
{
    [SerializeField] private string _id;

    private Animator _anim;
    private AudioSource _source;

    public string ID => _id;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
    }

    public void OnTouch()
    {
        SaveSystem.Instance.MarkMuzzleSolved(this);

        gameObject.layer = LayerMask.NameToLayer("Default");
        enabled = false;

        _anim.SetBool("IsTouched", true);

        SaveSystem.Instance.SaveGame();
    }

    public void PlaySound(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }

    public void Solve()
    {
        _source.mute = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
        enabled = false;

        _anim.SetBool("IsTouched", true);
        _anim.speed = 1000f;
    }
}
