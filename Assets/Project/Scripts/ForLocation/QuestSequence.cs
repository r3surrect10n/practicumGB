using System.Text;
using UnityEngine;

public class QuestSequence : MonoBehaviour
{
    [SerializeField] private LevelControl levelControl;
    [SerializeField] private QuestObject[] quests;

    [SerializeField] private string nameSequence;

    private bool isStart = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (quests.Length > 0)
        {
            for (int i = 0; i < quests.Length; i++)
            {
                quests[i].SetLevelControl(levelControl);
            }
            //quests[0].ChangeStatus(QuestStatus.isAccessible);
        }


    }

    public void SetStartQuest()
    {
        if (quests.Length > 0)
        {
            quests[0].SetLevelControl(levelControl);
            quests[0].ChangeStatus(QuestStatus.isAccessible);
            isStart = true;
        }
    }

    public string ToCsvString(char sep='#')
    {
        StringBuilder sb = new StringBuilder($"{isStart}{sep}{nameSequence}{sep}");
        for (int i = 0; i < quests.Length; i++)
        {
            sb.Append($"{quests[i].Status}{sep}");
        }
        return sb.ToString();
    }

    public void SetQuestsStatus(string csv, char sep = '#')
    {
        string[] ar = csv.Split(sep);
        print($"ar.count={ar.Length}  csv=<{csv}>");
        if (ar.Length > 1)
        {
            if (bool.TryParse(ar[0], out isStart) == false)
            {
                isStart = false;
                SetStartQuest();
            } 
            for (int i = 0; i < quests.Length; i++)
            {
                quests[i].SetLevelControl(levelControl);
                QuestStatus status = QuestStatus.isClosed;
                switch (ar[i + 2])
                {
                    case "isClosed": status = QuestStatus.isClosed; break;
                    case "isAccessible": status = QuestStatus.isAccessible; break;
                    case "isSuccess": status = QuestStatus.isSuccess; break;
                    case "isFailed": status = QuestStatus.isFailed; break;
                }
                if (quests[i].MiniGameScene != "")
                {
                    status = GameManager.Instance.currentPlayer.listMiniGames.GetMiniGamesStatus(quests[i].MiniGameScene);
                    quests[i].ChangeStatus(status);
                }
                else quests[i].ChangeStatus(status, false);
                print($"znFromCsv={ar[i+2]}  status={status}");
            }
        }
        else
        {
            SetStartQuest();
            return;
        }
    }
}
