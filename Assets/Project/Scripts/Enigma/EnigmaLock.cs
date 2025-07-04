using System.Collections;
using UnityEngine;

public class EnigmaLock : MonoBehaviour, IClickable
{
    [SerializeField] private EnigmaSafe _enigmaSafe;

    [SerializeField] private int _lockNumber;

    private const float _rotateDegrees = 36f;
    private float _rotationDuration = 0.3f;

    private int _startValue = 0;
    private int _currentValue;

    private bool _isRotating = false;

    public bool IsRotating => _isRotating;
    public int CurrentValue => _currentValue;

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
            StartCoroutine(Rotate(-_rotateDegrees));
        }
    }

    public void RotateBackward()
    {
        _currentValue = (_currentValue + 9) % 10;
        StartCoroutine(Rotate(_rotateDegrees));
    }

    private void ResetValue()
    {
        _currentValue = _startValue;
    }

    private IEnumerator Rotate(float angle)
    {
        _isRotating = true;        

        Quaternion startedRotaton = transform.localRotation;
        Quaternion finalRotation = startedRotaton * Quaternion.Euler(0, 0, angle);

        float elapsed = 0;

        while (elapsed < _rotationDuration) 
        {
            transform.localRotation = Quaternion.Slerp(startedRotaton, finalRotation, elapsed / _rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = finalRotation;

        _isRotating = false;
    }
}
