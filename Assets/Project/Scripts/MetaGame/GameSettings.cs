using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private PlayerLook _playerMouseController;

    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sensivitySlider;

    [SerializeField] private Text _soundText;
    [SerializeField] private Text _musicText;
    [SerializeField] private Text _sensivityText;

    public void OnSceneLoad()
    {
        _soundSlider.value = _musicSource.volume;
        
    }

    public void OnSaveClick()
    {
        
    }
}
