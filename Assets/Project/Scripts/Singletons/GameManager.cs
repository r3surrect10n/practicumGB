using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //public NoteBook noteBook = new NoteBook();
    public string currentDay = "1";
    public PlayerInfo currentPlayer = new PlayerInfo();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Invoke("LoadGame", 0.08f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void LoadGame()
    {
#if UNITY_WEBGL
        LoadYandex();
#endif
#if UNITY_EDITOR
        if (File.Exists(Application.persistentDataPath
          + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            Debug.Log(Application.persistentDataPath + "/MySaveData.dat");
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            //Debug.Log(data.ToString());
            UpdateLoadingData(data);
        }
        else
        {
            Debug.Log("There is no save data!");
            GameManager.Instance.currentPlayer = PlayerInfo.FirstGame();
        }
#endif
    }

    public void UpdateLoadingData(SaveData data)
    {
        GameManager.Instance.currentPlayer.isLoaded = true;

        GameManager.Instance.currentPlayer.totalScore = data.score;
        GameManager.Instance.currentDay = data.day;
        GameManager.Instance.currentPlayer.currentLocation = data.location;
        GameManager.Instance.currentPlayer.CsvToPosition(data.csvPosition);
        GameManager.Instance.currentPlayer.CsvToRotation(data.csvRotation);
        GameManager.Instance.currentPlayer.currentCameraName = data.camera;

        //GameManager.Instance.currentPlayer.inventory = new Inventory(data.csvInventory);

        GameManager.Instance.currentPlayer.isHintView = data.isHints;
        GameManager.Instance.currentPlayer.isSoundFone = data.isFone;
        GameManager.Instance.currentPlayer.isSoundEffects = data.isEffects;
        GameManager.Instance.currentPlayer.volumeFone = data.volFone;
        GameManager.Instance.currentPlayer.volumeEffects = data.volEffects;


    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData.dat");
        SaveData data = new SaveData();

        data.score = GameManager.Instance.currentPlayer.totalScore;
        data.csvPosition = GameManager.Instance.currentPlayer.PositionToCsv();
        data.csvRotation = GameManager.Instance.currentPlayer.RotationToCsv();
        data.camera = GameManager.Instance.currentPlayer.currentCameraName;
        data.csvInventory = "";
        data.day = GameManager.Instance.currentDay;
        data.location = GameManager.Instance.currentPlayer.currentLocation;

        //data.csvInventory = GameManager.Instance.currentPlayer.inventory.ToCsvString();

        data.isHints = GameManager.Instance.currentPlayer.isHintView;
        data.isFone = GameManager.Instance.currentPlayer.isSoundFone;
        data.isEffects = GameManager.Instance.currentPlayer.isSoundEffects;
        data.volFone = GameManager.Instance.currentPlayer.volumeFone;
        data.volEffects = GameManager.Instance.currentPlayer.volumeEffects;

        //DateTime dt = DateTime.Now;
        //data.timeString = $"{dt.Year:0000}-{dt.Month:00}-{dt.Day:00}-{dt.Hour:00}";

#if UNITY_WEBGL
        string jsonStr = JsonUtility.ToJson(data);
        SaveYandex(jsonStr);
        SetToLeaderboard(GameManager.Instance.currentPlayer.totalScore);
#endif

        //PlayerInfo.Instance.Save(jsonStr);
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
}

[Serializable]
public class PlayerInfo
{
    public bool isLoaded = false;
    public int totalScore = 0;
    public int currentScore = 0;

    public string currentLocation = "";
    public string currentDay = "";
    public Vector3 currentPosition = Vector3.zero;
    public Vector3 currentRotation = Vector3.zero;
    public string currentCameraName = "";

    //public Inventory inventory;
    //public Inventory currentInventory;

    public bool isHintView = true;
    public bool isSoundFone = true;
    public bool isSoundEffects = true;
    public int volumeFone = 50;
    public int volumeEffects = 100;

    public string playerName = "-------";
    public Texture photo = null;


    public PlayerInfo()
    {
        //maxLevel = 0;
        //currentLevel = 0;
        //inventory = new Inventory();
    }

    public static PlayerInfo FirstGame()
    {
        return new PlayerInfo();
    }

    public void LevelComplete()
    {
        //UpdateReward(currentLevel);
        /*currentLevel++;
        if (currentLevel > maxLevel)
        {
            maxLevel = currentLevel;
        }*/
        totalScore += currentScore;
    }

    public void ClearCurrentParam()
    {
        currentScore = 0;
    }

    public string PositionToCsv(char sep = ';')
    {
        return $"{currentPosition.x:0.000}{sep}{currentPosition.y:0.000}{sep}{currentPosition.z:0.000}{sep}";
    }

    public void CsvToPosition(string csv, char sep = ';')
    {
        string[] ar = csv.Split(sep);
        if (ar.Length == 3)
        {
            if (float.TryParse(ar[0], out float fx) && float.TryParse(ar[1], out float fy) && float.TryParse(ar[2], out float fz))
            {
                currentPosition = new Vector3(fx, fy, fz);
            }
            else currentPosition = Vector3.zero;
        }
        else currentPosition = Vector3.zero;
    }
    public string RotationToCsv(char sep = ';')
    {
        return $"{currentRotation.x:0.000}{sep}{currentRotation.y:0.000}{sep}{currentRotation.z:0.000}{sep}";
    }

    public void CsvToRotation(string csv, char sep = ';')
    {
        string[] ar = csv.Split(sep);
        if (ar.Length == 3)
        {
            if (float.TryParse(ar[0], out float fx) && float.TryParse(ar[1], out float fy) && float.TryParse(ar[2], out float fz))
            {
                currentRotation = new Vector3(fx, fy, fz);
            }
            else currentRotation = Vector3.zero;
        }
        else currentRotation = Vector3.zero;
    }
}

[Serializable]
public class SaveData
{
    public int score;
    public string csvInventory = "";
    public string csvPosition = "";
    public string csvRotation = "";
    public string location;
    public string camera;
    public string day;

    public bool isFone;
    public bool isEffects;
    public bool isHints;
    public int volFone;
    public int volEffects;
    public override string ToString()
    {
        return "SaveData: location=" + location + " position=" + csvPosition;
    }
}


