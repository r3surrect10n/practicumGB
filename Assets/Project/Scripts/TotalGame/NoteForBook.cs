using UnityEngine;
using UnityEngine.UI;

public class NoteForBook : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject notePanel;
    [SerializeField] private Text txtNote;
    [SerializeField] private string ids_note;
    [SerializeField] private string roomNote;
    public void EndInteract()
    {
        if (roomNote != "") GameManager.Instance.currentPlayer.notebookList.AddNote(new NotebookItem(ids_note, roomNote));
        notePanel.SetActive(false);
    }

    public void Interact()
    {
        string lang = GameManager.Instance.currentLanguale;
        txtNote.text = NoteSet.Instance.GetNote(ids_note, lang);
        notePanel.SetActive(true);
    }

    public void OnMuzzleSolve()
    {
        throw new System.NotImplementedException();
    }
}
