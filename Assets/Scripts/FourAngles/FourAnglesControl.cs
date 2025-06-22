using UnityEngine;
using System.Collections.Generic;

public class FourAnglesControl : MonoBehaviour
{
    [SerializeField] private GameObject[] chipPrefabs;
    [SerializeField] private FourAngkesUIControl uiControl;

    private int[] chipTabl;
    private GameObject[] chips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateChips();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateChips()
    {
        int i, numPos, px, py;
        float y = 0.018f;
        chipTabl = new int[25];
        chips = new GameObject[16];
        List<int> numbers = new List<int>();
        Vector3 pos = Vector3.zero;

        for (i = 0; i < 25; i++)
        {
            chipTabl[i] = -1;
        }
        chipTabl[7] = 0; chipTabl[11] = 0; chipTabl[13] = 0; chipTabl[17] = 0;
        for (i = 0; i < 16; i++)
        {
            numbers.Add(i);
        }
        for (i = 0; i < 16; i++)
        {
            numPos = numbers[Random.Range(0, numbers.Count)];
            px = numPos / 4;
            if (px > 1) px++;
            py = numPos % 4;
            if (py > 1) py++;
            chipTabl[5 * py + px] = (i / 4) + 1;
            pos.x = -0.576f + 0.288f * px;
            pos.y = y;
            pos.z = -0.576f + 0.288f * py;
            GameObject chip = Instantiate(chipPrefabs[i / 4], pos, Quaternion.identity);
            chip.GetComponent<ChipControl>().SetBoardControl(GetComponent<FourAnglesControl>());
            chips[i] = chip;
            //print($"i={i} numPos={numPos}  px={px} py={py}  pos={pos}");
            numbers.Remove(numPos);
        }
    }

    public int[] GetChipsTabl()
    {
        return chipTabl;
    }

    public void MoveChip(int from, int to)
    {
        chipTabl[to] = chipTabl[from];
        chipTabl[from] = 0;
        if (TestWin())
        {
            uiControl.HintView("Очередная подсказка");
        }
    }

    private bool TestWin()
    {
        int i;
        int[] dops = { 0, 3, 15, 18};
        for (i = 0; i < 4; i++)
        {
            if (TestAngle(dops[i]) == false) return false;
        }
        return true;
    }

    private bool TestAngle(int dop)
    {
        if ((chipTabl[dop] == chipTabl[dop + 1]) && (chipTabl[dop + 5] == chipTabl[dop + 6]) && (chipTabl[dop + 5] == chipTabl[dop + 1])) return true;
        return false;
    }
}
