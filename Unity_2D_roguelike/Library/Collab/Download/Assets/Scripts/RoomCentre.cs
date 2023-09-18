using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomCentre : MonoBehaviour
{
    public static RoomCentre instance;

    public List<GameObject> enemies = new List<GameObject>();

    public bool openWhenEnemiesCleared;

    public Room theRoom; 

    public GameObject levelExit;

    public List<Transform> essayPagePoints = new List<Transform>();
    public List<GameObject> essayPages = new List<GameObject>();
    public List<GameObject> essayPagesInRoom = new List<GameObject>();
    public List<int> pageLocation = new List<int>();
    public int numPagesInRoom;
    public int newPageLocation;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (openWhenEnemiesCleared)
        {
            theRoom.closeWhenEntered = true;
        }

        numPagesInRoom = Random.Range(1, essayPages.Count);
        for(int i = 0; i < numPagesInRoom; i++)
        {
            bool distinctLocation = true;
            //do
            //{
                newPageLocation = Random.Range(0, numPagesInRoom-1);
            //    if(pageLocation.Contains(newPageLocation))
            //    {
            //        distinctLocation = false;
            //    }
            //    else
            //    {
                    pageLocation.Add(newPageLocation);
            //    }
            //} while(!distinctLocation);
/*                 if(pageLocation.Count > 0)
                {
                    for(int j = 0; j < pageLocation.Count; j++)
                    {
                        if(pageLocation[j] == newPageLocation)
                        {
                            distinctLocation = false;
                        }
                    }
                    if(distinctLocation)
                    {
                        pageLocation.Add(newPageLocation);
                    }
                }
                else
                {
                    pageLocation.Add(newPageLocation);
                }
            } while(!distinctLocation);*/
            Instantiate(essayPages[i], essayPagePoints[pageLocation[i]].position, essayPagePoints[pageLocation[i]].rotation);
            essayPagesInRoom.Add(essayPages[i]);
        }
        LevelGenerator.instance.numPagesInRoom = LevelGenerator.instance.numPagesInRoom + essayPagesInRoom.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].GetComponent<EnemyController>().canMove == false)
                {
                    enemies.RemoveAt(i);
                    // when enemy put to sleep, decrease number of total enemies by 1 (in LevelGenerator)
                    LevelGenerator.instance.numEnemiesInRoom--;
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        }

        /* if (enemies.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);

                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                theRoom.OpenDoors();
            }
        } */

        /*try
        {
            if(essayPageController.instance.currentNumPages == essayPageController.instance.maxNumPages && enemies.Count == 0)
            {
                levelExit.SetActive(true);
            }
        }
        catch{}*/
    }
}
