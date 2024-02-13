using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Assets/Scenes/GameScene.unity");
    }

    public void Options()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
