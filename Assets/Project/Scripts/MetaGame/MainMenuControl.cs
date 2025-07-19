using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;
        Application.targetFrameRate = 60;
    }

    public void PlayGame()
    {
        StartCoroutine(StartGame());

        //SceneManager.LoadScene("Comics");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene("Comics");
    }
}
