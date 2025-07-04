using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _note;
    [SerializeField] private Text _noteText;    
    [SerializeField] private string _noteContent;

    public void Interact()
    {
        _note.SetActive(true);
        _noteText.text = _noteContent;
    }

    public void EndInteract()
    {
        _note.SetActive(false);
        Destroy(gameObject);
    }

    public void OnMuzzleSolve()
    {
        throw new System.NotImplementedException();
    }
}
