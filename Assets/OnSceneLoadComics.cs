using System.Collections;
using UnityEngine;

public class OnSceneLoadComics : MonoBehaviour
{    
    [SerializeField] private GameObject _fadePanel;
    [SerializeField] private CanvasGroup _canvasGr;
    [SerializeField] private float _fadeDuration = 1f;

    [SerializeField] private GameObject _mouse;
    [SerializeField] private GameObject _comicsClicker;

    private void Awake()
    {      
        _fadePanel.SetActive(true);        
    }

    private void Start()
    { 
        _canvasGr.alpha = 1f;
        _fadePanel.SetActive(true);
        
        StartCoroutine(SceneStartRoutine());
    }

    private IEnumerator SceneStartRoutine()
    {        
        float timer = 0f;
        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            _canvasGr.alpha = 1f - (timer / _fadeDuration);
            yield return null;
        }
        _canvasGr.alpha = 0f; 

        _fadePanel.SetActive(false);

        _mouse.SetActive(true);
        _comicsClicker.SetActive(true);
    }    
}
