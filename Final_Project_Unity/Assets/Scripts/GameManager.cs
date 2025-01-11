using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private ObstacleFactory obstacleFactory;
    [SerializeField] private PlayerScript_Marcos playerScript;
    [SerializeField] private float waitTime = 2.0f;
    [SerializeField] private int level = 1;
    private LevelData levelData;


    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persists between scenes
            level = 0;
            ResetStage();
        }
        else
        {
            Destroy(gameObject);
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
        level++; // Lo pongo aquí para no entrar en bucle infinito
        animationManager.OpenCurtains();
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
    private void ToggleCurtains()
    {
        animationManager.ToggleCurtains();
    }
    private void ResetPlayer()
    {
        PlayerPosition playerPosition = GetPlayerPosition();
        if (playerPosition != null)
        {
            playerScript.SetInitialPosition(playerPosition.x, playerPosition.y, playerPosition.z);
        }

        playerScript.resetPosition();
        Debug.Log("Moving player...");
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

            foreach (EnemyData enemyData in enemiesData.enemies)
            {
                enemyFactory.AddEnemyData(enemyData);
            }
            enemyFactory.SpawnAllEnemies();
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


    public PlayerPosition GetPlayerPosition()
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
}
