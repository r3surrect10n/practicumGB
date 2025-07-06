using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _note;

    public void Interact()
    {
        _note.SetActive(true);        
    }

    public void EndInteract()
    {
        _note.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OnMuzzleSolve()
    {
        throw new System.NotImplementedException();
    }
}
