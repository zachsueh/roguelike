using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class essayPages : MonoBehaviour
{
    public static essayPages instance;
    public string test;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        test = other.tag;
        if(other.tag == "Player" || other.tag == "Bot")
        {
            if(essayPageController.instance.currentNumPages != essayPageController.instance.maxNumPages)
            {
                Destroy(gameObject);
            }

            essayPageController.instance.pickupPage();
            LevelGenerator.instance.numPagesInRoom--;
        }
    }
}
