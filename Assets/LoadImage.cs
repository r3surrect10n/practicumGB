using UnityEngine;
using DG.Tweening;

public class LoadImage : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float rotateTime = 0.5f;
    [SerializeField] private float waitTime = 0.3f;
    [SerializeField] private float scaleMultiplier = 1.1f;
    [SerializeField] private int repeatCount = 2;        

    private Sequence _flipSequence;

    private void OnEnable()
    {
        RunFlipSequence();
    }   

    private void RunFlipSequence()
    {        
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        _flipSequence = DOTween.Sequence();

        for (int i = 0; i < repeatCount; i++)
        {
            
            _flipSequence.Append(transform.DORotate(new Vector3(0, 0, 180f), rotateTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutSine));
            _flipSequence.Join(transform.DOScale(Vector3.one * scaleMultiplier, rotateTime)
                .SetEase(Ease.InOutSine));
            
            _flipSequence.AppendInterval(waitTime);
            
            _flipSequence.Append(transform.DORotate(new Vector3(0, 0, -180f), rotateTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutSine));
            _flipSequence.Join(transform.DOScale(Vector3.one, rotateTime)
                .SetEase(Ease.InOutSine));           
        }
    }
}