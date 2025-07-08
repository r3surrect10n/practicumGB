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
        //string scene = "HousePlayersScene";
        string scene = "Building";

        if (GameManager.Instance.currentPlayer.currentLocation != "")
        {
            scene = GameManager.Instance.currentPlayer.currentLocation;
        }

        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
