using UnityEngine;

public class ToggleSwitcher : MonoBehaviour, IClickable
{
    [SerializeField] private ElectricShield _electricShield;

    [Header("Toggle settings")]
    [SerializeField] private int _toggleNumber;
    [SerializeField] private bool _condition;

    [Header("Lamps color materials")]
    [SerializeField] private Material _red;
    [SerializeField] private Material _green;    

    [SerializeField] private Renderer _renderer;
    private Animator _anim;

    private bool _startCondition;

    public bool Condition => _condition;

    private void Awake()
    {
        _anim = GetComponent<Animator>();        
    }

    private void Start()
    {
        _startCondition = _condition;
        SwitchToggleStatus();
    }

    public void SetValue()
    {
        _electricShield.ToggleSwitch(_toggleNumber);      
    }

    public void SwitchToggleStatus()
    {
        _condition = !_condition;

        if (_condition)
            _renderer.material = _green;
        else 
            _renderer.material = _red;

        _anim.SetBool("IsOn", _condition);
    }

    public void ResetToggle()
    {
        _condition = _startCondition;
        SwitchToggleStatus();
    }
}
