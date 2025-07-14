using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    [SerializeField] private bool _doorStatus;

    [SerializeField] private AudioClip _openDoorSound;
    [SerializeField] private AudioClip _closeDoorSound;

    private AudioSource _doorAudioSource;
    private Animator _anim;
    private bool _doorCondition = false;

    

    private void Awake()
    {
        _doorAudioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
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
    }

    private void SetDoorCondition(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerMovement>() && _doorStatus)
        {
            Debug.Log(name);

            //_doorCondition = !_doorCondition;
            _anim.SetBool("DoorStatus", _doorCondition);
        }
    }

    public void PlaySound()
    {
        if (_doorCondition)
            _doorAudioSource.PlayOneShot(_openDoorSound);
        else
            _doorAudioSource?.PlayOneShot(_closeDoorSound);
    }

}
