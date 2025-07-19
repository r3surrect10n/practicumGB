using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider sensitivitySlider;

    [SerializeField] private Text musicText;
    [SerializeField] private Text sfxText;
    [SerializeField] private Text sensitivityText;

    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private PlayerLook _playerLook;

    private void Start()
    {
        var sm = SettingsManager.Instance;

        // Инициализация слайдеров из настроек
        RefreshUI();

        // Подписка на события изменения
        musicSlider.onValueChanged.AddListener((v) =>
        {
            sm.SetMusicVolume(v);
            UpdateTexts();
        });

        sfxSlider.onValueChanged.AddListener((v) =>
        {
            sm.SetSFXVolume(v);
            UpdateTexts();
        });

        sensitivitySlider.onValueChanged.AddListener((v) =>
        {
            sm.SetSensitivity(v);
            UpdateTexts();
            // Если нужно, можно вызвать событие или метод в PlayerLook для обновления чувствительности
        });
    }

    // Вызываем при открытии панели настроек
    public void RefreshUI()
    {
        var sm = SettingsManager.Instance;
        musicSlider.value = sm.MusicVolume;
        sfxSlider.value = sm.SFXVolume;
        sensitivitySlider.value = sm.MouseSensitivity;
        UpdateTexts();
    }

    public void SaveAndClose()
    {
        // Сохраняем значения - уже сделано при изменениях, но на всякий случай
        var sm = SettingsManager.Instance;
        sm.SetMusicVolume(musicSlider.value);
        sm.SetSFXVolume(sfxSlider.value);
        sm.SetSensitivity(sensitivitySlider.value);

        if (SceneManager.GetActiveScene().name == "Building")
            _playerLook.SetSensivity();

        settingsPanel.SetActive(false);
    }

    private void UpdateTexts()
    {
        musicText.text = musicSlider.value <= 0.001f ? "Muted" : $"{Mathf.RoundToInt(musicSlider.value * 100)}%";
        sfxText.text = sfxSlider.value <= 0.001f ? "Muted" : $"{Mathf.RoundToInt(sfxSlider.value * 100)}%";
        sensitivityText.text = $"x{sensitivitySlider.value:0.00}";
    }
}