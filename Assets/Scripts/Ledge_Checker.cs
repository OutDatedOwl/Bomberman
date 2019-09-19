using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge_Checker : MonoBehaviour
{
    Vector3 wallPoint, landPoint, origin;
    Vector3 dir;
    Vector3 direction;

    [SerializeField]
    Transform hands;

    bool hanging;
    bool climbing;
    bool landPointMarked;

    Player player;

    int layer_mask;

    RaycastHit hit;

    private void Start()
    {
        player = this.GetComponent<Player>();
        layer_mask = LayerMask.GetMask("Environment");
    }

    private void Update()
    {
        origin = transform.position;
        dir = transform.up + transform.forward / 4;
        origin.y += 1.4f;
        if (!hanging) // Looking for a ledge to grab
        {
            if (Physics.Raycast(origin, this.transform.forward, out hit, 1f, layer_mask))
            {
                Debug.Log(hit.transform.localScale * ((BoxCollider)hit.collider).size.x);
                Debug.Log(hit.transform.localScale * ((BoxCollider)hit.collider).size.y);
                Debug.Log(hit.transform.localScale * ((BoxCollider)hit.collider).size.z);
                wallPoint = hit.point;
                hanging = true;
                player.stopControllerInput = true;
                player.controller.enabled = false;
            }
        }
        if (hanging)
        {           
            if (Physics.Raycast(hands.position + dir * 1.5f, -Vector3.up, out hit, 1.7f, layer_mask)) // Casting ray down for plateau
            {
                MarkLandPoint();
                if (Input.anyKeyDown)
                {
                    climbing = true;
                }        
            }
            if (climbing)
            {
                transform.position = Vector3.Lerp(transform.position, landPoint, 3.5f * Time.deltaTime);
                if (Vector3.Distance(transform.position, landPoint) <= 0.2f)
                {
                    hanging = false;
                    climbing = false;
                    player.stopControllerInput = false;
                    player.controller.enabled = true;
                    landPointMarked = false;
                } 
            }
        }        
    }

    void MarkLandPoint()
    {
        if (!landPointMarked)
        {
            landPoint = hit.point;
            landPointMarked = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        direction = transform.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(origin, direction);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(hands.position + dir * 1.5f, -Vector3.up);
    }
}
