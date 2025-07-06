using UnityEngine;
using UnityEngine.UI;

public class RadioReceiverControl : MonoBehaviour
{
    [SerializeField] private Text txtFrequence;
    [SerializeField] private Text txtNameFM;
    [SerializeField] private AudioClip[] soundsRadio;
    [SerializeField] private AudioClip[] soundsNouse;

    private int _baseUnits = 86;
    private int _maxDeltaUnits = 22;
    private int _znUnits = 0;
    private int _znHundredths = 0;

    private AudioSource soundRadio;

    private float timer = 1f;
    private bool isSoundOn = false;

    private string[] freqs = { "087.90", "089.50", "091.30", "093.70", "095.50", "097.70", "099.90", "101.10", "103.30", "106.70"};
    private string[] nameRadio = { "����� ������", "���� �������", "���� ������", "�������� ������", "���� � �����", "�������� �����", "����������", "����� ����", "����� �����", "������� 77.7" };
    /*"87.9 FM � ����� ������
89.5 FM � ����� ���� �������
91.3 FM � ���� ������ FM
93.7 FM � �������� ������
95.5 FM � ���� � �����
97.7 FM � �������� �����
99.9 FM � ���������� FM
101.1 FM � ����� ���� �����
103.3 FM � ����� �����
106.7 FM � ������� 77.7"*/

    private void Awake()
    {
        soundRadio = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        txtFrequence.text = "";
        txtNameFM.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            timer = 1f;
            if (gameObject.layer == LayerMask.NameToLayer("Interaction"))
            {
                soundRadio.Stop();
                isSoundOn = false;
            }
            if (gameObject.layer == LayerMask.NameToLayer("Outline"))
            {
                string frq = $"{(_znUnits + _baseUnits):000}.{_znHundredths:00}";
                if (frq == "099.90" && isSoundOn == false)
                {
                    soundRadio.clip = soundsRadio[soundsRadio.Length - 1];
                    soundRadio.Play();
                    isSoundOn = true;
                }
            }
        }
    }

    public void OnChangeBtnClick(int btn)
    {
        switch(btn)
        {
            case 1: _znUnits++; _znUnits %= _maxDeltaUnits; break;
            case 2: _znUnits += _maxDeltaUnits - 1; _znUnits %= _maxDeltaUnits; break;
            case 3: 
                _znHundredths += 10;
                if (_znHundredths == 100)
                {
                    _znUnits++;
                    _znUnits %= _maxDeltaUnits;
                }
                _znHundredths %= 100; break;
            case 4:
                _znHundredths += 90;
                if (_znHundredths == 90)
                {
                    _znUnits += _maxDeltaUnits - 1;
                    _znUnits %= _maxDeltaUnits;
                }
                _znHundredths %= 100; break;
        }
        string frq = $"{(_znUnits + _baseUnits):000}.{_znHundredths:00}";
        txtFrequence.text = "FM " + frq;
        ViewNameRadio(frq);
    }

    private void ViewNameRadio(string frq)
    {
        bool isNouse = true;
        for (int i = 0; i < 10; i++)
        {
            if (frq == freqs[i])
            {
                isNouse = false;
                txtNameFM.text = nameRadio[i];
                if (soundRadio != null)
                {
                    soundRadio.clip = soundsRadio[i % (soundsRadio.Length - 1)];
                    soundRadio.Play();
                }
                break;
            }
        }
        if (frq == "099.90")
        {
            soundRadio.clip = soundsRadio[soundsRadio.Length - 1];
            soundRadio.Play();
        }
        if (isNouse)
        {
            txtNameFM.text = "-------";
            if (soundRadio != null)
            {
                soundRadio.clip = soundsNouse[Random.Range(0, soundsNouse.Length)];
                soundRadio.Play();
            }
        }
    }
}
