using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    [SerializeField] private AnimationManager animationManager;
    [SerializeField] private PlayerScript_Marcos playerScript;
    [SerializeField] private float waitTime = 2.0f;


    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persists between scenes
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
        yield return new WaitForSeconds(waitTime); 
        ResetPlayer();
        PopulateStage();
        ToggleCurtains();
        yield return new WaitForSeconds(waitTime);
    }

    public void ResetStage()
    {
        StartCoroutine(ResetStageRutine());
    }
    private void ToggleCurtains()
    {
        animationManager.ToggleCurtains();
    }
    private void ResetPlayer()
    {
        playerScript.resetPosition();
        Debug.Log("Moving player...");
    }
    private void PopulateStage()
    {
        // Populate the stage with objects
        Debug.Log("Populating stage...");
    }
}
