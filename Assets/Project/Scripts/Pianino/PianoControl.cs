using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PianoControl : MonoBehaviour
{
    [SerializeField] private PianoKey[] pianoKeys;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private int comboMusic;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Text txtInfo;
    [SerializeField] private Button btnRestart;

    private BoxCollider _boxCollider;
    private AudioSource music;
    private int _musCombo = 0;
    private int _countPressed = 0;
    private bool isReplay;
    private float timer = 1f;
    private int[] divZn = { 10000, 1000, 100, 10, 1 };

    private void Awake()
    {
        music = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CursorVisible.CursorEnable();

        PianoControl pc = GetComponent<PianoControl>();
        for (int i = 0; i < pianoKeys.Length; i++)
        {
            pianoKeys[i].SetPianoControl(pc);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isReplay)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = 1f;
                int num = (comboMusic / divZn[_countPressed]) % 10;
                music.clip = clips[num - 1];
                music.Play();
                _countPressed++;
                if (_countPressed == 5)
                {
                    isReplay = false;
                    Restart();
                }
            }
        }
    }

    public void PressedKey(int num)
    {
        if (isReplay) return;
        _countPressed++;
        _musCombo *= 10;
        _musCombo += num + 1;
        music.clip = clips[num];
        music.Play();
        if (_countPressed == 5)
        {
            if (_musCombo == comboMusic)
            {
                txtInfo.text = "Мелодия угадана !";
                btnRestart.gameObject.SetActive(false);
            }
            else
            {
                txtInfo.text = "Вы ошиблись !";
                btnRestart.gameObject.SetActive(true);
            }
            _boxCollider.enabled = true;
            infoPanel.gameObject.SetActive(true);
        }
    }

    public void SetMusicCombo(int combo)
    {
        comboMusic = combo;
        _countPressed = 0;
        _musCombo = 0;
    }

    public void OnClickReplay()
    {
        isReplay = true;
        _countPressed = 0;
    }

    public void OnClickQuit()
    {
        SceneManager.LoadScene("Building");
    }

    public void Restart()
    {
        _countPressed = 0;
        _musCombo = 0;
        infoPanel.gameObject.SetActive(false);
        _boxCollider.enabled = false;
    }
}
