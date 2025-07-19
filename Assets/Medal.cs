using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Medal : MonoBehaviour, ITouchable
{
    [SerializeField] private GameObject _medal;
    [SerializeField] private GameObject _endGameScreen;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _medalionClip;
    [SerializeField] private Animator _animator;

    public void OnTouch()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");

        StartCoroutine(SetMedal());
    }

    private IEnumerator SetMedal()
    {
        _audioSource.PlayOneShot(_medalionClip);
        _medal.SetActive(true);

        yield return new WaitForSeconds(1f);

        _animator.SetBool("IsOpen", true);

        //_endGameScreen.SetActive(true);

        //yield return new WaitForSeconds(5f);

        //SceneManager.LoadScene("MainMenuScene");

        StopCoroutine(SetMedal());
    }

    private void SetMedalS()
    {
        _audioSource.PlayOneShot(_medalionClip);
        _animator.SetBool("IsOpen", true);
        _medal.SetActive(true);
    }
}
