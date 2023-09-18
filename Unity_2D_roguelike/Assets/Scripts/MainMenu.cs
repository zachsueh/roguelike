using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public string levelToLoad, levelToTutorial;

    /// <summary>
    /// Privatization class
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// when button clicked, will load main scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    /// <summary>
    /// when button clicked, will load tutorial scene
    /// </summary>
    public void GotoTutorial()
    {
        SceneManager.LoadScene(levelToTutorial);
    }

    public void ExitGame()
    {
        //this line ONLY works when you're not in the Unity editor
        Application.Quit();
    }

}
