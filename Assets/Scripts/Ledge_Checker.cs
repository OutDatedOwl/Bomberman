using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge_Checker : MonoBehaviour
{
    public Vector3 rayCastOut;

    bool hanging;

    Player player;

    int layer_mask;

    public Transform targetPos;

    RaycastHit hit;

    private void Start()
    {
        player = this.GetComponent<Player>();
        layer_mask = LayerMask.GetMask("Environment");
    }

    private void Update()
    {
        if (!hanging)
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 1f, layer_mask))
            {
                
                hanging = true;
                player.stopControllerInput = true;
                player.controller.enabled = false;       
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                player.stopControllerInput = false;
                player.controller.enabled = true;
                transform.position = Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * 100f);
                hanging = false;
                Debug.Log("HIT");
            }
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
