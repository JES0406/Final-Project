using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    [SerializeField] private AnimationManager animationManager;
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
            level = 1;
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
        ToggleCurtains();
        GetLevelData();
        yield return new WaitForSeconds(waitTime); 
        ResetPlayer();
        PopulateStage();
        level++; // Lo pongo aquí para no entrar en bucle infinito
        ToggleCurtains();
        yield return new WaitForSeconds(waitTime);
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
        // Populate the stage with objects
        Debug.Log("Populating stage...");
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
