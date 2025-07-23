using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    [SerializeField] private Transform _player;
    [SerializeField] private SpriteFlipper _saveIcon;
    private SaveData _currentSave;
    private string SavePath => Path.Combine(Application.persistentDataPath, "autosave.json");

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        InvokeRepeating(nameof(AutoSave), 60f, 60f);
    }

    private void AutoSave()
    {
        SaveGame();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Building")
        {
            _saveIcon = Object.FindFirstObjectByType<SpriteFlipper>();

            UpdatePlayerReference();
            StartCoroutine(LoadGameCoroutine());
        }
    }

    private void UpdatePlayerReference()
    {
        if (_player == null || _player.gameObject == null)        
            _player = GameObject.FindWithTag("Player")?.transform;        
    }

    public void SaveGame()
    {
        var data = new SaveData();

        _saveIcon.Save();

        data.playerPosition = new float[]
        {
                _player.position.x,
                _player.position.y,
                _player.position.z
        };

        data.playerRotation = new float[]
        {
                _player.eulerAngles.x,
                _player.eulerAngles.y,
                _player.eulerAngles.z
        };

        data.notes = _currentSave?.notes ?? new List<string>();
        data.clearedObjects = _currentSave?.clearedObjects ?? new List<string>();
        data.solves = _currentSave?.solves ?? new List<string>();
        
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        _currentSave = data;
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        yield return null;

        if (!File.Exists(SavePath))
        {
            _currentSave = new SaveData();
            yield break;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        if (data == null)
        {
            _currentSave = new SaveData();
            yield break;
        }

        UpdatePlayerReference();

        _player.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
        _player.eulerAngles = new Vector3(data.playerRotation[0], data.playerRotation[1], data.playerRotation[2]);

        _currentSave = data;
                
        var allNotes = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<INotes>();
        foreach (var obj in allNotes)
        {
            if (_currentSave.notes.Contains(obj.ID))                           
                obj.ClearNote();            
        }

        var allClearables = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IClearable>();
        foreach (var obj in allClearables)
        {
            if (_currentSave.clearedObjects.Contains(obj.ID))
                obj.Clear();
        }

        var allMuzzlees = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IMuzzles>();
        foreach (var obj in allMuzzlees)
        {
            if (_currentSave.solves.Contains(obj.ID))
                obj.Solve();
        }

        Debug.Log("[SaveSystem] Game loaded successfully");
    }    

    public void MarkClearable(IClearable obj)
    {
        if (_currentSave == null)
            _currentSave = new SaveData();

        if (!_currentSave.clearedObjects.Contains(obj.ID))
        {
            _currentSave.clearedObjects.Add(obj.ID);
            Debug.Log($"{obj.ID} is saved in clearedObjects");
        }
    }

    public void MarkNoteReaded(INotes obj)
    {
        if (_currentSave == null)
            _currentSave = new SaveData();

        if (!_currentSave.notes.Contains(obj.ID))
        {
            _currentSave.notes.Add(obj.ID);
            Debug.Log($"{obj.ID} is saved in notes");
        }
    }

    public void MarkMuzzleSolved(IMuzzles obj)
    {
        if (_currentSave == null)
            _currentSave = new SaveData();

        if (!_currentSave.solves.Contains(obj.ID))
        {
            _currentSave.solves.Add(obj.ID);
            Debug.Log($"{obj.ID} is saved in solves");
        }
    }
}