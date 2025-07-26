using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class StartFinalCutscene : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _screenDot;
    [SerializeField] private GameObject _notebookImage;
    [SerializeField] private GameObject _mouse;

    [SerializeField] private ViewManager _viewManager;
    [SerializeField] private CinemachineBrain _cmBrain;
    [SerializeField] private CinemachineCamera _playerCam;
    [SerializeField] private CinemachineCamera _firstCutsceneCam;

    [SerializeField] private GameObject _fadeObject;
    [SerializeField] private CanvasGroup _fadePanel;
    [SerializeField] private float _blinkTime;

    [SerializeField] private GameObject[] _cultFrames;    

    [SerializeField] private GameObject[] _phrases;

    private int _frameCounter = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {         
            _player.GetComponent<PlayerMovement>().enabled = false;
            _cmBrain.DefaultBlend.Time = 0f;
            StartCoroutine(PlayCutscene());            
        }
    }

    public void OnFinalButtonClick()
    {
        SwitchFrames(_frameCounter);

        _frameCounter++;
    }

    private void SwitchFrames(int frame)
    {
        switch (frame)
        {
            case 0:
                _mouse.SetActive(false);
                StartCoroutine(PlayFirstScene());
                break;
            case 1:
                _phrases[_frameCounter - 1].SetActive(false);
                _phrases[_frameCounter].SetActive(true);
                break;
            case 2:
                StartCoroutine(PlaySecondScene());
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
            case 21:
                break;
            case 22:
                break;
            case 23:
                break;



        }
    }

    private IEnumerator PlayCutscene()
    {
        _fadeObject.SetActive(true);

        yield return StartCoroutine(Fade(0, 1, _blinkTime));

        _notebookImage.SetActive(false);
        _screenDot.SetActive(false);
        _mouse.SetActive(true);
        
        _viewManager.SetView(_playerCam, _firstCutsceneCam);
        
        yield return StartCoroutine(Fade(1, 0, _blinkTime));

        _fadeObject.SetActive(false);
    }

    private IEnumerator PlayFirstScene()
    {
        _fadeObject.SetActive(true);

        yield return StartCoroutine(Fade(0, 1, _blinkTime));

        _cultFrames[_frameCounter - 1].SetActive(false);
        _cultFrames[_frameCounter].SetActive(true);
        _phrases[0].SetActive(true);        

        yield return StartCoroutine(Fade(1, 0, _blinkTime));

        _fadeObject.SetActive(false);
    }

    private IEnumerator PlaySecondScene()
    {
        _fadeObject.SetActive(true);

        yield return StartCoroutine(Fade(0, 1, _blinkTime));

        _cultFrames[_frameCounter - 1].SetActive(false);
        _cultFrames[_frameCounter].SetActive(true);
        _phrases[_frameCounter].SetActive(true);        

        yield return StartCoroutine(Fade(1, 0, _blinkTime));

        _fadeObject.SetActive(false);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            _fadePanel.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _fadePanel.alpha = endAlpha; 
    }
}
