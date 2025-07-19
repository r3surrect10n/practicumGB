using UnityEngine;

public class MusicInitializer : MonoBehaviour
{
    private void Start()
    {
        if (SettingsManager.Instance != null)
        {
            SettingsManager.Instance.SetMusicSource(GetComponent<AudioSource>());
        }
    }
}