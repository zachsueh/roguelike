using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomCentre : MonoBehaviour
{
    public static RoomCentre instance;

    // list of all enemies in that room
    // used by LevelGenerator class to calculate total number of enemies in level
    public List<GameObject> enemies = new List<GameObject>();

    public bool openWhenEnemiesCleared;

    public Room theRoom; 

    public GameObject levelExit;

    // list of locations where an essay could be located
    public List<Transform> essayPagePoints = new List<Transform>();
    // list of essay page prefabs
    public List<GameObject> essayPages = new List<GameObject>();
    // list of actual essay pages in that room
    // used by LevelGenerator class to calculate total number of pages in level
    public List<GameObject> essayPagesInRoom = new List<GameObject>();
    // list of actual page locations used in that room
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

        // randomly adds essay pages to room centre whilst current number in level is less than maximum
        if(LevelGenerator.instance.numPagesInLevel < LevelGenerator.instance.maxNumPagesInLevel)
        {
            // generate random number of essay pages for each room
            numPagesInRoom = Random.Range(0, essayPagePoints.Count);
            if(LevelGenerator.instance.numPagesInLevel + numPagesInRoom > LevelGenerator.instance.maxNumPagesInLevel)
            {
                // if adding these new pages will bring current number over maximum, do nothing
            }
            else
            {
                // otherwise, iterate through each essay page
                for(int i = 0; i < numPagesInRoom; i++)
                {
                    bool distinctLocation = true;
                    do
                    {
                        // randomly assigns a location for the essay page in room
                        newPageLocation = Random.Range(0, essayPagePoints.Count);
                        // if location already contains an essay page, repeat until location is distinct
                        if(pageLocation.Contains(newPageLocation))
                        {
                            distinctLocation = false;
                        }
                        else
                        {
                            // once distinct location found, add page location to list
                            pageLocation.Add(newPageLocation);
                            distinctLocation = true;
                        }
                    } while(!distinctLocation);

                    // generate essay page in scene at page location and add prefab to list of essay pages in room
                    Instantiate(essayPages[i], essayPagePoints[pageLocation[i]].position, essayPagePoints[pageLocation[i]].rotation);
                    essayPagesInRoom.Add(essayPages[i]);    // allows tracking of number of pages active in room, and subsequently level
                }
                
                // update number of current pages in level, in LevelGenerator class
                LevelGenerator.instance.numPagesInLevel = LevelGenerator.instance.numPagesInLevel + essayPagesInRoom.Count;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && theRoom.roomActive && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                // if enemy has been put to sleep, remove this enemy from the list of enemies for the room they're assigned to
                if (enemies[i].GetComponent<EnemyController>().canMove == false)
                {
                    enemies.RemoveAt(i);
                    // when enemy put to sleep, decrease number of total enemies by 1 (in LevelGenerator)
                    LevelGenerator.instance.numEnemiesInLevel--;
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
