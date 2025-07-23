using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HereTip : MonoBehaviour, IClearable
{
    [SerializeField] private string _id;

    [SerializeField] private GameObject _tellWindow;
    [SerializeField] private Text _tellText;

    [SerializeField] private float _tellDuration;
    [SerializeField] private string _tellPhrase;

    private Coroutine _tellCoroutine;

    private bool _isWalked = false;

    public string ID => _id;

    public void Clear()
    {
        _tellWindow.SetActive(false);

        Destroy(gameObject);
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() && !_isWalked)
            _tellCoroutine = StartCoroutine(TellTime());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
            SaveSystem.Instance.MarkClearable(this);
    }

    private IEnumerator TellTime()
    {
        _isWalked = true;

        _tellText.text = _tellPhrase;        

        _tellWindow.SetActive(true);
        
        yield return new WaitForSeconds(_tellDuration);

        Clear();
    }
}
