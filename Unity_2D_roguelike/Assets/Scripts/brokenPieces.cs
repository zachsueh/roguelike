using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class controls the broken pieces released by the breakable object
public class brokenPieces : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 moveDirection;

    public float deceleration = 5f;

    public float lifetime = 3f;

    public SpriteRenderer theSR;
    public float fadeSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        // when breakable is broken by player each piece is given a random speed to move in, in the x and y directions
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // update piece bosition and move direction (using linear interpolation)
        transform.position += moveDirection * Time.deltaTime;

        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);

        // decrease lifetime with each update
        lifetime -= Time.deltaTime;

        // when lifetime less than 0, fade the colour and when it's not visible any more, destroy the piece
        if(lifetime < 0)
        {
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, Mathf.MoveTowards(theSR.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(theSR.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
