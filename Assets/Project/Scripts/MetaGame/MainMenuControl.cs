using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private OnSceneExit _sceneExit;

    [SerializeField] private Button _continueButton;

    private string _savePath => Path.Combine(Application.persistentDataPath, "autosave.json");

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            if (File.Exists(_savePath))
                _continueButton.interactable = true;
            else
                _continueButton.interactable = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 1f;
        Application.targetFrameRate = 60;
    }

    public void NewGame()
    {    
        if (File.Exists(_savePath))
            File.Delete(_savePath);

        _sceneExit.OnSceneEnd("Comics");
    }

    public void ContinueGame()
    {
        if (File.Exists(_savePath))
            _sceneExit.OnSceneEnd("Building");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }    
}
