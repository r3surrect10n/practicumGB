using UnityEngine;

public class NotebookControl : MonoBehaviour
{
    [SerializeField] private GameObject notePanel;
    private bool isVisible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnViewNotebook()
    {
        isVisible = !isVisible;
        if (isVisible)
        {
            
        }
        else
        {
            
        }
        notePanel.SetActive(isVisible);
    }
}
