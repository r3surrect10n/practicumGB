using UnityEngine;
using UnityEngine.InputSystem;

public class UIInteract : MonoBehaviour
{
    [SerializeField] private GameObject _note;
    [SerializeField] private PlayerInput playerInput;

    public void OnClose()
    {
        Cursor.visible = false;        
        Cursor.lockState = CursorLockMode.Locked;
        playerInput.enabled = true;
        _note.SetActive(false);        
    }

    public void MouseCursor(bool showing)
    {
        Cursor.visible = showing;
        if (showing)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
