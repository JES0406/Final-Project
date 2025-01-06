using UnityEngine;
using System.IO;

public static class LevelDataHandler
{

    public static LevelData LoadLevelData(string level)
    {
        string filePath = "Assets/Levels/Level_" + level + ".json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);
            return levelData;
        }
        else
        {
            Debug.LogError("Level data file not found at: " + filePath);
            return null;
        }
    }
}
[System.Serializable]
public class PlayerPosition
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class LevelData
{
    public PlayerPosition playerPosition;
}
