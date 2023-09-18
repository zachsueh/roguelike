using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f; //bullet speed is 7.5 by default, can be changed in Unity.
    public Rigidbody2D theRB;
    public GameObject impactEffect; 

    public int damageToGive = 50; //bullets do 50 damage by default. can be changed in Unity.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed; //when a piece of fruit is fired, moves the bullet whilst taking into account rotation
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation); //when the fruit hits an object, it explodes with fruit juice.
        Destroy(gameObject); //fruit disappears from game


        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive); //if it hits an enemy, it does 50 damage (which can be changed).
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); //if a bullet becomes invisible (leaves the scene), remove it from the game.
    }
}

