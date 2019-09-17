using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    //BombToss bombKicking; // Gameobject to find player Player
    //bool canKickBomb; // Are there bombs on screen? If true then can't create new bomb, if not then Player is allowed new bomb to kick
    //GameObject[] kickBomb; // Checking if bombs are on screen to allow Player to create new bomb to kick
    private int playerHP = 3; // Player HP

    //List<GameObject> bombAllowance;

    public AudioClip chickenClip;

    public AudioSource chickenSource;

    private void Start()
    {
        //GameObject canKickBombs = GameObject.FindGameObjectWithTag("Player");
        //bombKicking = canKickBombs.GetComponent<BombToss>();
        //chickenSource.clip = chickenClip;
    }

    private void Update()
    {
        //kickBomb = GameObject.FindGameObjectsWithTag("Bomb_Foot");
        //CheckForBombs();
    }

    void CheckForBombs()
    {
        /*
        if (kickBomb.Length == 0) // No bombs? Allow new bomb
        {
            canKickBomb = true;
        }
        if (kickBomb.Length == 1) // If there are bombs on screen then restrict new bomb being created
        {
            canKickBomb = false;
        }
        bombKicking.canKickBomb = canKickBomb; // Return boolean to BombToss script to allow Player to create new bomb or not
        */

    }

    public void HealthSystem(GameObject thingHit)
    {
        if (thingHit.tag == "Player" && playerHP != 0)
        {
            playerHP = playerHP - 1;
        }

        if (thingHit.tag == "Enemy")
        {
            chickenSource.Play();
            //thingHit.SetActive(false);
            //Destroy(thingHit);
        }
    }

}
