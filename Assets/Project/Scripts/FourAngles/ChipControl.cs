using UnityEngine;

public class ChipControl : MonoBehaviour
{
    [SerializeField] private int chipID;

    public int ChipID { get => chipID; }

    private FourAnglesControl anglesControl;

    private bool isMove = false;
    private int startNum = -1, endNum = -1, oldNum = -1;
    private Vector3 startPos;
    private Vector3 delta = Vector3.zero;
    private int[] chipsTabl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 figPos = transform.position;
            float dx = mp.x - delta.x, dz = mp.z - delta.z;
            int x = oldNum % 5, z = oldNum / 5;
            if (dx > 0)
            {   //  ������
                if (transform.position.x < (x * 0.288f - 0.576f)) ;
                else if ((x < 4) && (chipsTabl[oldNum + 1] != 0)) dx = 0;
                else if (x == 4) dx = 0;
            }
            else
            {   //  �����
                if (transform.position.x > (x * 0.288f - 0.576f)) ;
                else if ((x > 0) && (chipsTabl[oldNum - 1] != 0)) dx = 0;
                else if (x == 0) dx = 0;
            }
            if (dz > 0)
            {   //  �����
                if (transform.position.z < (z * 0.288f - 0.576f)) ;
                else if ((z < 4) && (chipsTabl[oldNum + 5] != 0)) dz = 0;
                else if (z == 4) dz = 0;
            }
            else
            {   //  ����
                if (transform.position.z > (z * 0.288f - 0.576f)) ;
                else if ((z > 0) && (chipsTabl[oldNum - 5] != 0)) dz = 0;
                else if (z == 0) dz = 0;
            }
            if (Mathf.Abs(dx) > Mathf.Abs(dz))
            {
                figPos.x += dx;
                figPos.z = z * 0.288f - 0.576f;
            }
            if (Mathf.Abs(dx) < Mathf.Abs(dz))
            {
                figPos.z += dz;
                figPos.x = x * 0.288f - 0.576f;
            }
            transform.position = figPos;
            delta = mp;
            int newPos = NumPos();
            if (oldNum != newPos) oldNum = newPos;
        }
    }

    public void SetBoardControl(FourAnglesControl fac)
    {
        anglesControl = fac;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = transform.position;
            startNum = NumPos();
            oldNum = startNum;
            isMove = true;
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            delta = mp;
            chipsTabl = anglesControl.GetChipsTabl();
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (oldNum != startNum)
            {
                //print($"old={oldNum} start={startNum}");
                endNum = oldNum;
                Vector3 pos = new Vector3((endNum % 5) * 0.288f - 0.576f, transform.position.y, (endNum / 5) * 0.288f - 0.576f);
                transform.position = pos;
                anglesControl.MoveChip(startNum, endNum);
            }
            else
            {
                transform.position = startPos;
            }
            isMove = false;
        }
    }

    private int NumPos()
    {
        int x = Mathf.RoundToInt((transform.position.x + 0.576f) / 0.288f);
        int y = Mathf.RoundToInt((transform.position.z + 0.576f) / 0.288f);
        return 5 * y + x;
    }
}
