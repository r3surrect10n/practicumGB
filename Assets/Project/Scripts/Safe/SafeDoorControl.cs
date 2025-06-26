using UnityEngine;

public class SafeDoorControl : MonoBehaviour
{
    [SerializeField] private HingeJoint jointHandle;
    [SerializeField] private HingeJoint jointDoor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jointDoor.useMotor = false;
        jointHandle.useMotor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        jointHandle.useMotor = true;
        Invoke("BeginOpenDoor", 1.5f);
    }

    public void BeginOpenDoor()
    {
        jointDoor.useMotor = true;
    }
}
