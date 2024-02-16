using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;

    private string gameScenePath = "Assets/Scenes/GameScene.unity";
    private string mainMenuScenePath = "Assets/Scenes/MainMenu.unity";
    private string controlsScenePath = "Assets/Scenes/ControlsPage.unity";


    public void Play()
    {
        playerStats.ResetStats();
        SceneManager.LoadScene(gameScenePath);
    }

    public void Options()
    {
        
    }

    public void Controls()
    {
        SceneManager.LoadScene(controlsScenePath);
    }

    public void Menu()
    {
        SceneManager.LoadScene(mainMenuScenePath);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
