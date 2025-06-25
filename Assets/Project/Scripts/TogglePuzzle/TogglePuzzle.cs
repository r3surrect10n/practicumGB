using UnityEngine;

public class TogglePuzzle : MonoBehaviour
{
    [SerializeField] private Toggle[] _toggles;    

    public void ToggleSwitchPuzzle(int toggleNum)
    {
        _toggles[toggleNum].SwitchToggleStatus();

        if (toggleNum == 0)
            _toggles[toggleNum + 1].SwitchToggleStatus();
        else if (toggleNum == _toggles.Length)
            _toggles[toggleNum - 1].SwitchToggleStatus();
        else
        {
            _toggles[toggleNum - 1].SwitchToggleStatus();
            _toggles[toggleNum + 1].SwitchToggleStatus();
        }
    }
}
