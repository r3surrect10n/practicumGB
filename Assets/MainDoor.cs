using UnityEngine;

public class MainDoor : MonoBehaviour
{
    [SerializeField] private TabletInteraction[] _tablets;

    [SerializeField] private GameObject _medalPlace;

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
}
