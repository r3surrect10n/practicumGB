using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DotGameControl : MonoBehaviour
{
    [SerializeField] private Button[] dotButtons1;
    [SerializeField] private Image[] lines1;
    [SerializeField] private Button[] dotButtons2;
    [SerializeField] private Image[] lines2;
    [SerializeField] private GameObject line1;
    [SerializeField] private GameObject line2;
    [SerializeField] private GameObject dots1;
    [SerializeField] private GameObject dots2;
    [SerializeField] private Image bigBee;
    [SerializeField] private Button code;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Color colorSelectDots1;
    [SerializeField] private Color colorSelectDots2;

    private int currentDot1 = -1, currentDot2 = -1;
    private List<int> selectPoints1 = new List<int>();
    private List<int> selectPoints2 = new List<int>();
    private Color colorDots1, colorDots2;

    private QuestStatus status = QuestStatus.isFailed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        infoPanel.SetActive(false);
        bigBee.gameObject.SetActive(false);
        int i;
        for (i = 0; i < lines1.Length; i++) lines1[i].gameObject.SetActive(false);
        for (i = 0; i < lines2.Length; i++) lines2[i].gameObject.SetActive(false);
        if (dotButtons1.Length > 0) colorDots1 = dotButtons1[0].GetComponent<Image>().color;
        if (dotButtons2.Length > 0) colorDots2 = dotButtons2[0].GetComponent<Image>().color;
        int numberCode = Random.Range(1000, 10000);
        SetCode(numberCode.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDotClick(int numPoint)
    {
        int number = numPoint % 100;
        int numLine = numPoint / 100;
        int i;
        if (numLine == 1)
        {
            if (currentDot1 == -1)
            {
                if (number == 1)
                {
                    currentDot1 = number;
                    selectPoints1.Add(numPoint);
                    dotButtons1[0].GetComponent<Image>().color = colorSelectDots1;
                }
            }
            else
            {
                if (currentDot1 + 1 == number)
                {
                    currentDot1 = number;
                    selectPoints1.Add(numPoint);
                    dotButtons1[number - 1].GetComponent<Image>().color = colorSelectDots1;
                    lines1[number - 2].gameObject.SetActive(true);
                }
                else if (number == 1 && currentDot1 == dotButtons1.Length)
                {
                    lines1[currentDot1 - 1].gameObject.SetActive(true);
                    for (i = 0; i < dotButtons1.Length; i++) dotButtons1[i].interactable = false;
                }
                else
                {
                    currentDot1 = ResetLines(dotButtons1, lines1, colorDots1);
                    selectPoints1.Clear();
                }
            }
        }
        if (numLine == 2)
        {
            if (currentDot2 == -1)
            {
                if (number == 1)
                {
                    currentDot2 = number;
                    selectPoints2.Add(numPoint);
                    dotButtons2[0].GetComponent<Image>().color = colorSelectDots2;
                }
            }
            else
            {
                if (currentDot2 + 1 == number)
                {
                    currentDot2 = number;
                    selectPoints2.Add(numPoint);
                    dotButtons2[number - 1].GetComponent<Image>().color = colorSelectDots2;
                    lines2[number - 2].gameObject.SetActive(true);
                }
                else if (number == 1 && currentDot2 == dotButtons2.Length)
                {
                    lines2[currentDot2 - 1].gameObject.SetActive(true);
                    for (i = 0; i < dotButtons2.Length; i++) dotButtons2[i].interactable = false;
                }
                else
                {
                    currentDot2 = ResetLines(dotButtons2, lines2, colorDots2);
                    selectPoints2.Clear();
                }
            }
        }
        if (TestAllPoints())
        {
            line1.SetActive(false);
            line2.SetActive(false);
            dots1.SetActive(false);
            dots2.SetActive(false);
            bigBee.gameObject.SetActive(true);
            status = QuestStatus.isSuccess;
        }
    }

    private bool TestAllPoints()
    {
        if (selectPoints1.Count == dotButtons1.Length && selectPoints2.Count == dotButtons2.Length) return true;
        return false;
    }

    private int ResetLines(Button[] arrBtn, Image[] arrLn, Color oldCol)
    {
        int i;
        for (i = 0; i < arrLn.Length; i++) arrLn[i].gameObject.SetActive(false);
        for (i = 0; i < arrBtn.Length; i++)
        {
            arrBtn[i].GetComponent<Image>().color = oldCol;
        }
        return -1;
    }

    public void ViewInfoPanel()
    {
        //  Здесь можно отправить код от сейфа(замка двери) с кнопки в блокнот 
        infoPanel.SetActive(true);
    }

    public void SetCode(string codeNumber)
    {
        code.transform.GetChild(0).gameObject.GetComponent<Text>().text = codeNumber;
    }

    public void OnClickQuit()
    {
        GameManager.Instance.currentPlayer.listMiniGames.AddMiniGame(new MiniGameStatus("Alexander", status));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("BuildingOld");
    }
}
