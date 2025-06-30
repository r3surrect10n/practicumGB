using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private string miniGameScene;
    [SerializeField] private string hintBefore;
    [SerializeField] private string hintAfter;
    [SerializeField] private string hintFailed;
    [SerializeField] private string toNotebook;
    [SerializeField] private string nameForInventory;
    [SerializeField] private string btnOkText;
    [SerializeField] private GameObject[] objectsAfter;
    [SerializeField] private GameObject hintPanel;

    private QuestStatus status = QuestStatus.isClosed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (effect != null) effect.Pause();
        if (objectsAfter != null)
        {
            foreach(GameObject go in objectsAfter) go.SetActive(false);
        }
    }

    public void ChangeStatus(QuestStatus newStatus)
    {
        status = newStatus;
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
                    if (quest != null) quest.ChangeStatus(QuestStatus.isAccessible);
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
            InventoryItem item = new InventoryItem(inventory.CountItem, nameForInventory, SpriteSet.Instance.GetSprite(nameForInventory));
            print(item.ToString());
            inventory.AddItem(item);
        }
        ChangeStatus(QuestStatus.isSuccess);
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
                        btnOk.transform.GetChild(0).gameObject.GetComponent<Text>().text = btnOkText;
                        btnOk.onClick.AddListener(OnClickOk);
                        print($"onClick => {btnOk.onClick}");
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
            }
        }
    }
}

public enum QuestStatus { isClosed, isAccessible, isSuccess, isFailed};
