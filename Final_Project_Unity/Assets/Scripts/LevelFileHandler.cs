using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class LevelDataHandler 
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

    public static void SaveData(GameData gameData)
    {
        // We save the level that the player got to.
        string filePath = "SaveData.json";
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
        string json = JsonUtility.ToJson(gameData);

        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
                streamWriter.WriteLine(json);
            }
        }
    }

    public static GameData LoadData()
    {
        string saveDataPath = "SaveData.json";

        if (File.Exists(saveDataPath))
        {
            string json = File.ReadAllText(saveDataPath);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            return new GameData();
        }
    }
}

[System.Serializable]
public class GameData
{
    public int Level = 1;
}

[System.Serializable]
public class PlayerPosition
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class EnemyStats
{
    public float health;
    public float attackDamage;
    public float attackRange;
    public float attackSpeed;
    public float attackTimer;
}

[System.Serializable]
public class EnemyPosition
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class EnemyData
{
    public int id;
    public EnemyPosition position;
    public EnemyStats stats;
}

    [System.Serializable]
public class EnemiesData
{
    public List<EnemyData> enemies;
}

[System.Serializable]
public class ObstaclePosition
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class ObstacleRotation
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class ObstacleData
{
    public int id;
    public ObstaclePosition position;
    public ObstacleRotation rotation;
}

[System.Serializable]
public class ObstaclesData
{
    public List<ObstacleData> obstacles;
}

[System.Serializable]
public class LevelData
{
    public PlayerPosition playerPosition;
    public EnemiesData enemiesData;
    public ObstaclesData obstaclesData;
}
