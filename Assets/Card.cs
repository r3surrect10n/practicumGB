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

    private Coroutine _coroutine;

    public void OnTouch()
    {
        _cardPanel.CardGetted();        

        _coroutine = StartCoroutine(TellTime());
    }

    private IEnumerator TellTime()
    {
        _tipText.text = _tipPhrase;

        _takingTip.SetActive(true);

        gameObject.SetActive(false);

        yield return new WaitForSeconds(_tipTime);

        _takingTip.SetActive(false);

        StopCoroutine(_coroutine);

        _coroutine = null;
    }
}
