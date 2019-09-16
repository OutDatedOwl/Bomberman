using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitros_Find : MonoBehaviour
{
    public float redSize;
    public float greenSize;
    public float speedRun;

    private float distance;
    private float timer;

    public Transform player;

    private Vector3 chargeSpot;

    private bool charging = false;
    private bool playerFound;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);

        if (distance <= redSize)
        {
            if (distance <= greenSize)
            {
                speedRun = 12;
            }
            else
            {
                speedRun = 8;
            }
            if (!playerFound)
            {
                PlayerPosFound(); // Player found set charge spot to player pos   
            }
            if (transform.position == chargeSpot)
            {
                timer += Time.deltaTime;
                //print(timer);
                if (timer >= 1f)
                {
                    chargeSpot = new Vector3(player.position.x, transform.position.y, player.position.z);
                }
            }
            if (transform.position != chargeSpot)
            {
                timer = 0;
                Charge(); // if the enemy is not at charge spot then commence charge
            }
        }
        else
        {
            playerFound = false;
        }
    }

    void PlayerPosFound()
    {
        playerFound = true;
        chargeSpot = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    void Charge()
    {
        transform.position = Vector3.MoveTowards(transform.position, chargeSpot, Time.deltaTime * speedRun);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.localPosition, redSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.localPosition, greenSize);
    }
}
