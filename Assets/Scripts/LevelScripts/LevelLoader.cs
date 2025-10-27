using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelObjectData
{
    public string type;
    public int x;
    public int y;
}

[System.Serializable]
public class LevelData
{
    public LevelObjectData[] objects;
}

public class LevelLoader : MonoBehaviour
{
    public LevelObjectFactory factory;
    public TextAsset levelFile;

    void Start()
    {
        if (levelFile == null)
        {
            Debug.LogError("No se ha asignado el archivo de nivel");
            return;
        }

        LevelData levelData = JsonUtility.FromJson<LevelData>(levelFile.text);

        foreach (var obj in levelData.objects)
        {
            LevelObjectType type = (LevelObjectType)System.Enum.Parse(typeof(LevelObjectType), obj.type);
            factory.Create(type, new Vector3(obj.x, obj.y, 0));
        }
    }
}
