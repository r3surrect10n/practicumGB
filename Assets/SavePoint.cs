using UnityEngine;

public class SavePoint : MonoBehaviour, IClearable
{
    [SerializeField] private string _id;
    public string ID => _id;

    public void Clear()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        SaveSystem.Instance.MarkClearable(this);
        SaveSystem.Instance.SaveGame();        
        Destroy(gameObject);
    }
}
