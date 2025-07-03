using System.Collections;
using UnityEngine;

public class EnigmaLock : MonoBehaviour, IClickable
{
    [SerializeField] private EnigmaSafe _enigmaSafe;

    [SerializeField] private int _lockNumber;

    private const float _rotateDegrees = 36f;
    private float _rotationDuration = 0.5f;

    private int _startValue = 0;
    private int _currentValue;

    private bool _isRotating = false;


    private void Start()
    {
        ResetValue();
    }

    public void SetValue()
    {
        _enigmaSafe.RotateLocks(_lockNumber);
    }

    public void RotateForward()
    {
        if (!_isRotating)
        {
            _currentValue = (_currentValue + 1) % 10;
        }
    }

    public void RotateBackward()
    {

    }

    private void ResetValue()
    {
        _currentValue = _startValue;
    }

    private IEnumerator Rotate(float angle)
    {
        _isRotating = true;

        Quaternion startedRotaton = transform.localRotation;
        yield return null;
    }
}
