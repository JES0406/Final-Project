using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button settingsButton;

    private void Start()
    {
        
    }

    public void OnNewGameClicked()
    {
        Debug.Log("New Game Clicked");
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("MainScene");
    }


    public void OnLoadGameClicked()
    {
        Debug.Log("Load Game Clicked");
        DisableMenuButtons();
        DataPersistanceManager.Instance.gameData = LevelDataHandler.LoadData();
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void OnSettingsClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("Settings");
    }

    public void OnExitGameClicked()
    {
        Debug.Log("Exit Game Clicked");
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        loadGameButton.interactable = false;
        exitGameButton.interactable = false;
        settingsButton.interactable = false;
    }
}
