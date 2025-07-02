using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private string miniGameScene;
    [SerializeField] private string hintBefore;
    [SerializeField] private string hintAfter;
    [SerializeField] private string hintFailed;
    [SerializeField] private string toNotebook;
    [SerializeField] private string nameSpriteToNotebook;
    [SerializeField] private string nameForInventory;
    [SerializeField] private string btnOkText;
    [SerializeField] private GameObject[] objectsAfter;
    [SerializeField] private GameObject hintPanel;

    private QuestStatus status = QuestStatus.isClosed;
    private LevelControl levelControl = null;
    public QuestStatus Status { get => status; }
    public string MiniGameScene { get => miniGameScene; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (effect != null) effect.Pause();
        if (objectsAfter != null)
        {
            foreach(GameObject go in objectsAfter) go.SetActive(false);
        }
    }

    public void SetLevelControl(LevelControl lc)
    {
        levelControl = lc;
    }

    public void ChangeStatus(QuestStatus newStatus, bool isSave = true)
    {
        if (newStatus == QuestStatus.isClosed) return;
        status = newStatus;
        if (isSave) levelControl.SaveQuestProgress();
        if (status == QuestStatus.isAccessible)
        {
            if (effect != null) effect.Play();
            print($"ChangeStatus effect.Play() status={status} for QO=>{gameObject.name}");
        }
        else
        {
            if (effect != null) effect.Pause();
            if (status == QuestStatus.isSuccess && objectsAfter != null)
            {
                foreach (GameObject go in objectsAfter)
                {
                    go.SetActive(true);
                    QuestObject quest = go.GetComponent<QuestObject>();
                    print($"QuestObject => {quest}");
                    if (quest != null)
                    {
                        quest.SetLevelControl(levelControl);
                        quest.ChangeStatus(QuestStatus.isAccessible, isSave);
                    }
                }
                if (toNotebook != "")
                {
                    Sprite sprite = null;
                    if (nameSpriteToNotebook != "")
                    {
                        sprite = SpriteSet.Instance.GetSprite(nameSpriteToNotebook);
                    }
                    GameManager.Instance.currentPlayer.listNoteMessages.AddMessage(new NotepadMessage(toNotebook, nameSpriteToNotebook, sprite));
                }
                gameObject.SetActive(false);
            }
        }
    }

    public void OnClickOk()
    {
        Button btnOk = hintPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
        if (btnOk != null) btnOk.onClick.RemoveListener(OnClickOk);
        if (nameForInventory != "")
        {
            Inventory inventory = GameManager.Instance.currentPlayer.inventory;
            Sprite spr = SpriteSet.Instance.GetSprite(nameForInventory);
            InventoryItem item = new InventoryItem(inventory.CountItem, nameForInventory, spr);
            //print($"{item.ToString()}  sprite=<{spr}>");
            inventory.AddItem(item);
        }
        if (miniGameScene != "")
        {
            if (levelControl != null)
            {
                levelControl.SavePositionAndRotation();
                levelControl.SaveQuestProgress();
            }
            SceneManager.LoadScene(miniGameScene);
            QuestStatus miniGameStatus = GameManager.Instance.currentPlayer.listMiniGames.GetMiniGamesStatus(miniGameScene);
            print($"OnClickOk status=<{miniGameStatus}>");
            if (miniGameStatus != QuestStatus.isClosed) ChangeStatus(GameManager.Instance.currentPlayer.listMiniGames.GetMiniGamesStatus(miniGameScene));
        }
        else ChangeStatus(QuestStatus.isSuccess);
        hintPanel.gameObject.SetActive(false);
    }

    public string GetHint()
    {
        print($"GetHint status={status} questName={gameObject.name}");
        if (status == QuestStatus.isAccessible) return hintBefore;
        if (status == QuestStatus.isSuccess) return hintAfter;
        if (status == QuestStatus.isFailed) return hintFailed;
        return "";
    }

    public string GetStrToNotebook()
    {
        return toNotebook;
    }

    private void OnTriggerEnter(Collider other)
    {
        print($"other.name={other.name}");
        if (other.CompareTag("Player"))
        {
            if (hintPanel != null)
            {
                string hint = GetHint();
                print($"hint= << {hint} >>");
                if (hint != "")
                {
                    Button btnOk = hintPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
                    if (btnOk != null)
                    {
                        if (btnOkText != "")
                        {
                            btnOk.transform.GetChild(0).gameObject.GetComponent<Text>().text = btnOkText;
                            btnOk.onClick.AddListener(OnClickOk);
                            btnOk.gameObject.SetActive(true);
                            //print($"onClick => {btnOk.onClick}");
                        }
                        else btnOk.gameObject.SetActive(false);
                    }
                    hintPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = hint;
                    hintPanel.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hintPanel != null)
            {
                hintPanel.SetActive(false);
                Button btnOk = hintPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
                if (btnOk != null)
                {
                    btnOk.onClick.RemoveListener(OnClickOk);
                    btnOk.gameObject.SetActive(true);
                }
            }
        }
    }
}

public enum QuestStatus { isClosed, isAccessible, isSuccess, isFailed};
