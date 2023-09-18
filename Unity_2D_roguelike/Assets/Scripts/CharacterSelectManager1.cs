using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager1 : MonoBehaviour
{
    public static CharacterSelectManager1 instance;

    public playerController activePlayer;
    public CharacterSelector1 activeCharSelect;

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
}
