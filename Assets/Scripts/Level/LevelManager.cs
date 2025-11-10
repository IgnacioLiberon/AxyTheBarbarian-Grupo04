using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public LevelObjectFactory factory;
    [SerializeField] private string fileName = "levelrat.json";
    [SerializeField] private bool encryptData = false;

    [Header("Day/Night Time Cycle Properties")]
    [SerializeField] private float dayDuration = 60f;
    [SerializeField] private string timeOfDay = "Day";
    public bool IsDaytime => currentTime < dayDuration; // Example 1-min day cycle
    private float currentTime = 0f;

    [Header("Save Data Property")]
    private LevelData loadedLevel;
    public FileDataHandler dataHandler;
    

    private void Awake()
    {
        instance = this;
        dataHandler = new FileDataHandler(
            Path.Combine(Application.dataPath, "Levels"),
            fileName,
            encryptData: encryptData
        );
    }
    private void Start()
    {
        loadedLevel = dataHandler.LoadData();

        if (loadedLevel == null)
        {
            Debug.LogError("Failed to load level data.");
            return;
        }

        PopulateObjects(loadedLevel);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > dayDuration * 2)
            currentTime = 0f;

        timeOfDay = IsDaytime ? "Day" : "Night";
    }

    public LevelData GetLoadedLevel() => loadedLevel;

    public void SaveLevel(LevelData data)
    {
        dataHandler.SaveData(data);
    }

    private void PopulateObjects(LevelData data)
    {
        foreach (var obj in data.objects)
        {
            LevelObjectType type = (LevelObjectType)System.Enum.Parse(typeof(LevelObjectType), obj.type);
            factory.Create(type, new Vector3(obj.x, obj.y, 0), obj.subtype);
        }
    }
}
