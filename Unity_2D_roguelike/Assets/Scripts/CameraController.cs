using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpeed;

    public Transform target;

    public Camera mainCamera, bigMapCamera;

    private bool bigMapActive; //sets whether big map is on or off

    public bool isBossRoom;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isBossRoom)
        {
            target = playerController.instance.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.M) && !isBossRoom)
        {
            if (!bigMapActive)
            {
                ActivateBigMap();
            } else
            {
                DeactivateBigMap();
            }
        }

    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ActivateBigMap() //when M is pressed, the user sees a big map. 
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive = true;

            bigMapCamera.enabled = true;
            mainCamera.enabled = false;

            playerController.instance.canMove = false; //stops the player from being able to move 

            Time.timeScale = 0f; //stops the game while map is loaded

            UIController.instance.mapDisplay.SetActive(false);
            UIController.instance.bigMapText.SetActive(true);
         }
    }

    public void DeactivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {

            bigMapActive = false;

            bigMapCamera.enabled = false;
            mainCamera.enabled = true;

            playerController.instance.canMove = true; //allows the player to move again

            Time.timeScale = 1f; //resumes the game

            UIController.instance.mapDisplay.SetActive(true);
            UIController.instance.bigMapText.SetActive(false);


        }

    }
}
