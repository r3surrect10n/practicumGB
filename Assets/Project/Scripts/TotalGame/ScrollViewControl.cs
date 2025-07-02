using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewControl : MonoBehaviour
{
    public GameObject messagePrefab;   // Шаблон для текстовых сообщений
    public GameObject imagePrefab;     // Шаблон для изображений с подписями
    public Transform contentContainer; // Контейнер контента в Scroll View

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GenerateContent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateContent()
    {
        for(int j = contentContainer.childCount; j > 0; j--)
        {
            Transform child = contentContainer.GetChild(j - 1);
            Destroy(child.gameObject);
        }
        for (int i = 0; i < GameManager.Instance.currentPlayer.listNoteMessages.CountMessage; i++)
        {
            NotepadMessage msg = GameManager.Instance.currentPlayer.listNoteMessages.GetNoteMessage(i);
            //GameObject go = Instantiate(messagePrefab, contentContainer);
            //Text captionTxt = go.GetComponentInChildren<Text>();
            //captionTxt.text = msg.Message; // Подпись

            if (msg.NameSprite == "")
            {   // Генерация текстовых сообщений
                GameObject go = Instantiate(messagePrefab, contentContainer);
                Text captionTxt = go.GetComponentInChildren<Text>();
                captionTxt.text = msg.Message; // Подпись
            }
            else
            {   // Генерация изображений с подписями
                GameObject go = Instantiate(imagePrefab, contentContainer);
                Image img = go.transform.GetChild(1).gameObject.GetComponent<Image>();
                Text captionTxt = go.GetComponentInChildren<Text>();
                img.sprite = msg.Sprite; // Спрайт изображения
                captionTxt.text = msg.Message; // Подпись
            }

            print($"Добавлено сообщение: {msg.Message}");



            //if (msg.NameSprite == "")
            //{   // Генерация текстовых сообщений
            //    if (img != null) img.enabled = false;
            //}
            //else
            //{   // Генерация изображений с подписями
            //    img.sprite = msg.Sprite; // Спрайт изображения
            //}
            //captionTxt.text = msg.Message; // Подпись
        }
    }

    public void ViewContent()
    {
        GenerateContent();
    }
}

[Serializable]
public class NotepadMessage
{
    private Sprite sprite;
    private string message;
    private string nameSprite;

    public Sprite Sprite { get => sprite; }
    public string Message { get => message; }
    public string NameSprite { get => nameSprite; }

    public NotepadMessage() { }
    public NotepadMessage(string msg, string nmSpr = "", Sprite spr = null)
    {
        nameSprite = nmSpr;
        sprite = spr;
        message = msg;
    }

    public NotepadMessage(string csv, char sep = '=')
    {
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (ar.Length >= 2)
        {
            message = ar[0];
            nameSprite = ar[1];
        }
    }

    public void SetSprite(Sprite spr)
    {
        sprite = spr;
    }

    public string ToCsvString(char sep = '=')
    {
        return $"{message}{sep}{nameSprite}{sep}";
    }
}

[Serializable]
public class ListNoteMessages
{
    private List<NotepadMessage> messages = new List<NotepadMessage>();
    public int CountMessage { get => messages.Count; }

    public ListNoteMessages() { }
    public ListNoteMessages(string csv, char sep = '#')
    {
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ar.Length; i++) messages.Add(new NotepadMessage(ar[i], '='));
    }

    public void AddMessage(NotepadMessage msg)
    {
        for (int i = 0; i < CountMessage; i++)
        {
            if (messages[i].Message == msg.Message) return;
        }
        messages.Add(new NotepadMessage(msg.Message, msg.NameSprite, msg.Sprite));
    }

    public string GetNameSprite(int index)
    {
        if (index >= 0 && index < CountMessage) return messages[index].NameSprite;
        return "";
    }

    public NotepadMessage GetNoteMessage(int index)
    {
        if (index >= 0 && index < CountMessage) return messages[index];
        return null;
    }

    public string ToCsvString(char sep = '#')
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < CountMessage; i++)
        {
            sb.Append($"{messages[i].ToCsvString()}{sep}");
        }
        return sb.ToString();
    }
}
