using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class controls the sleep animation of enemies when their health drops to zero
// less violent alternative to being killed
public class enemySleepAnimation : MonoBehaviour
{
    public static enemySleepAnimation instance;

    public SpriteRenderer theSR;
    public Sprite sleepingEnemy;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // replaces the enemy sprite with the sleeping sprite
    public void enemyToSleep()
    {
        theSR.sprite = sleepingEnemy;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
