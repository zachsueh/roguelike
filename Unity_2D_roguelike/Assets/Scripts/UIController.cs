using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// UIController class
/// </summary>
public class UIController : MonoBehaviour
{
    // singleton pattern
    public static UIController instance;

    public Slider healthSlider;
 
    public Text healthText;

    // pauseMenu, deathScreen, mapDisplay, bigMapText 
    public GameObject pauseMenu, deathScreen, mapDisplay, bigMapText, tutorialScene;

    
    public string mainMenuScene, newGameScene;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack;
    private bool fadeOutBlack;

    public Image currentGun;
    public Text gunText;

    public Slider essayPageSlider;
    public Text essayPageText;
    public Image numPages;

    /// <summary>
    /// Privatization class
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    // 
    /// <summary>
    /// Start is called before the first frame update.
    /// when a scene starts, have it fade in.
    /// </summary>
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;

        currentGun.sprite = playerController.instance.availableWeapons[playerController.instance.currentGun].gunUI; //starting weapon is shown in the UI
        gunText.text = playerController.instance.availableWeapons[playerController.instance.currentGun].weaponName;

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
      if(fadeOutBlack) //controls what happens when the screen fades
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(fadeScreen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        try{
            // update essay page UI at beginning of level
            if(essayPageController.instance.currentNumPages == 0)
            {
                essayPageSlider.maxValue = LevelGenerator.instance.numPagesInLevel;
                essayPageSlider.value = essayPageController.instance.currentNumPages;
                UIController.instance.essayPageText.text = essayPageController.instance.currentNumPages.ToString() + " / " + LevelGenerator.instance.numPagesInLevel.ToString();
                // update maximum number of pages in essayPageController script
                essayPageController.instance.maxNumPages = LevelGenerator.instance.numPagesInLevel;
            }
        }
        catch{}
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }


    /// <summary>
    /// when the users clicks to return to main menu, this happens.
    /// </summary>
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);

        Destroy(playerController.instance.gameObject);
    }

    
    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }


    /// <summary>
    /// when the user clicks resume, the gameplay is resumed.
    /// </summary>
    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }

    /// <summary>
    /// when button clicked, will load tutorial scene
    /// </summary>
    public void openTutorial()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        tutorialScene.SetActive(true);
        //SceneManager.LoadScene(tutorialScene);
    }

    public void closeTutorial()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(true);
        tutorialScene.SetActive(false);
        //SceneManager.LoadScene(tutorialScene);
    }

}
