using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered; //openWhenEnemiesCleared;

    public GameObject[] doors;

    //public List<GameObject> enemies = new List<GameObject>();

    [HideInInspector]
    public bool roomActive;

    public GameObject mapHider;

    public GameObject levelExit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /* try
        {
            if(essayPageController.instance.currentNumPages == essayPageController.instance.maxNumPages)
            {
                levelExit.SetActive(true);
            }
        }
        catch{} */

        /*if (enemies.Count > 0 && roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                // remove enemy from list if it's sleeping
                if(enemies[i].GetComponent<EnemyController>().canMove == false)
                {
                    enemies.RemoveAt(i);
                }
                /* if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);

                    i--;
                } 
            }

            if (enemies.Count == 0)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);

                    closeWhenEntered = false;
                }
            }
        }*/
    }

    public void OpenDoors() 
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);

            closeWhenEntered = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);

            if (closeWhenEntered)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }

            roomActive = true;

            mapHider.SetActive(false);
        }
    }

    private void onTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            roomActive = true;
        }
    }
}
