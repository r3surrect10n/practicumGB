using System;
using UnityEngine;

public class EnigmaSafe : MonoBehaviour 
{
    public static Action IsMuzzleDone;

    [SerializeField] private EnigmaLock[] _enigmaLocks;

    private Muzzle _muzzle;
    private Animator _anim;

    private void Awake()
    {
        _muzzle = GetComponent<Muzzle>();
        _anim = GetComponentInParent<Animator>();
    }

    public void RotateLocks(int index)
    {
        if (!_enigmaLocks[index].IsRotating)
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

            if (_enigmaLocks[0].CurrentValue == 7 && _enigmaLocks[1].CurrentValue == 5 && _enigmaLocks[2].CurrentValue == 3)
            {
                _anim.SetBool("IsOpen", true);
                _muzzle.OnPlayerInvoke();
            }
        }

    }
}
