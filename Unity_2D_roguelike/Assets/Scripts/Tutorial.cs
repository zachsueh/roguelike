using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// class to control tutorial scene
public class Tutorial : MonoBehaviour
{
    public string mainMenuScene;

    public string pauseMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // when button clicked, will return user to main menu
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    // when button clicked, will return user to pause menu
    public void ReturnToPauseMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(pauseMenuScene);
    }
}
