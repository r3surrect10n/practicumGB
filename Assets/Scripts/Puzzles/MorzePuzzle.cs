using UnityEngine;
using UnityEngine.UI;

public class MorzePuzzle : MonoBehaviour
{
    [SerializeField] private Text _morzeAlphabet;
    [SerializeField] private Text _generatedCode;
    [SerializeField] private Text _morzeCodeText;

    [Header("Number code for puzzle")]
    [SerializeField] private int _hiddenCodeLength;    //Длина кода

    private string[] _morzeCodeAlphabet;
    private int[] _combination; //Разделенная кодовая строка

    private string _encryptedCode;

    private void Start()
    {
        MorzeNumberCode();
    }

    public void OnCreateCodeButtonClick()
    {   
        ClearFields();
        CreateCode();
        CreateMorzeCombination();
        CombinationOut();
    }

    private void MorzeNumberCode()  //Вывод азбуки морзе для чисел
    {
        _morzeCodeAlphabet = new string[]
        {
            "-----",    //0
            ".----",    //1
            "..---",    //2
            "...--",    //3
            "....-",    //4
            ".....",    //5
            "-....",    //6
            "--...",    //7
            "---..",    //8
            "----."     //9
        };
        
        for (int i = 0; i < _morzeCodeAlphabet.Length; i++)
        {
            Debug.Log($"{i} - {_morzeCodeAlphabet[i]}");
            _morzeAlphabet.text += $"{i} - {_morzeCodeAlphabet[i]}\n";
        }
    }

    private void CreateCode()   //Генерация случайной последовательности чисел
    {
        int[] code = new int[_hiddenCodeLength];

        for (int i = 0; i < _hiddenCodeLength; i++)
            code[i] = Random.Range(0, 9);

        foreach (int i in code) 
        { 
            Debug.Log(i);
            _generatedCode.text += $"{i}\t"; 
        }

        _combination = code;
    }

    private void CreateMorzeCombination()   //Шифрование последовательности в азбуку морзе
    {
        foreach (int num in _combination)
            _encryptedCode += _morzeCodeAlphabet[num] + " ";
    }

    private void CombinationOut()
    {        
        _morzeCodeText.text = _encryptedCode;
    }

    private void ClearFields()
    {
        _encryptedCode = "";
        _morzeCodeText.text = "";
        _generatedCode.text = "";
    }
}
