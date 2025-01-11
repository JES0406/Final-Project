using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private ObstacleFactory obstacleFactory;
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private float waitTime = 2.0f;
    private int level = DataPersistanceManager.Instance.gameData.Level;
    private LevelData levelData;


    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            level = 0;
            ResetStage();
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    public IEnumerator ResetStageRutine()
    {
        Debug.Log("Resetting stage...");
        //ToggleCurtains();
        animationManager.CloseCurtains();
        GetLevelData();
        yield return new WaitForSeconds(waitTime);
        PopulateStage();
        ResetPlayer();
        Debug.Log(level);
        level++; // Lo pongo aquí para no entrar en bucle infinito
        Debug.Log(level);
        animationManager.OpenCurtains();
        Debug.Log(playerScript.transform.position);
    }

    public void ResetStage()
    {
        StartCoroutine(ResetStageRutine());
    }
    public void LevelUp()
    {
        ResetStage();
    }

    public int GetCurrentLevel()
    {
        return level;
    }

    private void ResetPlayer()
    {
        Debug.Log("Moving player...");

        Position playerPosition = GetPlayerPosition();
        if (playerPosition != null)
        {
            Debug.Log("Changing Position to: " + playerPosition);
            playerScript.SetInitialPosition(playerPosition.x, playerPosition.y, playerPosition.z);
        }
        else
        {
            Debug.Log("No player Position, reseting to 0,3,0");
            playerScript.SetInitialPosition(0, 3, 0);
        }

        playerScript.resetPosition();
    }
    private void PopulateStage()
    {
        Debug.Log("Populating stage...");
        ResetEnemies();
        ResetObstacles();
        PopulateEnemies();
        PopulateObstacles();
    }

    private void ResetEnemies()
    {
        enemyFactory.ResetEnemies();
    }

    private void ResetObstacles()
    {
        obstacleFactory.ResetObstacles();
    }

    private void PopulateEnemies()
    {
        if (levelData != null)
        {
            EnemiesData enemiesData = levelData.enemiesData;
            Debug.Log("Enemies:" + enemiesData.enemies.Count);


            foreach (EnemyData enemyData in enemiesData.enemies)
            {
                Debug.Log("Enemy spawned: " + enemyData);
                enemyFactory.AddEnemyData(enemyData);
            }
            enemyFactory.SpawnAllEnemies();
        }
        else
        {
            Debug.Log("No Enemies");
        }
    }

    private void PopulateObstacles()
    {
        if (levelData != null)
        {
            ObstaclesData obstaclesData = levelData.obstaclesData;

            foreach (ObstacleData obstacleData in obstaclesData.obstacles)
            {
                obstacleFactory.AddObstacleData(obstacleData);
            }
            obstacleFactory.SpawnAllObstacles();
        }
    }


    public Position GetPlayerPosition()
    {
        
        Debug.Log("LevelData: " + levelData);

        if (levelData != null)
        {
            return levelData.playerPosition;
        }
        else
        {
            Debug.LogError("Level data not found for level: " + level);
        }
        return null;
    }

    private void GetLevelData()
    {
        // Select the data for the current level
        levelData = LevelDataHandler.LoadLevelData(level.ToString());
    }

    private void OnApplicationQuit()
    {
        GameData gameData = new GameData();
        gameData.Level = level;
        LevelDataHandler.SaveData(gameData);
    }
}
