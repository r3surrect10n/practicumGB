using UnityEngine;

public class ChangeFrqBtnControl : MonoBehaviour
{
    [SerializeField] private RadioReceiverControl receiverControl;
    [SerializeField] private int numberButton;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            receiverControl.OnChangeBtnClick(numberButton);
            if (anim != null)
            {
                anim.SetTrigger("TriggerDown");
            }
        }
    }

}
