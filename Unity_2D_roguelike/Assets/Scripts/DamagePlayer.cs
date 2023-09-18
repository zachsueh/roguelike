﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour 
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) //if the player comes into contact with an object that has the DamagePlayer script and is a trigger, the player takes damage
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(); 
        }
    }

    private void OnTriggerStay2D(Collider2D other) //if the player stays in contact with this object, the player keeps on receiving damage
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) //if the player comes into contact with an object that has a collider that has this script, the player will take damage.
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    private void OnCollisionStay2D(Collision2D other) //if the player stays in contact with this object, the player will keep on taking damage.
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}

