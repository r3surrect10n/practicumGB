using UnityEngine;

public class EnigmaSafe : MonoBehaviour
{
    [SerializeField] private EnigmaLock[] _enigmaLocks;

    public void RotateLocks(int index)
    {
        switch (index)
        {
            case 0:
                _enigmaLocks[0].RotateForward();
                _enigmaLocks[1].RotateForward();
                _enigmaLocks[2].RotateBackward();
                break;
            case 1:
                _enigmaLocks[0].RotateBackward();
                _enigmaLocks[1].RotateForward();
                _enigmaLocks[2].RotateForward();
                break;
            case 2:
                _enigmaLocks[0].RotateForward();
                _enigmaLocks[1].RotateBackward();
                _enigmaLocks[2].RotateForward();
                break;
            default:
                break;
        }
    }
}
