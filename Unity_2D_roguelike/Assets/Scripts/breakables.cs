using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class controls the breakable objects such as the chests
// it will break the object and release broken pieces and pickups when triggered by the player
public class breakables : MonoBehaviour
{
    // list of broken pieces
    public GameObject[] brokenPieces;
    public int maxPieces = 5;

    // list of pickups to drop
    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this method is triggered when the player walks over the breakable object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);

            // number of broken pieces to drop
            int piecesToDrop = Random.Range(1, maxPieces);

            // for each piece, generate which sprite will be used and then display in scene
            for(int i = 0; i < piecesToDrop; i++)
            {
                int randomPiece = Random.Range(0, brokenPieces.Length);

                Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
            }

            // drop pickups
            if(shouldDropItem)
            {
                // define chance of dropping pickup and release it if this chance is less than the desired probability
                float dropChance = Random.Range(0f, 100f);

                if(dropChance < itemDropPercent)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                }
            }
        }
    }
}
