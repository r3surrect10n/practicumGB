using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RadioControl : MonoBehaviour
{
    [SerializeField] private AudioSource radioSonds;
    [SerializeField] private AudioClip[] music;
    [SerializeField] private AudioClip[] noise;
    [SerializeField] private AudioClip blago;
    [SerializeField] private Text txtDisplay;

    private string[] frequencys = { "102.6", "105.4", "88.7", "90.4", "103.7", "106.8"};
    private string currentFrequency = "0";
    private bool isSet = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        txtDisplay.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNumberButton(int num)
    {
        if (currentFrequency == "0") currentFrequency = "";
        if (isSet)
        {
            currentFrequency = "";
            isSet = false;
        }
        currentFrequency += num.ToString();
        txtDisplay.text = currentFrequency;
    }

    public void OnClickPoint()
    {
        currentFrequency += ".";
        txtDisplay.text = currentFrequency;
    }

    public void OnClickSet()
    {
        isSet = true;
        int i, index = -1;
        for (i = 0; i < frequencys.Length; i++)
        {
            if (frequencys[i] == currentFrequency)
            {
                index = i;
                break;
            }
        }
        switch(index)
        {
            case 0: radioSonds.clip = blago; break;
            case 1: radioSonds.clip = music[0]; break;
            case 2: radioSonds.clip = music[1]; break;
            case 3: radioSonds.clip = music[2]; break;
            case 4: radioSonds.clip = music[4]; break;
            case 5: radioSonds.clip = music[3]; break;
            default: radioSonds.clip = noise[Random.Range(0, noise.Length)];break;
        }
        radioSonds.Play();
    }

    public void OnClickQuit()
    {
        radioSonds.Stop();
        SceneManager.LoadScene("Building");
    }
}
