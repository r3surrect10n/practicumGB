using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Building");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
