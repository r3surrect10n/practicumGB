using System.Collections;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _player;

    [SerializeField] private BookInteract[] _books;

    private Animator _anim;

    private int _booksTouched = 0;
    

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void CheckBooks()
    {
        _booksTouched++;        

        if (_booksTouched == 3)
            CheckSolve();
    }

    private void CheckSolve()
    {
        if (_books[0].IsTouched && _books[5].IsTouched && _books[6].IsTouched)
        {
            _player.ClearPreviousHighlight();

            foreach (var book in _books)
            {
                book.gameObject.layer = LayerMask.NameToLayer("Default");                
            }

            StartCoroutine(MoveBookshelf());

        }
        else
        {
            _booksTouched = 0;

            foreach (var book in _books)
            {
                book.Reset();
            }
        }
    }

    private IEnumerator MoveBookshelf()
    {
        yield return new WaitForSeconds(1f);

        _anim.SetBool("IsOpen", true);

        StopCoroutine(MoveBookshelf());
    }
}
