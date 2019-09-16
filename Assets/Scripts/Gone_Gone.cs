using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gone_Gone : MonoBehaviour
{
    private BombToss canThrowBomb;

    private void Start()
    {
        GameObject bombTossing = GameObject.FindGameObjectWithTag("Player"); // Find player
        canThrowBomb = bombTossing.GetComponent<BombToss>(); // Finding bool on Player script BombToss
    }

    public void OnDestroy()
    {
        canThrowBomb.canThrowBomb = true; // Change the bool on BombToss script so Player can create new bomb
        canThrowBomb.canThrowTripleBomb = true;
        canThrowBomb.tripleBombGone = true;
    }
}
