using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;

    private string gameScenePath = "Assets/Scenes/GameScene.unity";

    public void Play()
    {
        playerStats.ResetStats();
        SceneManager.LoadScene(gameScenePath);
    }

    public void Options()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
