using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch_Pad : MonoBehaviour
{
    public Transform topSpot;
    public Transform bottomSpot;
    public Transform panel;
    private float speed;
    public float acc;
    bool bombLaunch = false;
    bool goingDown = false;
    bool goingUp = false;

    private void Update()
    {
        if (bombLaunch && goingUp)
        {
            speed = speed + acc * Time.deltaTime;
            panel.position = Vector3.MoveTowards(panel.position, topSpot.position, Time.deltaTime * speed);
        }
        if (panel.position.y == topSpot.position.y)
        {
            Invoke("ChangeDir", 1f);
            speed = 0;
        }
        if (goingDown && panel.position.y <= topSpot.position.y)
        {
            speed = speed + acc * Time.deltaTime;
            panel.position = Vector3.MoveTowards(panel.position, bottomSpot.position, Time.deltaTime * speed);
        }
        if (panel.position.y == bottomSpot.position.y)
        {
            goingDown = false;
            speed = 0;
        }
    }

    void ChangeDir()
    {
        goingDown = true;
        goingUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Explosion")
        {
            bombLaunch = true;
            goingUp = true;
        }


        /*
        if (other.tag == "Player")
        {
            other.transform.parent = transform.parent;
            playerInside = true;
            goingUp = true;
        }
        */
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Player")
        {
            other.transform.parent = null;
            playerInside = false;
        }
        
    }
    */
    
}
