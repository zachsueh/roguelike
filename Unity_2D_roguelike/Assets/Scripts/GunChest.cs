using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{

    public GunPickup[] potentialGuns; //create a list of potential guns for the user to change within Unity

    public SpriteRenderer theSR; 
    public Sprite chestOpen; //allows the chest to change to an open sprite 

    public GameObject notification;

    private bool canOpen; 
    private bool isOpen; 

    public Transform spawnPoint; //point at which the gun will spawn having been deployed from chest

    public float scaleSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && !isOpen) //if the chest is closed:
        {
            if (Input.GetKeyDown(KeyCode.E)) //if the player gets within range of the chest and presses E:
            {
                int gunSelect = Random.Range(0, potentialGuns.Length);

                Instantiate(potentialGuns[gunSelect], spawnPoint.position, spawnPoint.rotation); //the chest will be opened and out will pop a gun!

                theSR.sprite = chestOpen; //chest is now open

                isOpen = true; //set this so that chest can't be opened again.

                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); //increases the size of the chest by 20% for a brief moment

                
            }
        }

        if (isOpen)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //allows the user to open the chest when the player comes into contact with it
    {
        if(other.tag == "Player")
        {
            notification.SetActive(true);

            canOpen = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(false);

            canOpen = false;

        }
    }
}
