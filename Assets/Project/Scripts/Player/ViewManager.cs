using Unity.Cinemachine;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private CursorVisible _cursor;

    private bool _isFirstPerson = true;
    private int _currrentCamPriority = 100;
    private int _otherCamPriority = 10;    

    public bool IsFirstPerson => _isFirstPerson;
    public CinemachineCamera CurrentStaticCam { get; private set; }

    public void SetView(CinemachineCamera currentCam, CinemachineCamera nextCam)
    {
        _isFirstPerson = !_isFirstPerson;        

        currentCam.Priority = _otherCamPriority;
        nextCam.Priority = _currrentCamPriority;

        if (_isFirstPerson)
            _cursor.CursorDisable();
        else
            _cursor.CursorEnable();
    }
}
