using UnityEngine;
using DG.Tweening;

public class SpriteFlipper : MonoBehaviour
{
    [Header("���������")]
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
        // ���������� ������������� ����� ������� ��������
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;        

        // ������� ������ ������������������, ���� ��� ����
        if (_flipSequence != null && _flipSequence.IsActive())
        {
            _flipSequence.Kill();
        }

        _flipSequence = DOTween.Sequence().SetUpdate(true);

        for (int i = 0; i < repeatCount; i++)
        {
            // ������� � ��������������� �����
            _flipSequence.Append(transform.DORotate(new Vector3(0, 0, 180f), rotateTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutSine));
            _flipSequence.Join(transform.DOScale(Vector3.one * scaleMultiplier, rotateTime)
                .SetEase(Ease.InOutSine));

            // ��������
            _flipSequence.AppendInterval(waitTime);

            // ������� � ��������������� �������
            _flipSequence.Append(transform.DORotate(new Vector3(0, 0, -180f), rotateTime, RotateMode.LocalAxisAdd)
                .SetEase(Ease.InOutSine));
            _flipSequence.Join(transform.DOScale(Vector3.one, rotateTime)
                .SetEase(Ease.InOutSine));

            // �������� ������ ���� ��� �� ��������� ��������
            if (i < repeatCount - 1)
            {
                _flipSequence.AppendInterval(waitTime);
            }
        }

        // �����: ������������� ������������ � 0 ������ ����� ���������� ��������!
        if (disableAfter)
        {
            _flipSequence.OnComplete(() => _canvasRend.alpha = 0f);
        }
    }
}