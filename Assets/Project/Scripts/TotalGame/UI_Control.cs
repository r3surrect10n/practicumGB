using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private Text hintText;
    [SerializeField] private float hintDelay = 5f;

    private float timer = 5f;
    bool isHintView = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = hintDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHintView)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                hintPanel.SetActive(false);
                isHintView = false;
            }
        }
        
    }

    public void ViewHint(string hint)
    {
        hintText.text = hint;
        hintPanel.SetActive(true);
        isHintView = true;
        timer = hintDelay;
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
