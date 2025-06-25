using UnityEngine;
using UnityEngine.UI;

public class SafeKeyBoardControl : MonoBehaviour
{
    [SerializeField] private GameObject[] keys;
    [SerializeField] private SafeDoorControl doorControl;
    [SerializeField] private string password;
    [SerializeField] private string hint = "Ещё одна подсказка";
    [SerializeField] private Text displayText;
    [SerializeField] private GameObject infoPanel;

    private string _enteredStr = "";
    private char[] codeKey = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '*', '0', '#' };
    private bool isError = false, isViewText = false;
    private float timer = 0.3f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SafeKeyBoardControl skbc = gameObject.GetComponent<SafeKeyBoardControl>();
        for(int i = 0; i < keys.Length; i++)
        {
            NumberKeyControl keyControl = keys[i].GetComponent<NumberKeyControl>();
            if (keyControl != null) keyControl.SetBoardControl(skbc);
        }
        displayText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (isError)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timer = 0.3f;
                isViewText = !isViewText;
                displayText.gameObject.SetActive(isViewText);
            }
        }
    }

    public void KeyPressed(int num)
    {

    }

    private void OnMouseUp()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = -1, y = -1;
        if (mousePos.x > 0.1f && mousePos.x < 0.24f) x = 0;
        if (mousePos.x > -0.08f && mousePos.x < 0.08f) x = 1;
        if (mousePos.x > -0.24f && mousePos.x < -0.1f) x = 2;
        if (mousePos.y > 0.14f && mousePos.y < 0.28f) y = 0;
        if (mousePos.y > 0.02f && mousePos.y < 0.12f) y = 1;
        if (mousePos.y > -0.12f && mousePos.y < -0.02f) y = 2;
        if (mousePos.y > -0.28f && mousePos.y < -0.14f) y = 3;
        //print($"x={x} y={y}  mousePos={mousePos} for input={Input.mousePosition}");
        if (x != -1 && y != -1)
        {
            int index = 3 * y + x;
            keys[index].GetComponent<NumberKeyControl>().KeyPressed();
            if (index == 9)
            {
                _enteredStr = "";
                isError = false;
                displayText.gameObject.SetActive(true);
            }
            else
            {
                if (isError) return;
                _enteredStr += codeKey[index];
                if (_enteredStr.Length == password.Length)
                {
                    if (_enteredStr == password)
                    {
                        transform.parent = doorControl.gameObject.transform;
                        doorControl.OpenDoor();
                        Invoke("ViewHint", 3f);
                    }
                    else
                    {
                        _enteredStr = "ERROR";
                        isError = true;
                    }
                }
            }
            displayText.text = _enteredStr;
        }
    }

    public void ViewHint()
    {
        infoPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = $"Вы нашли подсказку : \"{hint}\"";
        infoPanel.gameObject.SetActive(true);
    }

    public void SetPassword(string psw)
    {
        password = psw;
    }

    public void SetHint(string sh = "Ещё одна подсказка")
    {
        hint = sh;
    }
}
