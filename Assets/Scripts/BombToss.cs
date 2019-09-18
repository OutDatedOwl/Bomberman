using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombToss : MonoBehaviour
{
    public GameObject bombPrefab; // Used for thowing bomb reference
    public GameObject bombLeftPrefab;
    public GameObject bombRightPrefab;
    public GameObject bombFootPrefab; // Used for kicking bomb reference

    private GameObject bombThow; // Spawned throwing bomb so we don't destroy bombPrefab
    private GameObject bombFoot;// Spawned kicking bomb so we don't destroy bombPrefab

    public Transform feet; // Used to spawn bomb by Player feet    
    public Transform hands; // Used to spawn bomb by Player hands

    public bool canThrowBomb = true; // Check to see if the bomb has been destroyed to create new bomb
    public bool canKickBomb = true; // Check to see if the bomb is below bomb allowance
    public bool canThrowTripleBomb = true;
    public bool tripleBombGone = true;

    private bool startTimer = false;

    public List<GameObject> bombAllowance; // The amount of bombs Player is allowed to have on screen

    float bombTimeCounter;

    private void Start()
    {
        bombAllowance = new List<GameObject>();
        //bombAllowance.Add(bombFoot); // Add one bomb to list for player, will increase with bomb power ups
    }

    void Update()
    {
        // Cannot create when running diag to right FIX THIS
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (canThrowBomb) // Checking if the current thrown bomb has been destroyed, if so then create new bomb to throw
            {
                bombThow = Instantiate(bombPrefab, hands.position, this.transform.rotation);
                startTimer = true;
                canThrowBomb = false;                
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (startTimer)
            {
                bombTimeCounter += Time.deltaTime;
            }   
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (canThrowTripleBomb && bombTimeCounter >= 3f)
            {
                bombThow = Instantiate(bombLeftPrefab, hands.position, this.transform.rotation);
                bombThow = Instantiate(bombRightPrefab, hands.position, this.transform.rotation);
                canThrowTripleBomb = false; 
            }
            tripleBombGone = false;
            startTimer = false;
            bombTimeCounter = 0;
        }

        // Cannot create when running diag to left FIX THIS
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (bombAllowance.Count <= 10) // Number of bombs Player can have, will increase with power ups to allow more bombs
            {
                bombFoot = Instantiate(bombFootPrefab, feet.position, this.transform.rotation);                
                /*
                if (canKickBomb) // Checking if the current bomb on ground has been destroyed, if so then create new bomb to kick
                {
                    bombFoot = Instantiate(bombFootPrefab, feet.position, this.transform.rotation);
                    //bombAllowance.Add(bombFoot);
                    //Debug.Log(bombAllowance.Count);
                }
                */
            }
        }
    }

}
