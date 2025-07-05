using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UI_Notebook : MonoBehaviour
{
    [SerializeField] private Button[] navBtn;
    [SerializeField] private Text txtLeft;
    [SerializeField] private Text txtRight;

    public static string[] NameRooms = { "Book", "Control", "Laboratory", "Bed", "Secret", "Headquarters", "Warehouse" };
    //[SerializeField] private NotebookControl notebookControl;
    //[SerializeField] 
    private Color selectBtn = Color.blue;

    private int currentPage = 0;
    private Color oldColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oldColor = navBtn[0].transform.GetChild(0).gameObject.GetComponent<Text>().color;
        navBtn[0].transform.GetChild(0).gameObject.GetComponent<Text>().color = selectBtn;
        ViewText();
    }

    public void OnNavBtnClick(int num)
    {
        if (num != currentPage)
        {
            navBtn[currentPage].transform.GetChild(0).gameObject.GetComponent<Text>().color = oldColor;
            oldColor = navBtn[num].transform.GetChild(0).gameObject.GetComponent<Text>().color;
            navBtn[num].transform.GetChild(0).gameObject.GetComponent<Text>().color = selectBtn;
            currentPage = num;
            ViewText();
        }
    }

    private void ViewText()
    {
        List<NotebookItem> items = GameManager.Instance.currentPlayer.notebookList.GetRoomItemList(UI_Notebook.NameRooms[currentPage]);
        //print($"count={items.Count} for room={UI_Notebook.NameRooms[currentPage]}");
        string lang = GameManager.Instance.currentLanguale;
        StringBuilder sb = new StringBuilder();
        foreach(NotebookItem item in items)
        {
            string note = NoteSet.Instance.GetNote(item.IDS, lang);
            sb.Append($"{note}{"\n********************************\n\n"}");
        }
        string leftText = sb.ToString();
        
        string[] ar = leftText.Split("\n");
        int countLines = ar.Length;
        foreach(string s in ar)
        {
            countLines += s.Length / 35;
        }
        if (countLines < 34)
        {
            txtLeft.text = sb.ToString();
            txtRight.text = "";
            //txtRight.text = $"CountString={ar.Length}  countLines={countLines}";
        }
        else
        {
            StringBuilder lsb = new StringBuilder();
            StringBuilder rsb = new StringBuilder();
            for (int i = 0; i < ar.Length; i++)
            {
                if (i < 34) lsb.Append($"{ar[i]}{"\n"}");
                else rsb.Append($"{ar[i]}{"\n"}");
            }
            txtLeft.text = lsb.ToString();
            txtRight.text = rsb.ToString();
        }

    }
}

[Serializable]
public class NotebookList
{
    private List<NotebookItem> notes = new List<NotebookItem>();

    public int CountNotes { get => notes.Count; }

    public NotebookList() { }
    public NotebookList(string csv, char sep = '#')
    {
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        foreach(string item in ar)
        {
            notes.Add(new NotebookItem(item));
        }
    }

    public string ToCsvString(char sep = '#')
    {
        StringBuilder sb = new StringBuilder();
        foreach (NotebookItem item in notes) sb.Append($"{item.ToCsvString()}{sep}");
        return sb.ToString();
    }

    public List<NotebookItem> GetRoomItemList(string nameRoom)
    {
        List<NotebookItem> roomList = new List<NotebookItem>();
        foreach(NotebookItem item in notes)
        {
            if (item.NameRoom == nameRoom)
            {
                roomList.Add(item);
            }
        }
        return roomList;
    }

    public void AddNote(NotebookItem item)
    {
        foreach(NotebookItem note in notes)
        {
            if (note.IDS == item.IDS && note.NameRoom == item.NameRoom) return;
        }
        notes.Add(item);
    }
}

[Serializable]
public class NotebookItem
{
    private string nameRoom;
    private string ids;

    public string NameRoom { get => nameRoom; }
    public string IDS { get => ids; }

    public NotebookItem() { }
    public NotebookItem(string id, string nm)
    {
        nameRoom = nm;
        ids = id;
    }
    public NotebookItem(string csv, char sep = '=')
    {
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (ar.Length >= 2)
        {
            nameRoom = ar[0];
            ids = ar[1];
        }
    }
    public string ToCsvString(char sep = '=')
    {
        return $"{nameRoom}{sep}{ids}{sep}";
    }
}
