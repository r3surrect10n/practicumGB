using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _stationsSounds;

    [SerializeField] private Text _stationName;
    [SerializeField] private Text _stationFrequency;
    
    private AudioSource _radioSource;

    private int _firstHalfFreqStart = 80;
    private int _secondHalfFreqStart = 1;

    private int _firstHalf;
    private int _secondHalf;

    private bool _isCharged = false;

    private void Awake()
    {
        _radioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _firstHalf = _firstHalfFreqStart;
        _secondHalf = _secondHalfFreqStart;

        UpdateDisplay();        
    }

    public void SwitchStations(int buttonIndex)
    {        
        switch (buttonIndex)
        {
            case 1: 
                if (_firstHalf < 120)                
                    _firstHalf++;                
                break;
            case 0:
                if (_firstHalf > 80)                
                    _firstHalf--;               
                break;
            case 2:
                _secondHalf += 2;
                if (_secondHalf > 9)
                {
                    _secondHalf = _secondHalf % 10;
                    if (_firstHalf < 120)
                    {
                        _firstHalf++;
                    }
                    else
                    {
                        _secondHalf = 9;
                    }
                }
                break;
            case 3: 
                _secondHalf -= 2;
                if (_secondHalf < 1)
                {
                    _secondHalf += 10;
                    if (_firstHalf > 80)
                    {
                        _firstHalf--;
                    }
                    else
                    {
                        _secondHalf = 1;
                    }
                }               
                break;

            default: break;
        }

        UpdateDisplay();        
    }

    private void UpdateDisplay()
    {
        string frequency = $"{_firstHalf}.{_secondHalf}";
        _stationFrequency.text = frequency;

        CheckStation(_stationFrequency.text);
    }

    private void CheckStation(string frequency)
    {
        _radioSource.Stop();

        switch (frequency)
        {
            case "87.9":
                _stationName.text = "Голос Завета";
                _radioSource.clip = _stationsSounds[1];
                break;
            case "89.5":
                _stationName.text = "Радио Свет Надежды";
                _radioSource.clip = _stationsSounds[2];
                break;
            case "91.3":
                _stationName.text = "Путь Истины FM";
                _radioSource.clip = _stationsSounds[3];
                break;
            case "93.7":
                _stationName.text = "Эммануил Вещает";
                _radioSource.clip = _stationsSounds[4];
                break;
            case "95.5":
                _stationName.text = "Вера и Слово";
                _radioSource.clip = _stationsSounds[5];
                break;
            case "97.7":
                _stationName.text = "Небесная Волна";
                _radioSource.clip = _stationsSounds[6];
                break;
            case "99.9":
                _stationName.text = "БлагоВесть FM";
                _radioSource.clip = _stationsSounds[7];
                break;
            case "101.1":
                _stationName.text = "Живая Вода Радио";
                _radioSource.clip = _stationsSounds[8];
                break;
            case "103.3":
                _stationName.text = "Время Джаза";
                _radioSource.clip = _stationsSounds[9];
                break;
            case "106.7":
                _stationName.text = "Сеятель 77.7";
                _radioSource.clip = _stationsSounds[10];
                break;
            default:
                _stationName.text = "-----";
                _radioSource.clip = _stationsSounds[0];
                break;
        }

        _radioSource.Play();
    }
}
