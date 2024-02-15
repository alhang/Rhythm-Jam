using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreenUI;
    private string mainMenuScenePath = "Assets/Scenes/MainMenu.unity";

    void Awake()
    {
        deathScreenUI.SetActive(false);
    }

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
        deathScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        deathScreenUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScenePath);
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerDeath -= ShowDeathScreen;
    }
}
