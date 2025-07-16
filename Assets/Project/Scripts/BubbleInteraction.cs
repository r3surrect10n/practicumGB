using UnityEngine;

public class BubbleInteraction : MonoBehaviour, ITouchable
{
    [SerializeField] private GameObject _textBubble;

    public void OnTouch()
    {
        throw new System.NotImplementedException();
    }
}
