using System.Collections;
using UnityEngine;

public class Comics : MonoBehaviour
{
    [SerializeField] private GameObject[] _comicsFrames;
    [SerializeField] private GameObject _comicsClicker;
    [SerializeField] private GameObject _mouse;
    [SerializeField] private float _fadeDuration = 0.5f; // Длительность анимации

    private int _currentFrame = 0;
    private bool _isTransitioning = false; // Флаг для предотвращения множественных кликов во время анимации

    private void Start()
    {
        // Включаем первый кадр при старте
        if (_comicsFrames.Length > 0)
        {
            _comicsFrames[0].SetActive(true);
            SetAlpha(_comicsFrames[0], 1f);
        }
    }

    public void NextFrameOnClick()
    {
        if (_isTransitioning) return;
        StartCoroutine(SwitchFramesWithFade());
    }

    private IEnumerator SwitchFramesWithFade()
    {
        _isTransitioning = true;
        _currentFrame++;

        if (_currentFrame == 1)
            _mouse.SetActive(false);

        if (_currentFrame >= _comicsFrames.Length)
        {
            _isTransitioning = false;
            yield break;
        }

        if (_currentFrame != 5)
            yield return StartCoroutine(HandleSpecialCases());

        // Включаем новый кадр и плавно проявляем его
        GameObject newFrame = _comicsFrames[_currentFrame];
        newFrame.SetActive(true);
        yield return StartCoroutine(FadeObject(newFrame, 0f, 1f, _fadeDuration));

        // Обрабатываем специальные случаи из твоего кода

        _isTransitioning = false;
    }

    private IEnumerator HandleSpecialCases()
    {
        switch (_currentFrame)
        {
            case 3:
                yield return StartCoroutine(FadeAndDisableFrame(2));
                break;

            case 5:
                yield return StartCoroutine(FadeAndDisableFrame(3));
                break;

            case 7:
                yield return StartCoroutine(FadeAndDisableFrame(23));                
                break;

            case 9:
                yield return StartCoroutine(FadeAndDisableFrame(8));
                break;

            case 10:
                yield return StartCoroutine(FadeAndDisableFrame(9));
                break;

            case 19:
                yield return StartCoroutine(FadeAndDisableFrame(24));                
                break;

            case 21:
                yield return StartCoroutine(FadeAndDisableFrame(20));
                break;

            case 22:
                yield return StartCoroutine(FadeAndDisableObject(_comicsClicker));
                break;
        }
    }

    private IEnumerator FadeAndDisableFrame(int frameIndex)
    {
        if (frameIndex >= 0 && frameIndex < _comicsFrames.Length && _comicsFrames[frameIndex].activeSelf)
        {
            yield return StartCoroutine(FadeObject(_comicsFrames[frameIndex], 1f, 0f, _fadeDuration));
            _comicsFrames[frameIndex].SetActive(false);
        }
    }

    private IEnumerator FadeAndDisableObject(GameObject obj)
    {
        if (obj != null && obj.activeSelf)
        {
            yield return StartCoroutine(FadeObject(obj, 1f, 0f, _fadeDuration));
            obj.SetActive(false);
        }
    }

    private IEnumerator FadeObject(GameObject obj, float startAlpha, float endAlpha, float duration)
    {
        CanvasGroup group = GetOrAddCanvasGroup(obj);
        float time = 0f;
        
        while (time < duration)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            yield return null;
        }
        
        group.alpha = endAlpha;
    }

    private CanvasGroup GetOrAddCanvasGroup(GameObject obj)
    {
        CanvasGroup group = obj.GetComponent<CanvasGroup>();
        if (group == null) group = obj.AddComponent<CanvasGroup>();
        return group;
    }

    private void SetAlpha(GameObject obj, float alpha)
    {
        CanvasGroup group = GetOrAddCanvasGroup(obj);
        group.alpha = alpha;
    }

    //public void NextFrameOnClick()
    //{
    //    _currentFrame++;
    //    SwitchFrames();
    //}

    //private void SwitchFrames()
    //{        
    //    if (_currentFrame < _comicsFrames.Length)
    //    {
    //        switch (_currentFrame)
    //        {
    //            default:
    //                _comicsFrames[_currentFrame].SetActive(true);
    //                break;

    //            case 1:
    //                _mouse.SetActive(false);
    //                _comicsFrames[_currentFrame].SetActive(true);
    //                break;
    //            case 3:
    //                _comicsFrames[_currentFrame - 1].SetActive(false);
    //                _comicsFrames[_currentFrame].SetActive(true);
    //                break;

    //            case 5:
    //                _comicsFrames[3].SetActive(false);
    //                _comicsFrames[_currentFrame].SetActive(true);
    //                break;

    //            case 7:
    //                _comicsFrames[_currentFrame - 3].SetActive(false);
    //                _comicsFrames[_currentFrame - 1].SetActive(false);
    //                _comicsFrames[_currentFrame].SetActive(true);
    //                break;

    //            case 9:
    //                _comicsFrames[_currentFrame - 1].SetActive(false);
    //                _comicsFrames[_currentFrame].SetActive(true);
    //                break;

    //            case 10:
    //                _comicsFrames[_currentFrame - 1].SetActive(false);
    //                _comicsFrames[_currentFrame].SetActive(true);
    //                break;

    //            case 19:
    //                _comicsFrames[14].SetActive(false);
    //                _comicsFrames[_currentFrame - 1].SetActive(false);
    //                _comicsFrames[_currentFrame].SetActive(true);
    //                break;

    //            case 21:
    //                _comicsFrames[_currentFrame - 1].SetActive(false);
    //                _comicsFrames[_currentFrame].SetActive(true);                    
    //                break;
    //            case 22:
    //                _comicsClicker.SetActive(false);
    //                _comicsFrames[_currentFrame].SetActive(true);
    //                break;
    //        }
    //    }
    //    else 
    //        return;
    //}
}
