using UnityEngine;
using System.Collections.Generic;

public class DaoBoardControl : MonoBehaviour
{
    [SerializeField] private GameObject[] tails;
    [SerializeField] private DaoUIControl uIControl;

    private List<GameObject> daoTails = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShuffledTails();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShuffledTails()
    {
        List<int> listNumbers = new List<int>() { 0, 1, 2, 3, 4, 5 };
        int rndNumber = 0;
        int count = 0, numTail;
        Vector3 posTail = Vector3.zero;
        DaoBoardControl dbc = gameObject.GetComponent<DaoBoardControl>();
        while(listNumbers.Count > 0)
        {
            rndNumber = Random.Range(0, listNumbers.Count);
            numTail = listNumbers[rndNumber];
            listNumbers.RemoveAt(rndNumber);
            posTail.x = -2.4f;
            posTail.y = 2.8f - count * 1.1f;
            posTail.z = -1f;
            GameObject tail = Instantiate(tails[numTail], posTail, Quaternion.Euler(-90f, 0, 0));
            tail.GetComponent<DaoTailControl>().SetParamsTail(numTail, dbc);
            //daoTails.Add(tail);
            count++;
        }
    }

    public bool TestDownTail(GameObject tail)
    {
        //print(tail.GetComponent<BoxCollider>().size);
        Vector3 tailPos = tail.transform.position;
        Vector3 boardPos = transform.position;
        if (Mathf.Abs(tailPos.x - boardPos.x) < 0.2f)
        {   //  по горизонтали в створе доски
            float fl_in = (2.24f - tailPos.y) / 0.71f;
            //float fl_in = (2.24f - tailPos.y) / 0.85f;
            int index = (int)(fl_in);
            print($"pos={tailPos}  fl_in={fl_in} index={index}");
            bool isFree = true;
            foreach(GameObject go in daoTails)
            {
                int tailIndex = (int)((2.24f - go.transform.position.y) / 0.85f);
                if (tailIndex == index)
                {
                    print($"foreach ti={tailIndex} ind={index}");
                    isFree = false;
                    break;
                }
            }
            if (isFree && ((index >= 0) && (index < 6)))
            {
                Vector3 downPos = new Vector3(boardPos.x, 2.24f - index * 0.85f, -0.7f);
                tail.transform.position = downPos;
                tail.transform.parent = transform;
                daoTails.Add(tail);
                Success();
                return true;
            }
        }
        return false;
    }

    public void Success()
    {
        if (daoTails.Count == 6)
        {
            int indexPos;
            for (int i = 0; i < 6; i++)
            {
                indexPos = (int)((2.24f - daoTails[i].transform.position.y) / 0.85f); 
                if (indexPos != daoTails[i].GetComponent<DaoTailControl>().NumberTail)
                {
                    uIControl.BuildingFailed();
                    return;
                }
            }
        }
        else
        {            
            return;
        }
        uIControl.ViewPageButtons();
    }
}
