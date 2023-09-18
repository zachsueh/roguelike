using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Gun theGun;

    public float waitToBeCollected = .5f; //sets a brief moment of time that the weapon can't be collected for

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime; 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToBeCollected <= 0)
        {
            bool hasGun = false;
            foreach(Gun gunToCheck in playerController.instance.availableWeapons)
            {
                if(theGun.weaponName == gunToCheck.weaponName) 
                {
                    hasGun = true;
                }
            }

            if (!hasGun) //if the player doesn't already have the gun:
            {
                Gun gunClone = Instantiate(theGun); //set the gun to do the same as the player's current gun (rotation etc)
                gunClone.transform.parent = playerController.instance.gunArm;
                gunClone.transform.position = playerController.instance.gunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;  

                playerController.instance.availableWeapons.Add(gunClone); //adds the gun to the player's arsenal
                playerController.instance.currentGun = playerController.instance.availableWeapons.Count - 1;
                playerController.instance.switchGun(); //switches to the new gun.

            }

            Destroy(gameObject); //when the user has picked up the weapon, it disappears from the floor
        }
    }
}
