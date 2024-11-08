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
        DisableMenuButtons();
        SceneManager.LoadScene("SampleScene");
    }

    public void OnLoadGameClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadScene("SampleScene");
    }

    public void OnSettingsClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadScene("Settings");
    }

    public void OnExitGameClicked()
    {
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
