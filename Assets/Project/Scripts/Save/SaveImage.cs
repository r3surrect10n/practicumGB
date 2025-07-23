using DG.Tweening;
using UnityEngine;

public class SaveImage : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 0.5f;
    [SerializeField] private int rotationCount = 2; 
    [SerializeField] private float fadeDelay = 0.5f; 

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;

        StartRotating();
    }

    private void StartRotating()
    {
        Sequence seq = DOTween.Sequence();
                
        seq.Append(
            transform.DORotate(new Vector3(0, 0, 360 * rotationCount), rotationDuration * rotationCount,RotateMode.FastBeyond360).SetEase(Ease.Linear)
        );
        
        seq.AppendInterval(fadeDelay);
        
        seq.OnComplete(() => gameObject.SetActive(false));
    }
}
