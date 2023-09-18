using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class controls the health of the player and updates UI
public class PlayerHealthController : MonoBehaviour
{

    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth; //sets the current and max health. Can be changed in Unity.
    public float damageInvincLength = 1f; //sets the invincibility length (after being damaged) to one second.
    private float invincCount;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        maxHealth = CharacterTracker.instance.maxHealth;
        currentHealth = CharacterTracker.instance.currentHealth; //when the user moves between levels, the player's current health is tracked between them.
        

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth; //allows the user to see their health in the top right.
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime; 

            if (invincCount <= 0) 
            {
                playerController.instance.bodySR.color = new Color(playerController.instance.bodySR.color.r, playerController.instance.bodySR.color.g, playerController.instance.bodySR.color.b, 1f);

            }
        }
    }

    // method damages player when player is hit by a bullet or runs over a spike
    public void DamagePlayer()

    {
        if (invincCount <= 0) 
        {

            currentHealth--; //health goes down by 1 if the player is not currently invincible.

            invincCount = damageInvincLength;

            //when the player is invincible, the user can see this because the player becomes slightly transparent.
            playerController.instance.bodySR.color = new Color(playerController.instance.bodySR.color.r, playerController.instance.bodySR.color.g, playerController.instance.bodySR.color.b, 0.5f);

            if (currentHealth <= 0)
            {
                playerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true); //when the player dies, the death screen appears.
            }


            UIController.instance.healthSlider.value = currentHealth; 
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString(); //UI shows health on top left
        }
    }

    // method heals player when health pickup collected
    public void healPlayer(int healAmount)
    {
        currentHealth += healAmount; //if the player picks up a health pack, the current health increases
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth; //but it can't go above the max health.
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
