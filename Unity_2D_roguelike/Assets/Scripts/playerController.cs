using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{


    // public variables can be accessed in the Inspector pane in Unity

    public static playerController instance;

    public float moveSpeed;
    private Vector2 moveInput; 

    public Rigidbody2D theRB;

    public Transform gunArm;

    public Animator anim;

    public SpriteRenderer bodySR;

    public List<Gun> availableWeapons = new List<Gun>();
    [HideInInspector]
    public int currentGun;

    [HideInInspector]
    public bool canMove = true;

    private void Awake()
    {
        instance = this;

        //when the player enters a new level, don't destroy it
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIController.instance.currentGun.sprite = availableWeapons[currentGun].gunUI; //the UI in bottom left shows the player's start weapon
        UIController.instance.gunText.text = availableWeapons[currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        //runs if player is able to move (not deactivated)
        if(canMove) {
            if (!LevelManager.instance.isPaused)
            {
                // horizontal and vertical input provided by keyboard
                moveInput.x = Input.GetAxisRaw("Horizontal");
                moveInput.y = Input.GetAxisRaw("Vertical");

                moveInput.Normalize();

                // take transform position from Unity and add on moveInput
                // deltaTime = 1/(refresh rate)
                // transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

                theRB.velocity = moveInput * moveSpeed;

                Vector3 mousePos = Input.mousePosition;
                Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.localPosition);

                // if mouse position is to the left of the player:
                if (mousePos.x < screenPoint.x)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    // f stands for float
                    gunArm.localScale = new Vector3(-1f, -1f, 1f); //the player switches around.
                }
                else
                {
                    transform.localScale = Vector3.one;
                    gunArm.localScale = Vector3.one;
                }

                // rotate gun arm
                Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
                float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
                gunArm.rotation = Quaternion.Euler(0, 0, angle);

                
                
                if (Input.GetKeyDown(KeyCode.Tab)) //the user can switch between their available weapons by pressing tab
                {
                    if (availableWeapons.Count > 0)
                    {
                        currentGun++;
                        if (currentGun >= availableWeapons.Count)
                        {
                            currentGun = 0;
                        }

                        switchGun();
                    }
                    else Debug.LogError("Player has no guns!"); 
                }

                if (moveInput != Vector2.zero)
                {
                    anim.SetBool("isMoving", true);
                }
                else
                {
                    anim.SetBool("isMoving", false);
                }
            }
        }
        // at end of level, whilst waiting for next level to load, deactivate player
        else {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }

    public void switchGun() //when the user switches gun, it is reflected in the UI on the bottom left.
    {
        foreach(Gun theGun in availableWeapons)
        {
            theGun.gameObject.SetActive(false);
        }

        availableWeapons[currentGun].gameObject.SetActive(true);
        UIController.instance.currentGun.sprite = availableWeapons[currentGun].gunUI;
        UIController.instance.gunText.text = availableWeapons[currentGun].weaponName;
    }

}
