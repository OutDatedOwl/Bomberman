using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp_Follow : MonoBehaviour
{
    public Transform stomperParent;
    public Transform stomper;
    public Transform botStomp;
    public Transform topStomp;

    private Vector3 moveStomperDest;
    private Vector3 playerPos;

    private float playerPosX;
    private float playerPosZ;
    private float dropSpeed;

    public float speed;
    public float liftSpeed;
    public float acc;

    bool playerPosFound = false;
    bool goingDown = true;
    bool goingUp = false;
    bool atTop;

    // Update is called once per frame
    void Update()
    {
        if (atTop && !playerPosFound)
        {
            stomperParent.position = Vector3.MoveTowards(stomperParent.position, playerPos, Time.deltaTime * speed);
        }
        if (playerPosFound)
        {
            DropDown();
        }

        PlayerFound();
    }

    void DropDown()
    {
        if (goingDown)
        {
            atTop = false;
            dropSpeed = dropSpeed + acc * Time.deltaTime;
            stomper.position = Vector3.MoveTowards(stomper.position, botStomp.position, Time.deltaTime * dropSpeed);
        }

        if (stomper.position.y == botStomp.position.y)
        {
            goingUp = true;
            goingDown = false;
            dropSpeed = 0;
        }

        if (stomper.position.y == topStomp.position.y)
        {
            goingUp = false;
            goingDown = true;
            atTop = true;
            playerPosFound = false;
        }

        if (goingUp)
        {
            //speed = 0;
            stomper.position = Vector3.MoveTowards(stomper.position, topStomp.position, Time.deltaTime * liftSpeed);
            //Invoke("MoveUp", 0.75f);
        }
    }

    void PlayerFound()
    {
        if (stomperParent.position.x == playerPosX && stomperParent.position.z == playerPosZ)
        {
            playerPosFound = true;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            playerPosX = col.transform.position.x;
            playerPosZ = col.transform.position.z;
            playerPos = new Vector3(playerPosX, stomper.position.y, playerPosZ);
            atTop = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            atTop = false;
        }
    }

}
