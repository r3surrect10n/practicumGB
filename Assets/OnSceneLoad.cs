using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;

public class OnSceneLoad : MonoBehaviour
{
    [SerializeField] private GameObject _playerObject;

    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;
    private PlayerInteraction _playerInteraction;

    [SerializeField] private GameObject _fadePanel;
    [SerializeField] private CanvasGroup _canvasGr;
    [SerializeField] private float _fadeDuration = 1f;

    [SerializeField] private float _forTipTime = 2f;
    [SerializeField] private GameObject _movementTip;

    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private GameObject _loadImage;
    [SerializeField] private GameObject _loadText;
    private Coroutine _loadCoroutine;

    private string SavePath => Path.Combine(Application.persistentDataPath, "autosave.json");
    
    private void Awake()
    {      
        _fadePanel.SetActive(true);

        if (_playerObject != null)
        {
            _playerMovement = _playerObject.GetComponent<PlayerMovement>();
            _playerLook = _playerObject.GetComponent<PlayerLook>();
            _playerInteraction = _playerObject.GetComponent<PlayerInteraction>();
        }       
    }

    private void Start()
    {   
        SetPlayerComponents(false);

        
        _canvasGr.alpha = 1f;
        _fadePanel.SetActive(true);
        
        StartCoroutine(SceneStartRoutine());
    }

    private IEnumerator SceneStartRoutine()
    {
        if (File.Exists(SavePath) && SceneManager.GetActiveScene().name == "Building")
        {
            _audioListener.enabled = false;
            _loadImage.SetActive(true);
            _loadText.SetActive(true);
            yield return new WaitForSeconds(3f);
            _loadImage.SetActive(false);
            _loadText.SetActive(false);
            _audioListener.enabled = true;
        }

        float timer = 0f;
        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            _canvasGr.alpha = 1f - (timer / _fadeDuration);
            yield return null;
        }
        _canvasGr.alpha = 0f;
        
        yield return new WaitForSeconds(_forTipTime);
        
        if (_movementTip != null)
        {
            _movementTip.SetActive(true);
        }
        
        SetPlayerComponents(true);
        
        _fadePanel.SetActive(false);
    }

    private void SetPlayerComponents(bool state)
    {
        if (_playerMovement != null) _playerMovement.enabled = state;
        if (_playerLook != null) _playerLook.enabled = state;
        if (_playerInteraction != null) _playerInteraction.enabled = state;
    }    
}
