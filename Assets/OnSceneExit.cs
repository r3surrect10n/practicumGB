using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneExit : MonoBehaviour
{
    [SerializeField] private GameObject _fadePanel;
    [SerializeField] private CanvasGroup _canvasGr;
    [SerializeField] private float _fadeDuration = 1f;

    public void OnSceneEnd(string scene)
    {
        Time.timeScale = 1f;
        _canvasGr.alpha = 0;
        _fadePanel.SetActive(true);
        StartCoroutine(SceneStartRoutine(scene));
    }

    private IEnumerator SceneStartRoutine(string scene)
    {
        float timer = 0f;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            _canvasGr.alpha = Mathf.Clamp01(timer / _fadeDuration);
            yield return null;
        }
        _canvasGr.alpha = 1f;

        SceneManager.LoadScene(scene);        
    }
}
