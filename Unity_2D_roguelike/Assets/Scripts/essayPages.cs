using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class controls the collection of essay pages
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

    // method triggered when player walks over essay page
    private void OnTriggerEnter2D(Collider2D other)
    {
        test = other.tag;
        if(other.tag == "Player" || other.tag == "Bot")
        {
            // when page collected, destroy the page from the scene, update UI
            // and decrease number of pages on current level by 1
            if(essayPageController.instance.currentNumPages != essayPageController.instance.maxNumPages)
            {
                Destroy(gameObject);
            }
            
            essayPageController.instance.pickupPage();
        }
    }
}
