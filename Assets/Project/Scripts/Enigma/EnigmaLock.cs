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
        if (!_isRotating)
        {
            _currentValue = (_currentValue + 9) % 10;
            StartCoroutine(Rotate(_rotateDegrees));
        }
    }

    public void ResetValue()
    {        
        int steps = (_startValue -  _currentValue + 10) % 10;

        if (steps == 0)
            return;

        _currentValue = _startValue;

        StartCoroutine(Rotate(-steps * _rotateDegrees));
    }

    private IEnumerator Rotate(float angle)
    {
        _isRotating = true;        

        Quaternion currentRotaton = transform.localRotation;
        Quaternion endRotation = currentRotaton * Quaternion.Euler(0, 0, angle);

        float elapsed = 0;

        while (elapsed < _rotationDuration) 
        {
            transform.localRotation = Quaternion.Slerp(currentRotaton, endRotation, elapsed / _rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = endRotation;

        _isRotating = false;
    }
}
