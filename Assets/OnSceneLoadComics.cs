using System.Collections;
using UnityEngine;

public class OnSceneLoadComics : MonoBehaviour
{    
    [SerializeField] private GameObject _fadePanel;
    [SerializeField] private CanvasGroup _canvasGr;
    [SerializeField] private float _fadeDuration = 1f;

    [SerializeField] private GameObject _mouse;
    [SerializeField] private GameObject _comicsClicker;

    [SerializeField] private AudioListener _listener;

    private void Awake()
    {      
        _fadePanel.SetActive(true);        
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        _canvasGr.alpha = 1f;
        _fadePanel.SetActive(true);
        
        StartCoroutine(SceneStartRoutine());
    }

    private IEnumerator SceneStartRoutine()
    {
        _listener.enabled = false;
        yield return new WaitForSeconds(5f);
        _listener.enabled = true;

        float timer = 0f;
        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            _canvasGr.alpha = 1f - (timer / _fadeDuration);
            yield return null;
        }
        _canvasGr.alpha = 0f; 

        _fadePanel.SetActive(false);

        yield return new WaitForSeconds(1f);

        _mouse.SetActive(true);
        _comicsClicker.SetActive(true);
    }    
}
