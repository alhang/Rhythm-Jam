using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    private string mainMenuScenePath = "Assets/Scenes/MainMenu.unity";
    private string victoryScenePath = "Assets/Scenes/VictoryScene.unity";
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnPlayerVictory += ShowVictoryScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowVictoryScreen()
    {
        SceneManager.LoadScene(victoryScenePath);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(mainMenuScenePath);
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerVictory -= ShowVictoryScreen;
    }
}
