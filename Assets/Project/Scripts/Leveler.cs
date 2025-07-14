using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class Leveler : MonoBehaviour, ITouchable
{
    [SerializeField] private GameObject[] _uvLights;
    [SerializeField] private GameObject[] _lampLights;
    [SerializeField] private GameObject _uvText;
    [SerializeField] private Material _lampDefaultMaterial;
    [SerializeField] private Material _lampUVMaterial;
    [SerializeField] private Renderer[] _lamps;

    [SerializeField] private AudioClip _upSound;
    [SerializeField] private AudioClip _downSound;

    private Coroutine _setLights;
    private Animator _anim;
    private AudioSource _levelerAudioSource;

    private bool _isUV = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _levelerAudioSource = GetComponent<AudioSource>();
    }

    public void OnTouch()
    {
        //if (_setLights == null)
        //    _setLights = StartCoroutine(SetLights());

        _isUV = !_isUV;
        _anim.SetBool("IsDown", _isUV);
    }

    //private IEnumerator SetLights()
    //{
    //    _isUV = !_isUV;
    //    _anim.SetBool("IsDown", _isUV);

    //    yield return new WaitForSeconds(0.5f);

    //    foreach (var lamp in _lamps)
    //    {
    //        if (_isUV)
    //            lamp.material = _lampUVMaterial;
    //        else
    //            lamp.material = _lampDefaultMaterial;
    //    }

    //    foreach (var uv in _uvLights)
    //    {
    //        if (_isUV)
    //            uv.SetActive(true);
    //        else
    //            uv.SetActive(false);
    //    }

    //    foreach (var light in _lampLights)
    //    {
    //        if (_isUV)
    //            light.SetActive(false);
    //        else
    //            light.SetActive(true);
    //    }

    //    if (_isUV)
    //        _uvText.SetActive(true);
    //    else
    //        _uvText.SetActive(false);

    //    StopCoroutine(_setLights);
    //    _setLights = null;
    //}

    public void SetLights()
    {
        foreach (var lamp in _lamps)
        {
            if (_isUV)
                lamp.material = _lampUVMaterial;
            else
                lamp.material = _lampDefaultMaterial;
        }

        foreach (var uv in _uvLights)
        {
            if (_isUV)
                uv.SetActive(true);
            else
                uv.SetActive(false);
        }

        foreach (var light in _lampLights)
        {
            if (_isUV)
                light.SetActive(false);
            else
                light.SetActive(true);
        }

        if (_isUV)
            _uvText.SetActive(true);
        else
            _uvText.SetActive(false);
    }

    public void PlaySound() 
    {
        if (_isUV)
            _levelerAudioSource.PlayOneShot(_downSound);
        else
            _levelerAudioSource.PlayOneShot(_upSound);
    }

}
