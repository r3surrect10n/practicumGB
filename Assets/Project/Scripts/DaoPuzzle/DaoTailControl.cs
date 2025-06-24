using System;
using UnityEngine;

[Serializable]
public class DaoTailControl : MonoBehaviour
{
    [SerializeField] private int numberTail;
    [SerializeField] private DaoBoardControl boardControl;

    private bool isMove = false;
    private bool isChild = false;
    private Vector3 startPos;
    private Vector3 delta = Vector3.zero;


    public int NumberTail { get => numberTail; }

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
            //figPos.x = mp.x + delta.x; figPos.z = mp.z + delta.z;
            figPos.x += mp.x - delta.x; figPos.y += 1.35f * (mp.y - delta.y);
            //float x = Mathf.Abs(-1.8f - figPos.x);
            //figPos.z = -2.08f * Mathf.Cos(Mathf.Asin(x / 2.08f));
            transform.position = figPos;
            delta = mp;
        }

    }

    public void SetParamsTail(int zn, DaoBoardControl dbc)
    {
        numberTail = zn;
        boardControl = dbc;
    }

    private void OnMouseDown()
    {
        if (isChild) return;
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
        if (!Input.GetMouseButtonDown(0))
        {
            isMove = false;
            delta = Vector3.zero;
            if(boardControl.TestDownTail(gameObject))
            {
                isChild = true;
                return;
            }
            transform.position = startPos;
        }
    }
}
