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
            if (btnOkText != "")
            {
                Button btnHint = hintPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
                btnHint.onClick.RemoveListener(OnBtnOKClick);
            }
            hintPanel.SetActive(false);
        }
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
            if (btnOkText != "")
            {
                btnText.text = btnOkText;
                btnHint.gameObject.SetActive(true);
                btnHint.onClick.AddListener(OnBtnOKClick);
            }
            else
            {
                btnHint.gameObject.SetActive(false);
            }
            txtHint.text = hintText;
            hintPanel.SetActive(true);
        }
    }

    public void OnBtnOKClick()
    {
        if (hintPanel != null)
        {
            print("OnBtnOKClick сработал !");
            if (btnOkText != "")
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
        if (isGameItem)
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
