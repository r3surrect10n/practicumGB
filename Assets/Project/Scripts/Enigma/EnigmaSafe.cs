using UnityEngine;

public class EnigmaSafe : MonoBehaviour, IResetable 
{
    [SerializeField] private EnigmaLock[] _enigmaLocks;

    [SerializeField] private AudioClip _lockSound;
    [SerializeField] private AudioClip _safeOpen;

    private SolvableMuzzle _solvableMuzzle;
    private Animator _anim;
    private AudioSource _audioSource;

    private void Awake()
    {
        _solvableMuzzle = GetComponent<SolvableMuzzle>();
        _anim = GetComponentInParent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void RotateLocks(int index)
    {
        if (!_enigmaLocks[index].IsRotating)
        {       
            _audioSource.PlayOneShot(_lockSound);

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
                _audioSource.PlayOneShot(_safeOpen);
                _solvableMuzzle.OnPlayerInvoke();
            }
        }

    }

    public void Reset()
    {
        foreach (var item in _enigmaLocks)
            item.ResetValue();
    }
}
