using System;

[Serializable]
public class LevelData
{
    public LevelObjectData[] objects;
}

[Serializable]
public class LevelObjectData
{
    public string type;
    public int x;
    public int y;
}
