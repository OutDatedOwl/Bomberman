using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stomp : MonoBehaviour
{
    private Transform enemyStomper;
    public Transform botStomp;
    public Transform topStomp;
    bool goingDown = true;
    bool goingUp = false;
    private float speed;
    public float acc;

    private void Start()
    {
        enemyStomper = this.GetComponent<Transform>();
    }

    void Update()
    {
        if (goingDown)
        {
            speed = speed + acc * Time.deltaTime;
            enemyStomper.position = Vector3.MoveTowards(enemyStomper.position, botStomp.position, Time.deltaTime * speed);
        }

        if (enemyStomper.position.y == botStomp.position.y)
        {
            goingUp = true;
            goingDown = false;
            speed = 0;
        }

        if (enemyStomper.position.y == topStomp.position.y)
        {
            goingUp = false;
            goingDown = true;
        }

        if (goingUp)
        {
            //speed = 0;
            enemyStomper.position = Vector3.MoveTowards(enemyStomper.position, topStomp.position, Time.deltaTime * 2);
            //Invoke("MoveUp", 0.75f);
        }
    }

    void MoveUp()
    {
        enemyStomper.position = Vector3.MoveTowards(enemyStomper.position, topStomp.position, Time.deltaTime * 2);
    }
}
