using UnityEngine;

public class NumberKeyControl : MonoBehaviour
{
    [SerializeField] private int numKey;
    [SerializeField] private float speed;

    public int NumberKey { get => numKey; }

    private SafeKeyBoardControl boardControl;
    private bool isDown = false;
    private bool isUp = false;
    private float offset = -0.0002f;

    //private float timer = 0.25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDown)
        {
            Vector3 pos = transform.localPosition;
            if (isUp)
            {
                if (pos.z < 0) pos.z += speed * Time.deltaTime;
                else
                {
                    pos.z = 0;
                    isUp = false;
                    isDown = false;
                }
            }
            else
            {
                if (pos.z > offset) pos.z -= speed * Time.deltaTime;
                else
                {
                    pos.z = offset;
                    isUp = true;
                }
            }
            transform.localPosition = pos;
        }
    }

    public void SetBoardControl(SafeKeyBoardControl skbc)
    {
        boardControl = skbc;
    }

    //private void OnMouseUp()
    //{
    //    //print($"Key pressed nameKey={gameObject.name}");
    //    if (Input.GetMouseButtonUp(0) && isDown == false)
    //    {
    //        print($"Key pressed nameKey={gameObject.name}");
    //        isDown = true;
    //        if (boardControl != null) boardControl.KeyPressed(numKey);
    //    }
    //}

    public void KeyPressed()
    {
        if (isDown == false) isDown = true;
    }
}
