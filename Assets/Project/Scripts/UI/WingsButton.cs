using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    [SerializeField] private float _onCursorX;
    [SerializeField] private float _moveTime;

    private Vector2 _originalPostition;

    private Button _wingButton;

    private RectTransform _buttonRectTransform;

    private void Awake()
    {
        _wingButton = GetComponent<Button>();
        _buttonRectTransform = GetComponent<RectTransform>();
        _originalPostition = _buttonRectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_wingButton.interactable)
            return;

        _buttonRectTransform.DOAnchorPosX(_originalPostition.x + _onCursorX, _moveTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_wingButton.interactable)
            return;

        _buttonRectTransform.DOAnchorPosX(_originalPostition.x, _moveTime);
    }    
}
