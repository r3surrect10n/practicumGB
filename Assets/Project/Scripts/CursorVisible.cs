using UnityEngine;

public class CursorVisible : MonoBehaviour
{
    [SerializeField] private GameObject _crosshair;

    private void Start()
    {
        CursorDisable();        
    }

    public void CursorEnable()
    {        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        _crosshair.SetActive(false);
    }

    public void CursorDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _crosshair.SetActive(true);
    }    
}
