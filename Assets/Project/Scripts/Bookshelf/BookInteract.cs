using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BookInteract : MonoBehaviour, ITouchable, IReadable
{
    [SerializeField] private Bookshelf _bookShelf;

    [SerializeField] private GameObject _bookNamePanel;
    [SerializeField] private Text _bookName;
    [SerializeField] private string _bookNameText;

    private bool _isTouched = false;
    private Coroutine _bookMoving;
    
    public bool IsTouched => _isTouched;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
  
    public void OnTouch()
    {        
        _bookMoving = StartCoroutine(BookMoving());
    }

    public void Reset()
    {        
        if (_isTouched) 
            SetBookState("Interaction");
    }

    private void SetBookState(string layerName)
    {
        _isTouched = !_isTouched;
        _anim.SetBool("IsChosen", _isTouched);

        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    private IEnumerator BookMoving()
    {
        SetBookState("Default");

        yield return new WaitForSeconds(0.5f);

        _bookShelf.CheckBooks();

        StopCoroutine(_bookMoving);
    }

    public void ShowName()
    {
        _bookName.text = _bookNameText;
        _bookNamePanel.SetActive(true);
    }

    public void HideName()
    {
        _bookNamePanel.SetActive(false);
    }
}


