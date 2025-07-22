using UnityEngine;

public class Comics : MonoBehaviour
{
    [SerializeField] private GameObject[] _comicsFrames;
    [SerializeField] private GameObject _comicsClicker;
    [SerializeField] private GameObject _mouse;

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
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;

                case 1:
                    _mouse.SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;
                case 3:
                    _comicsFrames[_currentFrame - 1].SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;

                case 5:
                    _comicsFrames[3].SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;

                case 7:
                    _comicsFrames[_currentFrame - 3].SetActive(false);
                    _comicsFrames[_currentFrame - 1].SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;

                case 9:
                    _comicsFrames[_currentFrame - 1].SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;

                case 10:
                    _comicsFrames[_currentFrame - 1].SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;

                case 19:
                    _comicsFrames[14].SetActive(false);
                    _comicsFrames[_currentFrame - 1].SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;

                case 21:
                    _comicsFrames[_currentFrame - 1].SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);                    
                    break;
                case 22:
                    _comicsClicker.SetActive(false);
                    _comicsFrames[_currentFrame].SetActive(true);
                    break;
            }
        }
        else 
            return;
    }
}
