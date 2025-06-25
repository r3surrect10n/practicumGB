using UnityEngine;

public class Toggle : MonoBehaviour
{
    //[SerializeField] private TogglePuzzle _togglePuzzle;
    [SerializeField] private int _toggleNumber;
    [SerializeField] private bool _toggleStatus;

    private Renderer _renderer;

    public int ToggleNumber => _toggleNumber;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        SetColor();        
    }

    public void TurnToggle()
    {
        //_togglePuzzle.ToggleSwitchPuzzle(_toggleNumber);
    }

    public void SwitchToggleStatus()
    {
        Debug.Log(gameObject.name + " " + _toggleStatus);
        _toggleStatus = !_toggleStatus;
        SetColor();
    }

    private void SetColor()
    {
        if (_toggleStatus)
            _renderer.material.color = Color.green;
        else
            _renderer.material.color = Color.red;
    }
}
