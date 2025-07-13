using UnityEngine;

public class CapInteraction : MonoBehaviour, ITouchable
{
    [SerializeField] private GameObject _textBubble;

    public void OnTouch()
    {
        throw new System.NotImplementedException();
    }
}
