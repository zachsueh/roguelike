using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    public GameObject layoutRoom;
    public Color startColor, endColor;

    public int distanceToEnd;

    public Transform generatorPoint;

    public enum Direction { up, right, down, left };
    public Direction selectedDirection;

    public float xOffset = 18f, yOffset = 10;

    public LayerMask whatIsRoom;

    private GameObject endRoom;

    private List<GameObject> layoutRoomObjects = new List<GameObject>();

    // list of all room centres used in a given level
    public List<RoomCentre> allRooms = new List<RoomCentre>();
    private RoomCentre currentRoom;
    // number of enemies in a given level
    public int numEnemiesInLevel = 0;
    public bool noEnemies = false;

    public int numPagesInLevel = 0;

    public RoomPrefabs rooms;

    private List<GameObject> generatedOutlines = new List<GameObject>();

    public RoomCentre centreStart, centreEnd;
    public RoomCentre[] potentialCentres;

    public int maxNumPagesInLevel;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;

        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();

        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

            layoutRoomObjects.Add(newRoom);

            if (i + 1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);

                endRoom = newRoom;
            }

            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            while (Physics2D.OverlapCircle(generatorPoint.position, .2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }


        }

        //create room outlines
        CreateRoomOutline(Vector3.zero);
        foreach (GameObject room in layoutRoomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);

        foreach(GameObject outline in generatedOutlines)
        {

            bool generateCentre = true;

            if(outline.transform.position == Vector3.zero)
            {
                Instantiate(centreStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                allRooms.Add(centreStart);
                generateCentre = false;

            }

            if(outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centreEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                allRooms.Add(centreEnd);
                generateCentre = false;

            }

            if (generateCentre)
            {
                int centreSelect = Random.Range(0, potentialCentres.Length);

                currentRoom = potentialCentres[centreSelect];
                allRooms.Add(currentRoom);

                Instantiate(currentRoom, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
            }
            
        }

        //iterate through all rooms in level and sum up number of enemies
        for(int room = 0; room < allRooms.Count; room++)
        {
            numEnemiesInLevel = numEnemiesInLevel + (allRooms[room].GetComponent<RoomCentre>().enemies.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // once all enemies put to sleep, set noEnemies boolean to true
        if (numEnemiesInLevel == 0)
        {
            noEnemies = true;
        }
        else
        {
            noEnemies = false;
        }

        // runs once all enemies put to sleep and all essay pages collected
        // sets the level exit (hole) active in room centre at the end of the level
        if ((noEnemies == true) && (essayPageController.instance.currentNumPages == essayPageController.instance.maxNumPages))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if(currentScene.name == "BossLevel")
            {
                //RoomCentre finalRoom = allRooms[allRooms.Count - 1];
                GameObject finalRoom = GameObject.Find("Room End Book(Clone)");
                finalRoom.GetComponent<RoomCentre>().levelExit.SetActive(true);
            }
            else
            {
                GameObject finalRoom = GameObject.Find("Room End 2(Clone)");
                finalRoom.GetComponent<RoomCentre>().levelExit.SetActive(true);
            }
            //finalRoom.GetComponent<RoomCentre>().levelExit.SetActive(true);
        }
        else
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if(currentScene.name == "BossLevel")
            {
                //RoomCentre finalRoom = allRooms[allRooms.Count - 1];
                GameObject finalRoom = GameObject.Find("Room End Book(Clone)");
                finalRoom.GetComponent<RoomCentre>().levelExit.SetActive(false);
            }
            else
            {
                GameObject finalRoom = GameObject.Find("Room End 2(Clone)");
                finalRoom.GetComponent<RoomCentre>().levelExit.SetActive(false);
            }
            //finalRoom.GetComponent<RoomCentre>().levelExit.SetActive(false);
        }

        /*if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;

            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;

            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;

            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;
        }
    }

    public void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), .2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), .2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, whatIsRoom);

        int directionCount = 0;
        if (roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }

        switch (directionCount)
        {
            case 0:
                Debug.LogError("Found no room exists!!");
                break;

            case 1:

                if (roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                }

                if (roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                }

                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                }

                if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                }

                break;

            case 2:

                if (roomAbove && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                }

                if (roomLeft && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                }

                if (roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
                }

                if (roomRight && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleRightDown, roomPosition, transform.rotation));
                }

                if (roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, transform.rotation));
                }

                if (roomLeft && roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, roomPosition, transform.rotation));
                }

                break;

            case 3:

                if (roomAbove && roomRight && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpRightDown, roomPosition, transform.rotation));
                }

                if (roomRight && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleRightDownLeft, roomPosition, transform.rotation));
                }

                if (roomBelow && roomLeft && roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleDownLeftUp, roomPosition, transform.rotation));
                }

                if (roomLeft && roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, transform.rotation));
                }

                break;

            case 4:


                if (roomBelow && roomLeft && roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.fourway, roomPosition, transform.rotation));
                }

                break;
        }
    }
}


[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singleLeft,
        doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp,
        tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
        fourway;

}
