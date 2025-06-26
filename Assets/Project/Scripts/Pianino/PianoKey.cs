using UnityEngine;

public class PianoKey : MonoBehaviour
{
    [SerializeField] private int numberKey;
    private PianoControl _pianoControl;
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void KeyPressed()
    {
        //_anim.Play("KeyPressed");
        _anim.SetTrigger("Pressed");
    }

    private void OnMouseUp()
    {
        print($"Нажата и отпущена клавиша {gameObject.name}");
        KeyPressed();
        _pianoControl.PressedKey(numberKey);
    }

    public void SetPianoControl(PianoControl pc)
    {
        _pianoControl = pc;
    }
}
