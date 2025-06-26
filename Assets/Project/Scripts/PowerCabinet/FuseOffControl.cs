using UnityEngine;

public class FuseOffControl : MonoBehaviour
{
    private Animator anim;
    private PowerCabinetControl powerCabinet;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetTrigger("TriggerMin");
            Invoke("FuseOff", 1f);
        }
    }

    private void FuseOff()
    {
        powerCabinet.FuseOffNoActive(transform.position.x);
        transform.parent.gameObject.SetActive(false);
    }

    public void SetParams(PowerCabinetControl pcc)
    {
        powerCabinet = pcc;
    }
}
