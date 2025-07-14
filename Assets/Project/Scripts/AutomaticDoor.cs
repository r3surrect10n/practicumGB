using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    [SerializeField] private bool _doorStatus;

    private Animator _anim;
    private bool _doorCondition = false;
    

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {        
        SetDoorCondition(other);   
    }

    private void OnTriggerExit(Collider other)
    {
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
            _doorCondition = !_doorCondition;
            _anim.SetBool("DoorStatus", _doorCondition);
        }
    }

}
