using UnityEngine;

public class ComicsEnd : MonoBehaviour
{
    [SerializeField] private GameObject[] _comicsFrames;
    [SerializeField] private GameObject _comicsClicker;
    [SerializeField] private OnSceneExit _onSceneExit;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private int _currentFrame = 0;

    public void NextFrameOnClick()
    {
        _currentFrame++;
        SwitchFrames();
    }

    private void SwitchFrames()
    {        
        if (_currentFrame < _comicsFrames.Length)
        {
            switch (_currentFrame)
            {
                default:
                    _comicsFrames[_currentFrame - 1].SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;

                case 1:                    
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;
                case 8:
                    _audioSource.PlayOneShot(_audioClip);
                    _onSceneExit.OnSceneEnd("MainMenuScene");
                    break;
            }
        }
        else 
            return;
    }
}
