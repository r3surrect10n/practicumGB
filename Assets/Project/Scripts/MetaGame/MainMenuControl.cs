using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        //string scene = "HousePlayersScene";
        string scene = "BuildingOld";
        if (GameManager.Instance.currentPlayer.currentLocation != "")
        {
            scene = GameManager.Instance.currentPlayer.currentLocation;
        }
        SceneManager.LoadScene(scene);
    }
}
