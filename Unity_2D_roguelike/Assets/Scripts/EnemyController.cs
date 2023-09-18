using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //instance for the bot
    public static EnemyController instance;



    public Rigidbody2D theRB; 
    public float moveSpeed;

    [Header ("BOSS STUFF")]
    public int currentHealth;
    public GameObject deathEffect;
    public GameObject levelExit;

    [Header ("Chase Player")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection; //variables for the chase player enemy. 

    [Header ("Chase Bot")]
    public bool shouldChaseBot;
    public float rangeToChaseBot; //variables for chase bot enemy.

    [Header ("Patrolling")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint; //variables for patrolling enemy. 

    [Header ("Run Away")]
    public bool shouldRunAway;
    public float runAwayRange; //variables for the coward enemy.

    [Header ("Wandering")]
    public bool shouldWander; 
    public float wanderLength; //how long the enemy should wander for
    public float pauseLength; //how long the enemy should pause for
    private float wanderCounter;
    private float pauseCounter;
    private Vector3 wanderDirection; //variables for the wandering enemy

    [Header ("Shooting")]
    public bool shouldShoot; //whether or not the enemy should shoot the player

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;

    [Header ("Variables")]
    public SpriteRenderer theBody; //allows us to change the sprite of enemies
    public Animator anim; //allows us to animate the enemies

    public int health = 150; //enemies default have 150 health - this can be changed in Unity

    public GameObject[] deathSplatters; //included just in case the user wants to have death splatters to make it more violent

    public GameObject hitEffect; //hit effect for when the bullets hit the enemy

    [HideInInspector]
    public bool canMove = true;

    public GameObject Zzzz; //the speech bubble to show that the enemy is sleeping and hence inactive

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        

        if (shouldWander)
        {
            pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f); //the enemy pauses for a random length of time +- a quarter of the pause length.
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            try {
                if (theBody.isVisible && BotController.instance.gameObject.activeInHierarchy && playerController.instance.gameObject.activeInHierarchy)
                {
                    moveDirection = Vector3.zero;
                    if (Vector3.Distance(transform.position, playerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer)
                    {
                        moveDirection = playerController.instance.transform.position - transform.position;
                    }
                    else if (Vector3.Distance(transform.position, BotController.instance.transform.position) < rangeToChaseBot && shouldChaseBot)
                    {
                        moveDirection = BotController.instance.transform.position - transform.position;
                    }
                    

                    moveDirection.Normalize(); //makes it so the enemy doesn't move diagonally at quicker speeds
                    theRB.velocity = moveDirection * moveSpeed; 

                    //the enemy is to shoot the player, and the player is within the shooting range:
                    if (shouldShoot && (Vector3.Distance(transform.position, playerController.instance.transform.position) < shootRange) || (Vector3.Distance(transform.position, BotController.instance.transform.position) < shootRange))
                    {
                        fireCounter -= Time.deltaTime;

                        if (fireCounter <= 0) 
                        {
                            fireCounter = fireRate;
                            Instantiate(bullet, firePoint.position, firePoint.rotation); //then the enemy should fire at the player.
                        }
                    }

                    /*else
                    {
                        theRB.velocity = Vector2.zero;
                    }*/
                }
            }
            catch{
                if(theBody.isVisible && playerController.instance.gameObject.activeInHierarchy) 
                {

                    //moveDirection = Vector3.zero;


                    if (Vector3.Distance(transform.position, playerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer) //enemy chases player if player is in range of the enemy and should chase player is switched on
                    {
                        moveDirection = playerController.instance.transform.position - transform.position; //in which case the enemy heads towards the player
                    } else
                    {
                        if (shouldWander) //otherwise, if should wander is switched on:
                        {

                            if (wanderCounter > 0)
                            {
                                wanderCounter -= Time.deltaTime; //move the wander counter down

                                //move enemy
                                moveDirection = wanderDirection; //move in the wander direction (which is random)

                                if (wanderCounter <= 0) //when the wander counter goes below zero:
                                {
                                    pauseCounter = Random.Range(pauseLength * .75f, pauseLength * 1.25f); //the enemy will pause for a random amount of time
                                }
                            }

                            if (pauseCounter > 0)
                            {
                                pauseCounter -= Time.deltaTime;

                                if (pauseCounter <= 0) //when the enemy's finished pausing, the enemy will resume wandering
                                {
                                    wanderCounter = Random.Range(wanderLength * .75f, wanderLength * 1.25f); //for a random amount of time

                                    wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f); //this randomises the move direction for the enemy
                                }
                            }

                        }
                        
                        if (shouldPatrol)
                        {
                            moveDirection = patrolPoints[currentPatrolPoint].position - transform.position; //the enemy moves between pre-defined patrol points within Unity

                            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .2f) //if the distance between the enemy and its patrol point is really small:
                            {
                                currentPatrolPoint++; //the enemy advances onto the next patrol point

                                if (currentPatrolPoint >= patrolPoints.Length) 
                                {
                                    currentPatrolPoint = 0; //when the enemy has moved to all the patrol points, it moves back to the first one and cycles through them again
                                }
                            }
                        }
                    }

                

                    if(shouldRunAway && Vector3.Distance(transform.position, playerController.instance.transform.position) < runAwayRange) //if the enemy is set to run away from the player, and the player enters the enemy's run away range:
                    {
                        moveDirection = transform.position - playerController.instance.transform.position; //then set the enemy's move direction to directly opposite the direction of the player
                    }

                    

                    moveDirection.Normalize(); //normalise the move direction so it can't move crazy fast diagonally

                    theRB.velocity = moveDirection * moveSpeed; //the enemy moves at a predefined move speed which can be changed in Unity



                    if (shouldShoot && Vector3.Distance(transform.position, playerController.instance.transform.position) < shootRange) //same as lines 101 to 111
                    {
                        fireCounter -= Time.deltaTime;

                        if (fireCounter <= 0)
                        {
                            fireCounter = fireRate;
                            Instantiate(bullet, firePoint.position, firePoint.rotation);
                        }
                    }

                    else
                    {
                        theRB.velocity = Vector2.zero;
                    }

                }
            }
            

            if (moveDirection != Vector3.zero) //if the enemy is moving, turn the animation on 
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        else
        {
            theRB.velocity = Vector2.zero; //if the enemy is not moving, have the moving animation turned off
            anim.SetBool("isMoving", false);
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        Instantiate(hitEffect, transform.position, transform.rotation); //when the enemy is hit by a piece of fruit, trigger the enemy's hit effect

        if(health <= 0) //when the enemy's health goes below zero, set its death sprite: sleeping
        {
            // enemy sleeping sprite
            //Destroy(gameObject); //don't make the enemy disappear
            canMove = false;    // ensure enemy can't move or shoot
            GetComponent<Animator>().enabled = false;   // disables animation
            Zzzz.SetActive(true);//add the zzz to show that the enemy is sleeping
            Transform enemyBody = transform.Find("Body");
            enemyBody.GetComponent<enemySleepAnimation>().enemyToSleep();    // replaces sprite

            // death splatter effects: uncomment all this and comment out lines 238 to 242 if you want the enemy to disappear and be replaced by a death splatter instead.
            /*int rotation = Random.Range(0, 4);

            int selectedSplatter = Random.Range(0, deathSplatters.Length);

            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation*90));*/

            //Instantiate(deathSplatter, transform.position, transform.rotation);
        }
    }


    //BOSS STUFF
    public void takeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Instantiate(deathEffect, transform.position, transform.rotation);

            if (Vector3.Distance(playerController.instance.transform.position, levelExit.transform.position) < 4f)
            {
                levelExit.transform.position += new Vector3(0f, 6f, 0f);
            }

            levelExit.SetActive(true);

            

        }
    }
}
