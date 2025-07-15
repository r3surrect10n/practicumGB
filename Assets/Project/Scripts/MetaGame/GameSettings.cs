using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private PlayerLook _playerMouseController;

    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sensivitySlider;

    [SerializeField] private Text _soundText;
    [SerializeField] private Text _musicText;
    [SerializeField] private Text _sensivityText;

    public void OnSceneLoad()
    {
        
    }

    public void OnSaveClick()
    {
        PlayerPrefs.SetFloat("sound", _soundSlider.value);
        PlayerPrefs.SetFloat("music", _musicSlider.value);
        PlayerPrefs.SetFloat("sensivity", _sensivitySlider.value);
        PlayerPrefs.Save();
    }
}
