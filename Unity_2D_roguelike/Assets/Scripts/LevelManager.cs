using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;
    public string artCutscene;
    public string historyCutscene;
    public string musicCutscene;
    public string scienceCutscene;

    // Is it a pause menu
    public bool isPaused;

    private float invincCount;

    public Transform startPoint;

    public string thisPlayer;

    /// <summary>
    /// Privatization class
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        playerController.instance.gameObject.SetActive(true);
        playerController.instance.transform.position = startPoint.position;
        playerController.instance.canMove = true;
        Time.timeScale = 1f;
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

        thisPlayer = "" + CharacterSelectManager1.instance.activePlayer;
    }

    

    public IEnumerator LevelEnd()
    {
        //AudioManager.instance.PlayLevelWin();

        playerController.instance.canMove = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        CharacterTracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;

        // if current scene is character select screen
        // load the correct cutscene depending on character selected
        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Character Select1")
        {
            if(thisPlayer == "Player (playerController)" || thisPlayer == "Player - Geo(Clone) (playerController)")
            {
                SceneManager.LoadScene(historyCutscene);
            }
            else if(thisPlayer == "Player - Art(Clone) (playerController)")
            {
                SceneManager.LoadScene(artCutscene);
            }
            else if(thisPlayer == "Player - Music(Clone) (playerController)")
            {
                SceneManager.LoadScene(musicCutscene);
            }
            else if(thisPlayer == "Player - Science(Clone) (playerController)")
            {
                SceneManager.LoadScene(scienceCutscene);
            }
        }
        // otherwise load the next level
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }


    /// <summary>
    /// When false, the pause menu is displayed.
    /// When true, the pause menu is hidden.
    /// </summary>
    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    
}