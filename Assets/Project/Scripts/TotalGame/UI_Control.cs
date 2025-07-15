using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
