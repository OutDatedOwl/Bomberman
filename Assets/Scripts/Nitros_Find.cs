using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitros_Find : MonoBehaviour
{
    Nitros_Find_Ticker nitros_Find_Ticker;

    public float greenSize;
    public float speedRun;

    public float distance;

    private float timer;

    public Transform player;

    public Vector3 chargeSpot;

    private bool playerFound;
    public bool insideSlideZone = false;

    private void Start()
    {
        nitros_Find_Ticker = this.GetComponent<Nitros_Find_Ticker>();
        distance = Vector3.Distance(transform.position, player.position);
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance > greenSize)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 1f)
        {
            if (!playerFound)
            {
                PlayerPosFound();
            }
            /*
            if (distance <= greenSize)
            {
                playerFound = true;

                if (transform.position == chargeSpot)
                {
                    if (insideSlideZone)
                    {
                        chargeSpot = new Vector3(player.position.x, transform.position.y, player.position.z);
                    }
                    timer = 0;
                }
            }
            else
                playerFound = false;
                */

            if (transform.position != chargeSpot)
            {
                //Debug.Log("CHARGE");
                Charge(); // if the enemy is not at charge spot then commence charge
            }

            //if (transform.position != chargeSpot && distance <= greenSize)
            //{
            //    insideSlideZone = false;
            //    nitros_Find_Ticker.ticker = 0;
            //}
            
        }

        if (distance <= greenSize)
        {
            playerFound = true;

            if (transform.position == chargeSpot)
            {
                if (insideSlideZone)
                {
                    chargeSpot = new Vector3(player.position.x, transform.position.y, player.position.z);
                }
                timer = 0;
            }
            if (transform.position != chargeSpot && timer == 0)
            {
                insideSlideZone = false;
                //nitros_Find_Ticker.ticker = 0;
                Charge();
            }
        }
        else
            playerFound = false;
    }

    void PlayerPosFound()
    {
        chargeSpot = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    public void Charge()
    {
        transform.position = Vector3.MoveTowards(transform.position, chargeSpot, Time.deltaTime * speedRun);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.localPosition, greenSize);
    }
}
