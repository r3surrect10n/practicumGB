using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _stationsSounds;
    [SerializeField] private string[] _stationsNames;

    [SerializeField] private Text _stationName;
    [SerializeField] private Text _stationFrequency;
    
    private AudioSource _radioSource;
    private Coroutine _searchingCoroutine;
    private bool _isSearching = false;
    private int _currentStationIndex = 0;

    private int _firstHalfFreqStart = 80;
    private int _secondHalfFreqStart = 1;

    private int _firstHalf;
    private int _secondHalf;

    private float _freqFindTime = 2;

    private void Awake()
    {
        _radioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _firstHalf = _firstHalfFreqStart;
        _secondHalf = _secondHalfFreqStart;

        UpdateDisplayAnother();        
    }

    public void SwitchStations(int buttonIndex)
    {
        if (_isSearching)
            return;

        if (buttonIndex == 0)
        {
            if (_currentStationIndex <= 0)
                _currentStationIndex = _stationsSounds.Length - 1;

            StartSearch(_currentStationIndex - 1);
        }
        else if (buttonIndex == 1)
        {
            if (_currentStationIndex >= _stationsNames.Length - 1)
                _currentStationIndex = -1;

            StartSearch(_currentStationIndex + 1);
        }
        else if (buttonIndex == 2 || buttonIndex == 3)
            ManualAdjast(buttonIndex);           
    }

    public void UpdateDisplayAnother()
    {
        _stationFrequency.text = $"{_firstHalf}.{_secondHalf}";
        Debug.Log(_stationFrequency.text + " 1");
        CheckStation(_stationFrequency.text);
    }

    private void UpdateDisplay()
    {
        Debug.Log(_stationFrequency.text + " 2");
        string frequency = SetFrequency(_currentStationIndex);
        _stationFrequency.text = frequency;

        CheckStation(_stationFrequency.text);
    }

    private void StartSearch(int targetIndex)
    {
        if (_searchingCoroutine != null)
            StopCoroutine(_searchingCoroutine);

        _searchingCoroutine = StartCoroutine(FindFrequency(targetIndex));
    }

    private void ManualAdjast(int buttonIndex)
    {
        switch (buttonIndex)
        {           
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

        UpdateDisplayAnother();
    }

    private void CheckStation(string frequency)
    {
        if (_radioSource.enabled == false)
            return;

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

    private IEnumerator FindFrequency(int targetFreq)
    {
        _isSearching = true;

        _stationName.text = "Поиск...";
        _stationFrequency.text = "";

        float currentFindTime = _freqFindTime;

        _radioSource.clip = _stationsSounds[0];
        _radioSource.Play();

        yield return new WaitForSeconds(_freqFindTime);

        _currentStationIndex = targetFreq;

        UpdateDisplay();

        _isSearching = false;
    }

    private string SetFrequency(int currentFrequency)
    {
        switch (currentFrequency)
        {
            case 0:                
                Frequencies(87, 9);
                break;
            case 1:
                Frequencies(89, 5);
                break;
            case 2:
                Frequencies(91, 3);
                break;
            case 3:
                Frequencies(93, 7);
                break;
            case 4:
                Frequencies(95, 5);
                break;
            case 5:
                Frequencies(97, 7);
                break;
            case 6:
                Frequencies(99, 9);
                break;
            case 7:
                Frequencies(101, 1);
                break;
            case 8:
                Frequencies(103, 3);
                break;
            case 9:
                Frequencies(106, 7);
                break;
            default: 
                break;
        }

        return $"{_firstHalf}.{_secondHalf}";
    }

    private void Frequencies(int first, int second)
    {
        _firstHalf = first;
        _secondHalf = second;
    }
}
