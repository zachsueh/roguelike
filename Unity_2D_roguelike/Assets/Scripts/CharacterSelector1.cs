using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector1 : MonoBehaviour
{
    private bool canSelect;

    public GameObject message;

    public playerController playerToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSelect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 playerPos = playerController.instance.transform.position;

                Destroy(playerController.instance.gameObject);

                playerController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
                playerController.instance = newPlayer;

                gameObject.SetActive(false);

                CameraController.instance.target = newPlayer.transform;

                CharacterSelectManager1.instance.activePlayer = newPlayer;
                CharacterSelectManager1.instance.activeCharSelect.gameObject.SetActive(true);
                CharacterSelectManager1.instance.activeCharSelect = this;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect = false;
            message.SetActive(false);
        }
    }
}
