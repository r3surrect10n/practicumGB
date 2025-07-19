using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioSource musicSource; // Может быть null, назначается в сцене
    [SerializeField] private AudioMixer sfxMixer;

    [Header("Defaults")]
    [SerializeField] private float defaultSensitivity = 1f;
    [SerializeField] private float defaultSoundVolume = 1f;
    [SerializeField] private float defaultMusicVolume = 1f;

    public float MusicVolume { get; private set; }
    public float SFXVolume { get; private set; }
    public float MouseSensitivity { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Загрузка настроек
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", defaultMusicVolume);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", defaultSoundVolume);
        MouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", defaultSensitivity);
    }

    private void Start()
    {
        ApplySettings();
    }

    public void SetMusicSource(AudioSource newMusicSource)
    {
        musicSource = newMusicSource;
        // Применяем текущую громкость к новому источнику
        if (musicSource != null)
            musicSource.volume = MusicVolume;
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
        PlayerPrefs.Save();

        if (musicSource != null)
            musicSource.volume = MusicVolume;
    }

    public void SetSFXVolume(float value)
    {
        SFXVolume = Mathf.Clamp01(value);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        PlayerPrefs.Save();

        if (sfxMixer != null)
        {
            float db = Mathf.Log10(Mathf.Clamp(SFXVolume, 0.0001f, 1f)) * 20f;
            sfxMixer.SetFloat("SFXVolume", db);
        }
    }

    public void SetSensitivity(float value)
    {
        MouseSensitivity = Mathf.Max(0.01f, value); // Минимум 0.01 чтобы не было 0
        PlayerPrefs.SetFloat("MouseSensitivity", MouseSensitivity);
        PlayerPrefs.Save();
    }

    public void ApplySettings()
    {
        SetMusicVolume(MusicVolume);
        SetSFXVolume(SFXVolume);
        SetSensitivity(MouseSensitivity);
    }
}