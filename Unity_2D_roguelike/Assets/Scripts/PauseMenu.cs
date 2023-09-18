using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// class to control pause menu scene
/// </summary>
public class PauseMenu : MonoBehaviour
{

    // singleton pattern
    public static PauseMenu instance;

    // pauseMenu gameObject
    public GameObject pauseMenu;

    // Is it a menu pause button
    public static bool isPaused = false;

    // Main menu scene
    public string mainMenuScene;

    // tutorial scene
    public string tutorialScene;

    /// <summary>
    /// Privatization class
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

   
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
    }

    
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }


    /// <summary>
    /// Pause menu update 
    /// </summary>
    public void PauseUnpause()
    {

        if (isPaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    /// <summary>
    /// when button clicked, will resume game
    /// </summary>
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    /// <summary>
    /// when button clicked, will return user to main menu
    /// </summary>
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    /// <summary>
    /// when button clicked, will load tutorial scene
    /// </summary>
    public void openTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(tutorialScene);
    }

}
