using UnityEngine;

public class MainDoor : MonoBehaviour, IMuzzles
{
    [SerializeField] private string _id;

    [SerializeField] private TabletInteraction[] _tablets;

    [SerializeField] private GameObject _medalPlace;

    [SerializeField] private OnSceneExit _sceneExit;

    private AudioSource _audioSource;

    public string ID => _id;

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
            SaveSystem.Instance.MarkMuzzleSolved(this);
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

    public void Solve()
    {
        _medalPlace.layer = LayerMask.NameToLayer("Interaction");
    }
}
