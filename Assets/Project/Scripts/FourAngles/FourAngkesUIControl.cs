using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FourAngkesUIControl : MonoBehaviour
{
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Image imgFone;
    [SerializeField] private Text txtHint;

    private float timer = 0.5f;
    private bool isBlink = false;
    private bool isFoneView = false;
    private QuestStatus status = QuestStatus.isFailed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlink)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = 0.33f;
                isFoneView = !isFoneView;
                imgFone.gameObject.SetActive(isFoneView);
            }
        }        
    }

    public void HintView(string hint)
    {
        txtHint.text = hint;
        hintPanel.SetActive(true);
        isBlink = true;
    }

    public void InfoView()
    {
        isBlink = false;
        isFoneView = false;
        imgFone.gameObject.SetActive(false);
        hintPanel.SetActive(false);
        infoPanel.SetActive(true);
    }

    public void SetStatus(QuestStatus zn)
    {
        status = zn;
    }

    public void OnQuitClick()
    {
        GameManager.Instance.currentPlayer.listMiniGames.AddMiniGame(new MiniGameStatus("FourAnglesScene", status));

        SceneManager.LoadScene("Building");
    }
}
