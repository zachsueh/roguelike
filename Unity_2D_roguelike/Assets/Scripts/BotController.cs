using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//THE BOT IS SUPPOSED TO BE AN AUTOMATED PLAYER
//If enemy is available:
//  Bot moves towards the enemy
//  Bot moves further away from the enemy (opposite to chasing the enemy) if the enemy gets too close
//  Bot shoots the enemy
//Once the enemy is dead, locate nearest objects to collect and collect them
//Bot moves through the dungeon and locates the nearest exit
public class BotController : MonoBehaviour
{
    public static BotController instance;

    public Rigidbody2D theRB;
    public float moveSpeed;

    public Vector3 moveDirection;
    public float rangeToRunAwayFromEnemy;
    private bool isEnemyChasing = false;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
       
    }

    void Update()
    {
        //randomly move bot in different directions, test to see if there's an enemy or object to collect

        // try allows code to see if enemy is active but once enemy killed, catch is run without error beign produced
        try
        {
            // if statement runs if enemy active in hierarchy (i.e. enemy in room)
            if (EnemyController.instance.gameObject.activeInHierarchy)
            {
                // initially bot is larger than rangeToRunAwayFromEnemy so it runs towards enemy
                if ((Vector3.Distance(transform.position, EnemyController.instance.transform.position) >= rangeToRunAwayFromEnemy) && (isEnemyChasing == false))
                {    
                    moveDirection = EnemyController.instance.transform.position - transform.position;
                }
                //check if the distance between the bot and the enemy is less than the range for the bot to run away from the enemy
                else if (Vector3.Distance(transform.position, EnemyController.instance.transform.position) < rangeToRunAwayFromEnemy)
                {
                    // when running away, enemy is chasing bot
                    isEnemyChasing = true;
                    //move the bot in the opposite direction to the enemy
                    moveDirection = transform.position - EnemyController.instance.transform.position;
                
                }
                moveDirection.Normalize();
                theRB.velocity = moveDirection * moveSpeed;

                if (Vector3.Distance(transform.position, EnemyController.instance.transform.position) < rangeToRunAwayFromEnemy)
                {
                    fireCounter -= Time.deltaTime;

                    if (fireCounter <= 0)
                    {
                        fireCounter = fireRate;
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                    }
                }
            }
        }
        catch (System.Exception)
        {
            // once enemy dead, collect essay page (doesn't work yet)
            /* if (essayPageController.instance.gameObject.activeInHierarchy)
            {
                moveDirection = essayPageController.instance.transform.position - transform.position;
            } */
        }
        
        // bot faces other direction when moving to the left
        if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
        
        //BOT SHOOTS THE ENEMY
        //AND MOVES THROUGH THE DUNGEON (TO INTERACT WITH OBJECTS / TO EXIT)



        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
