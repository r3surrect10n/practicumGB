using UnityEngine;

public class SavePoint : MonoBehaviour, IClearable
{
    [SerializeField] private string _id;
    [SerializeField] private SaveSystem _saveSystem;

    public string ID => _id;

    public void Clear()
    {
        SaveSystem.Instance.MarkClearable(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        _saveSystem.SaveGame();
        Clear();
        Destroy(gameObject);
    }
}
