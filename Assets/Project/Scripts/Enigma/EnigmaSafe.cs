using UnityEngine;

public class EnigmaSafe : MonoBehaviour, IResetable, IMuzzles 
{
    [SerializeField] private string _id;

    [SerializeField] private EnigmaLock[] _enigmaLocks;

    [SerializeField] private AudioClip _lockSound;
    [SerializeField] private AudioClip _safeOpen;

    private SolvableMuzzle _solvableMuzzle;
    private Muzzle _muzzle;
    private Animator _anim;
    private AudioSource _audioSource;

    public string ID => _id;

    private void Awake()
    {
        _solvableMuzzle = GetComponent<SolvableMuzzle>();
        _muzzle =GetComponent<Muzzle>();
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
                SaveSystem.Instance.MarkMuzzleSolved(this);

                _anim.SetBool("IsOpen", true);
                _audioSource.PlayOneShot(_safeOpen);
                _solvableMuzzle.OnPlayerInvoke();

                SaveSystem.Instance.SaveGame();
            }
        }

    }

    public void Reset()
    {
        foreach (var item in _enigmaLocks)
            item.ResetValue();
    }

    public void Solve()
    {
        _muzzle.SolvedCondition();

        _anim.SetBool("IsOpen", true);
        _anim.speed = 1000f;        
    }
}
