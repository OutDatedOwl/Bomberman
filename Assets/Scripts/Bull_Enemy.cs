using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull_Enemy : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Vector3 randomSpot;
    Vector3 center;
    Vector3 chargeSpot;
    Vector3 storeCharge;

    [SerializeField]
    float size;

    [SerializeField]
    float speed;
    [SerializeField]
    float speedRun;

    private float timer;
    private float distance;

    bool reachedMoveSpot = false;
    bool playerFound = false;
    bool charging = false;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        randomSpot = center + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(transform.position, player.position);

        if (distance <= size)
        {
            if (!playerFound)
            {
                PlayerPosFound(); // Player found set charge spot to player pos   
            }
            if (transform.position == chargeSpot)
            { 
                timer += Time.deltaTime;
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

        if (!playerFound)
        {
            if (reachedMoveSpot)
            {
                PickMoveSpot();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, randomSpot, Time.deltaTime * speed);
                if (transform.position.x == randomSpot.x && transform.position.z == randomSpot.z)
                {
                    reachedMoveSpot = true;
                }
            }
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

    void PickMoveSpot()
    {
        randomSpot = center + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        reachedMoveSpot = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.localPosition, size);
    }
}
