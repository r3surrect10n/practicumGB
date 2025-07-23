using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;

    private void OnTriggerEnter(Collider other)
    {
        _saveSystem.SaveGame();
        Destroy(gameObject);
    }
}
