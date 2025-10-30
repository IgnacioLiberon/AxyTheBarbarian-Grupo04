using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelObjectFactory factory;
    [SerializeField] private string fileName = "leveltest.json";
    [SerializeField] private bool encryptData = false;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        dataHandler = new FileDataHandler(
            Path.Combine(Application.dataPath, "Levels"),
            fileName,
            encryptData: encryptData
        );
    }
    private void Start()
    {
        LevelData levelData = dataHandler.LoadData();

        if (levelData == null)
        {
            Debug.LogError("Failed to load level data.");
            return;
        }

        PopulateObjects(levelData);
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
