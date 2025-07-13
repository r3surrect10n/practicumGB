using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIController: MonoBehaviour
{
    [SerializeField] private PlayerLook _playerLook;
    [SerializeField] private GameObject _crosshair;

    [SerializeField] private GameObject _menuPanel;    

    private bool _isPause = false;

    private void OnEnable()
    {
        PlayerInteraction.PauseGame += PauseGame;
    }

    private void OnDisable()
    {
        PlayerInteraction.PauseGame -= PauseGame;
    }

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
    
    //public void OnEscPush(InputAction.CallbackContext callbackContext)
    //{
    //    if (callbackContext.phase != InputActionPhase.Started)
    //        return;        

    //    PauseGame();
    //}

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    
    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void PauseGame()
    {
        _isPause = !_isPause;

        if (_isPause)
        {
            CursorEnable();
            Time.timeScale = 0;
        }
        else
        {
            CursorDisable();
            Time.timeScale = 1;
        }
        
        _menuPanel.SetActive(_isPause);
        _playerLook.enabled = !_isPause;        
    }
}
