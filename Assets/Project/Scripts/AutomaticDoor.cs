using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    [SerializeField] private bool _doorStatus;

    [SerializeField] private AudioClip _openDoorSound;
    [SerializeField] private AudioClip _closeDoorSound;

    private AudioSource _doorAudioSource;
    private Animator _anim;
    private Collider _doorTrigger;
    private bool _doorCondition = false;    

    private void Awake()
    {
        _doorAudioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
        _doorTrigger = GetComponent<Collider>();
    }

    private void Start()
    {
        if (!_doorStatus)
            _doorTrigger.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {     
        _doorCondition = true;
        SetDoorCondition(other);        
    }

    private void OnTriggerExit(Collider other)
    {
        _doorCondition = false;
        SetDoorCondition(other);        
    }

    public void SetDoorStatusOpen()
    {
        _doorStatus = true;
        _doorTrigger.enabled = true;        
    }

    private void SetDoorCondition(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerMovement>() && _doorStatus)
            _anim.SetBool("DoorStatus", _doorCondition);        
    }

    public void PlaySound()
    {
        if (_doorCondition)
            _doorAudioSource.PlayOneShot(_openDoorSound);
        else
            _doorAudioSource?.PlayOneShot(_closeDoorSound);
    }

}
