using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private string mainMenuScenePath = "Assets/Scenes/MainMenu.unity";
    private string deathScenePath = "Assets/Scenes/DeathScene.unity";

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnPlayerDeath += ShowDeathScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDeathScreen()
    {
        SceneManager.LoadScene(deathScenePath);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(mainMenuScenePath);
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerDeath -= ShowDeathScreen;
    }
}
