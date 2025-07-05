using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterQuestObject : MonoBehaviour, IInteractable
{
    [SerializeField] private LocationControl locationControl;
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private bool isTakeItem = false;
    [SerializeField] private string nameForInventory = "";
    [SerializeField] private bool isGameItem = false;
    [SerializeField] private string nameSceneMiniGame = "";
    [SerializeField] private string hintText = "";
    [SerializeField] private string btnOkText = "";
    [SerializeField] private string ids_note;
    [SerializeField] private string roomNote;

    public bool IsGameItem { get => isGameItem; set => isGameItem = value; }
    public string HintText { get => hintText; set => hintText = value; }
    public string BtnOkText { get => btnOkText; set => btnOkText = value; }

    //public bool IsGameItem { get =; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndInteract()
    {
        CursorVisible.CursorDisable();
        if (hintPanel != null)
        {
            if (BtnOkText != "")
            {
                Button btnHint = hintPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
                btnHint.onClick.RemoveListener(OnBtnOKClick);
            }
            hintPanel.SetActive(false);
        }
        if (roomNote != "") GameManager.Instance.currentPlayer.notebookList.AddNote(new NotebookItem(ids_note, roomNote));
        PerformActions();
    }

    public void Interact()
    {
        CursorVisible.CursorEnable();
        if (hintPanel != null)
        {
            Text txtHint = hintPanel.transform.GetChild(0).gameObject.GetComponent<Text>();
            Button btnHint = hintPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
            Text btnText = btnHint.transform.GetChild(0).gameObject.GetComponent<Text>();
            if (BtnOkText != "")
            {
                btnText.text = BtnOkText;
                btnHint.gameObject.SetActive(true);
                btnHint.onClick.AddListener(OnBtnOKClick);
            }
            else
            {
                btnHint.gameObject.SetActive(false);
            }
            txtHint.text = HintText;
            hintPanel.SetActive(true);
        }
    }

    public void OnBtnOKClick()
    {
        if (hintPanel != null)
        {
            print("OnBtnOKClick сработал !");
            if (BtnOkText != "")
            {
                Button btnHint = hintPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
                btnHint.onClick.RemoveListener(OnBtnOKClick);
            }
            hintPanel.SetActive(false);
            PerformActions();
        }
    }

    private void PerformActions()
    {
        CursorVisible.CursorDisable();
        if (isTakeItem)
        {
            if (nameForInventory != "")
            {
                Inventory inventory = GameManager.Instance.currentPlayer.inventory;
                Sprite spr = SpriteSet.Instance.GetSprite(nameForInventory);
                InventoryItem item = new InventoryItem(inventory.CountItem, nameForInventory, spr);
                //print($"{item.ToString()}  sprite=<{spr}>");
                inventory.AddItem(item);
            }
            Destroy(gameObject);
        }
        if (IsGameItem)
        {
            if (nameSceneMiniGame != "")
            {
                if (locationControl != null)
                {
                    locationControl.SavePositionAndRotation();
                }
                SceneManager.LoadScene(nameSceneMiniGame);
            }
        }

    }
}
