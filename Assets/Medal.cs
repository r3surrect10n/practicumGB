using System.Collections;
using UnityEngine;

public class Medal : MonoBehaviour, ITouchable, IMuzzles
{
    [SerializeField] private string _id;

    [SerializeField] private GameObject _medal;
    [SerializeField] private GameObject _endGameScreen;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _medalionClip;
    [SerializeField] private Animator _animator;

    public string ID => _id;

    public void OnTouch()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");

        StartCoroutine(SetMedal());
    }

    public void Solve()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        _medal.SetActive(true);
        _animator.SetBool("IsOpen", true);
        _animator.Update(10f);
    }

    private IEnumerator SetMedal()
    {
        SaveSystem.Instance.MarkMuzzleSolved(this);

        _audioSource.PlayOneShot(_medalionClip);
        _medal.SetActive(true);

        yield return new WaitForSeconds(1f);

        _animator.SetBool("IsOpen", true);        

        StopCoroutine(SetMedal());
    }

    private void SetMedalS()
    {
        _audioSource.PlayOneShot(_medalionClip);
        _medal.SetActive(true);
        _animator.SetBool("IsOpen", true);

        SaveSystem.Instance.MarkMuzzleSolved(this);
    }
}
