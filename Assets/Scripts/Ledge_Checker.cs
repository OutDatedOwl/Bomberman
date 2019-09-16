using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge_Checker : MonoBehaviour
{
    public Vector3 rayCastOut;
    Player player;
    Vector3 edgeGrab;

    private void Start()
    {
        player = this.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(rayCastOut, rayCastOut, 1f))
        {
            Debug.Log("HIT");
            //player.testGrab = true;
            //edgeGrab = new Vector3(0, 0, 0);
            //player.controller.Move(edgeGrab * Time.deltaTime);
        }
        
    }

    void OnDrawGizmosSelected()
    {
        rayCastOut = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(rayCastOut, direction);
    }
}
