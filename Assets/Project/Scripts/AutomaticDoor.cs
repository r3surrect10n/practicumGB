using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
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

    private void SetDoorCondition(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerMovement>())
        {            
            _doorCondition = !_doorCondition;
            _anim.SetBool("DoorStatus", _doorCondition);
        }
    }
}
