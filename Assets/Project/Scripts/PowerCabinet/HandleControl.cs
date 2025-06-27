using UnityEngine;

public class HandleControl : MonoBehaviour
{
    private PowerCabinetControl powerCabinet;
    private HingeJoint joint;

    private bool isUp = false;

    private void Awake()
    {
        joint = GetComponent<HingeJoint>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParams(PowerCabinetControl pcc)
    {
        powerCabinet = pcc;
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isUp)
            {
                joint.useMotor = false;
                isUp = false;
            }
            else
            {
                joint.useMotor = true;
                isUp = true;
                if (powerCabinet.TestFuses() == false)
                {
                    Invoke("HandleOff", 1.5f);
                }
            }
        }
    }

    private void HandleOff()
    {
        joint.useMotor = false;
        isUp = false;
    }
}
