using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class controls health pickups to restore player health
public class healthPickup : MonoBehaviour
{
    public int healAmount = 1;

    public float waitToBeCollected = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // waitToBeCollected ensure player can't pick up health pack immediately after destroying chest
        if(waitToBeCollected > 0)
        {
            waitToBeCollected -= Time.deltaTime;
        }
    }

    // method triggered when player walks over health pickup
    // destorys health pickup from scene and increases player health by healAmount
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && waitToBeCollected <= 0)
        {
            PlayerHealthController.instance.healPlayer(healAmount);
            
            Destroy(gameObject);
        }
    }
}
