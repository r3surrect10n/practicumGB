using UnityEngine;

public class FuseControl : MonoBehaviour
{
    [SerializeField] private int fuseID;

    public int FuseID { get => fuseID; }

    private PowerCabinetControl powerCabinet;

    private bool isMove = false;
    private bool isChild = false;
    private Vector3 startPos;
    private Vector3 delta = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 figPos = transform.position;
            //figPos.x = mp.x + delta.x; figPos.z = mp.z + delta.z;
            figPos.x += mp.x - delta.x; figPos.y += 1.35f * (mp.y - delta.y);
            transform.position = figPos;
            delta = mp;
        }

    }
    public void SetParamsFuse(PowerCabinetControl pcc)
    {
        powerCabinet = pcc;
    }

    private void OnMouseDown()
    {        
        if (isChild)
        {
            transform.parent = null;
            transform.position = startPos;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            startPos = transform.position;
            isMove = true;
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            delta = mp;
        }
    }

    private void OnMouseUp()
    {
        if (isChild) return;
        if (Input.GetMouseButtonUp(0))
        {
            isMove = false;
            delta = Vector3.zero;
            if (powerCabinet.FuseEntered(gameObject))
            {
                isChild = true;
                return;
            }
            transform.position = startPos;
        }
    }
}
