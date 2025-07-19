using UnityEngine;

public class MainDoor : MonoBehaviour
{
    [SerializeField] private TabletInteraction[] _tablets;

    [SerializeField] private GameObject _medalPlace;

    [SerializeField] private OnSceneExit _sceneExit;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void CheckPasswords()
    {
        bool allPasswords = true;

        foreach (var tablet in _tablets)
        {
            if (!tablet.IsPassCorrect())
            {
                allPasswords = false;
                break;
            }
        }

        if (allPasswords)
        {
            _medalPlace.layer = LayerMask.NameToLayer("Interaction");
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void LoadEndScene()
    {
        _sceneExit.OnSceneEnd("EndingScene");
    }
}
