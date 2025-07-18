using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, ITouchable
{
    [SerializeField] private CardReaderPanel _cardPanel;

    [SerializeField] private GameObject _takingTip;
    [SerializeField] private Text _tipText;
    [SerializeField] private string _tipPhrase;
    [SerializeField] private float _tipTime;

    public void OnTouch()
    {
        _cardPanel.CardGetted();        

        StartCoroutine(TellTime());
    }

    private IEnumerator TellTime()
    {
        _tipText.text = _tipPhrase;
        _takingTip.SetActive(true);

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;


        yield return new WaitForSeconds(_tipTime);

        _takingTip.SetActive(false);
        gameObject.SetActive(false);
    }
}
