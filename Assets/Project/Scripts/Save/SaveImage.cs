using UnityEngine;
using DG.Tweening;

public class SpriteFlipper : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float rotateTime = 0.5f;
    [SerializeField] private float waitTime = 0.3f;
    [SerializeField] private float scaleMultiplier = 1.1f;
    [SerializeField] private int repeatCount = 2;
    [SerializeField] private bool disableAfter = true;

    private CanvasGroup _canvasRend;

    private Sequence _flipSequence;

    private void OnEnable()
    {
        _canvasRend = GetComponent<CanvasGroup>();
    }

    public void Save()
    {
        _canvasRend.alpha = 0.5f;
        RunFlipSequence();
    }

    private void RunFlipSequence()
    {
        // Сбрасываем трансформацию перед началом анимации
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;        

        // Убиваем старую последовательность, если она есть
        if (_flipSequence != null && _flipSequence.IsActive())
        {
            _flipSequence.Kill();
        }

        _flipSequence = DOTween.Sequence().SetUpdate(true);

        for (int i = 0; i < repeatCount; i++)
        {
            // Поворот и масштабирование вперёд
            _flipSequence.Append(transform.DORotate(new Vector3(0, 0, 180f), rotateTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutSine));
            _flipSequence.Join(transform.DOScale(Vector3.one * scaleMultiplier, rotateTime)
                .SetEase(Ease.InOutSine));

            // Ожидание
            _flipSequence.AppendInterval(waitTime);

            // Поворот и масштабирование обратно
            _flipSequence.Append(transform.DORotate(new Vector3(0, 0, -180f), rotateTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutSine));
            _flipSequence.Join(transform.DOScale(Vector3.one, rotateTime)
                .SetEase(Ease.InOutSine));

            // Ожидание только если это не последняя итерация
            if (i < repeatCount - 1)
            {
                _flipSequence.AppendInterval(waitTime);
            }
        }

        // Важно: Устанавливаем прозрачность в 0 только после завершения анимации!
        if (disableAfter)
        {
            _flipSequence.OnComplete(() => _canvasRend.alpha = 0f);
        }
    }
}