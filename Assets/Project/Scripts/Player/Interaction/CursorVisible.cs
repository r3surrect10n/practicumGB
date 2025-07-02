using UnityEngine;

public class CursorVisible : MonoBehaviour
{
    private void Start()
    {
        CursorDisable();
    }

    public static void CursorEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void CursorDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
