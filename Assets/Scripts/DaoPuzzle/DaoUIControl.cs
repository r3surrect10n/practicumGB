using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DaoUIControl : MonoBehaviour
{
    [SerializeField] private Button btnRestart;
    [SerializeField] private Button btnPage1;
    [SerializeField] private Button btnPage2;
    [SerializeField] private Button btnHint;
    [SerializeField] private Text txtHint;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject board;
    [SerializeField] private Image foneHintBtn;

    private float timer = 0.2f;
    private bool isFoneViewing = false;
    private bool isFoneView = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnHint.gameObject.SetActive(false);
        btnPage1.gameObject.SetActive(false);
        btnPage2.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (isFoneViewing)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = 0.33f;
                isFoneView = !isFoneView;
                foneHintBtn.gameObject.SetActive(isFoneView);
            }
        }
    }

    public void ViewPageButtons()
    {
        btnPage1.gameObject.SetActive(true);
        btnPage2.gameObject.SetActive(true);
        book.SetActive(true);
    }

    public void ViewHint(string hint)
    {
        txtHint.text = hint;
        btnHint.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickQuit()
    {   //  загрузить сцену с которой сюда пришли
        // SceneManager.LoadScene("MainScene");
    }

    public void OnClickHint()
    {   //  сохранить в блокнот полученную фразу
        //  GameManager.Instance.notebook.Add(txtHint.text);
        helpPanel.gameObject.SetActive(true);
        btnHint.interactable = false;
        btnPage1.interactable = false;
        btnPage2.interactable = false;
        btnRestart.interactable = false;
        isFoneViewing = false;
        foneHintBtn.gameObject.SetActive(false);
    }

    public void OnClickPageBtn(int num)
    {
        isFoneViewing = true;
        Vector3 position = board.transform.position;
        if (num == 0)
        {
            ViewHint("Какая-то абракадабра");
            position.x = -2.13f;
            board.transform.position = position;
        }
        if (num == 1)
        {
            position.x = 2.13f;
            board.transform.position = position;
            ViewHint("Текст правильной подсказки");
        }
    }

    public void BuildingFailed()
    {
        helpPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Ключ-картинка собраны неверно! Попробуйте ещё раз!";
        helpPanel.gameObject.SetActive(true);
        Invoke("Restart", 2f);
    }
}
