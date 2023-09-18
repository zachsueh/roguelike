using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterCutscene : MonoBehaviour
{
    public string nextLevel;
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += loadScene;

        playerController.instance.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void loadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void skipToLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
