using Unity.Cinemachine;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private UIController _uiController;    

    private bool _isFirstPerson = true;
    private int _currrentCamPriority = 100;
    private int _otherCamPriority = 10;    

    public bool IsFirstPerson => _isFirstPerson;
    public CinemachineCamera CurrentStaticCam { get; private set; }

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
    }

    public void SetView(CinemachineCamera currentCam, CinemachineCamera nextCam)
    {
        _isFirstPerson = !_isFirstPerson;        

        currentCam.Priority = _otherCamPriority;
        nextCam.Priority = _currrentCamPriority;

        if (_isFirstPerson)        
            _uiController.CursorDisable();        
        else        
            _uiController.CursorEnable();        
    }
}
