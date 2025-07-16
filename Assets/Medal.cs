using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Medal : MonoBehaviour, ITouchable
{
    [SerializeField] private GameObject _medal;
    [SerializeField] private GameObject _endGameScreen;

    public void OnTouch()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        _medal.SetActive(true);

        yield return new WaitForSeconds(1f);

        _endGameScreen.SetActive(true);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("MainMenuScene");
    }
}
