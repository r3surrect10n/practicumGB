using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerCabinetControl : MonoBehaviour
{
    [SerializeField] private HandleControl handle;
    [SerializeField] private HingeJoint door;
    [SerializeField] private FuseControl[] fuses;
    [SerializeField] private GameObject fuseOff;
    [SerializeField] private GameObject scheme;
    [SerializeField] private GameObject lamp;
    [SerializeField] private GameObject textHelp;

    private List<GameObject> enteredFuses = new List<GameObject>();
    private int _fuseMask = 2;
    private QuestStatus status = QuestStatus.isFailed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PowerCabinetControl pcc = gameObject.GetComponent<PowerCabinetControl>();
        for (int i = 0; i < fuses.Length; i++)
        {
            fuses[i].SetParamsFuse(pcc);
        }
        handle.SetParams(pcc);
        fuseOff.transform.GetChild(0).gameObject.GetComponent<FuseOffControl>().SetParams(pcc);
        scheme.transform.GetChild(1).gameObject.SetActive(false);
        lamp.SetActive(false);
        for (int i = 0; i < fuses.Length; i++)
        {
            string nameFuse = $"Предохранитель {fuses[i].FuseID}А";
            if (GameManager.Instance.currentPlayer.inventory.GetItem(nameFuse) != null)
            {
                fuses[i].gameObject.SetActive(true);
            }
            else fuses[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool FuseEntered(GameObject fuse)
    {
        
        int index = -1;
        float fx = fuse.transform.position.x, fy = fuse.transform.position.y, cabX = transform.position.x, cabY = transform.position.y;
        float dx = Mathf.Abs(fx - cabX), dy = Mathf.Abs(fy - cabY);
        if ((dx < 0.15f) && (dy > 0.76f && dy < 1.36f)) index = 1;
        if ((dx > 0.14f && dx < 0.34f) && (dy > 0.76f && dy < 1.36f)) index = 0;
        if (index == 0 && fx > cabX) index = 2;
        //print($"fuse pos={fuse.transform.position}  dx={dx}  dy={dy}  index={index}");

        if ((_fuseMask & (1 << index)) > 0) return false;
        else
        {
            fuse.transform.position = scheme.transform.GetChild(index).position;
            fuse.transform.parent = scheme.transform;
            scheme.transform.GetChild(index).gameObject.SetActive(false);
            _fuseMask |= 1 << index;
            enteredFuses.Add(fuse);
            return true;
        }
        return false;
    }

    public bool TestFuses()
    {
        if (_fuseMask == 7)
        {
            int index = 0;
            foreach (GameObject fuse in enteredFuses)
            {
                int zn = fuse.GetComponent<FuseControl>().FuseID;
                if (zn < 16)
                {
                    fuseOff.transform.position = fuse.transform.position;
                    fuseOff.transform.GetChild(0).localScale = new Vector3(100f, 100f, 100f);
                    fuseOff.SetActive(true);
                    enteredFuses.Remove(fuse);
                    Destroy(fuse);
                    float dx = transform.position.x - fuseOff.transform.position.x;
                    if (Mathf.Abs(dx) < 0.1f) index = 1;
                    if (dx > 0 && Mathf.Abs(dx) > 0.2) index = 0;
                    if (dx < 0 && Mathf.Abs(dx) > 0.2) index = 2;
                    _fuseMask ^= 1 << index;
                    return false;
                }
            }
            //  все правильные предохранители на месте -> включаем свет
            Invoke("LampOn", 1.5f);
            status = QuestStatus.isSuccess;
            DelFusesFromInventory();
            return true;
        }
        return false;
    }

    private void DelFusesFromInventory()
    {
        Inventory inventory = GameManager.Instance.currentPlayer.inventory;
        for (int i = inventory.CountItem; i > 0; i--)
        {
            string nmItem = inventory.GetItem(i - 1).ItemName;
            for (int j = 0; j < fuses.Length; j++)
            {
                string nmFuse = $"Предохранитель {fuses[j].FuseID}А";
                if (nmFuse == nmItem)
                {
                    inventory.RemoveItem(i - 1);
                    break;
                }
            }
        }
    }

    private void LampOn()
    {
        textHelp.SetActive(false);
        lamp.SetActive(true);
    }

    public void FuseOffNoActive(float x)
    {
        int index = 1;
        int[] mask = { 6, 5, 3 };
        float dx = transform.position.x - x;
        if (Mathf.Abs(dx) < 0.1f) index = 1;
        if (dx > 0 && Mathf.Abs(dx) > 0.2) index = 0;
        if (dx < 0 && Mathf.Abs(dx) > 0.2) index = 2;
        _fuseMask &= mask[index];
        //print($"x={x} dx={dx}  index={index}  mask={_fuseMask}");
        scheme.transform.GetChild(index).gameObject.SetActive(true);
    }

    public void OnClickQuit()
    {
        GameManager.Instance.currentPlayer.listMiniGames.AddMiniGame(new MiniGameStatus("PowerCabinetScene", status));
        SceneManager.LoadScene("BuildingOld");
    }
}
