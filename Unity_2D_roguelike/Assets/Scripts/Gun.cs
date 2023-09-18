using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bulletToFire; //differentiates between bullets (fruit)
    public Transform firePoint; //creates a fire point for the fruit to come from for each weapon

    public float timeBetweenShots;
    private float shotCounter; //allows Unity to change how each weapon fires

    public string weaponName;
    public Sprite gunUI; //allows for a gun UI in the bottom left of the screen



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime; 

        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) //when the user clicks the left button, the player will fire their gun
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation); //fruit exits gun
                shotCounter = timeBetweenShots; //user can define differing shooting speeds in Unity for each weapon.
            }

            
        }
    }
}
