using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private float _onCursorX;
    [SerializeField] private float _moveTime;

    private Vector2 _originalPostition;

    private Button _noteButton;

    private RectTransform _buttonRectTransform;

    private void Awake()
    {
        _noteButton = GetComponent<Button>();
        _buttonRectTransform = GetComponent<RectTransform>();
        _originalPostition = _buttonRectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_noteButton.interactable)
            return;

        _buttonRectTransform.DOAnchorPosX(_originalPostition.x + _onCursorX, _moveTime).SetUpdate(true);
        _audioSource.PlayOneShot(_audioClip);
    }

    public void OnPointerExit(PointerEventData eventData)
    {        
        _buttonRectTransform.DOAnchorPosX(_originalPostition.x, _moveTime).SetUpdate(true);
    }
    
    public void SetOriginalRect()
    {
        _buttonRectTransform.anchoredPosition = _originalPostition;
    }
}
