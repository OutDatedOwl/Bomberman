using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge_Checker : MonoBehaviour
{
    Vector3 landPoint, origin;
    Vector3 dir, lerpDirection;
    Vector3 direction, wallGrab;
    Vector3 grabLedgePoint, ledge_Check;

    Collider collider;

    [SerializeField]
    Transform hands;

    public bool hanging;

    bool holding;
    bool climbing;
    bool landPointMarked;

    Player player;
    CharacterController controller;

    int layer_mask;
    int playerGrabSide;

    RaycastHit hit;

    private void Start()
    {
        player = this.GetComponent<Player>();
        controller = this.GetComponent<CharacterController>();
        layer_mask = LayerMask.GetMask("Environment");
    }

    private void Update()
    {
        origin = transform.position;
        dir = transform.up + transform.forward / 4;
        ledge_Check = -transform.forward / 4 + -transform.up / 6;
        origin.y += 1.4f;
        if (!hanging) // Looking for a ledge to grab
        {
            if (Physics.Raycast(origin, this.transform.forward, out hit, 0.75f, layer_mask))
            {
                collider = hit.transform.GetComponent<Collider>();
                LocatePlayerPosition(collider);
                holding = true;
                GrabOntoLedge();
                FaceWall();
                hanging = true;
                player.stopControllerInput = true;
                //player.controller.enabled = false;   
            }
        }
        if (hanging)
        {           
            if (Physics.Raycast(hands.position + dir * 1.5f, -Vector3.up, out hit, 1.7f, layer_mask)) // Casting ray down for plateau
            {
                MarkLandPoint();
                lerpDirection = new Vector3(transform.position.x, hit.point.y, transform.position.z) + transform.forward * 3/4;
                if (Input.anyKeyDown)
                {
                    climbing = true;
                }        
            }
            if (climbing)
            {
                transform.position = Vector3.Lerp(transform.position, lerpDirection, 3.5f * Time.deltaTime);
                if (Vector3.Distance(transform.position, lerpDirection) <= 0.2f)
                {
                    hanging = false;
                    climbing = false;
                    holding = false;
                    player.stopControllerInput = false;
                    //player.controller.enabled = true;
                    landPointMarked = false;
                } 
            }
        }        
    }

    private void FixedUpdate()
    {
        if (!Physics.Raycast(hands.position + ledge_Check + controller.velocity * Time.deltaTime, Vector3.down, out hit, 1f, layer_mask)
            && Physics.Raycast(transform.position + Vector3.up / 2, Vector3.down, out hit, 1f, layer_mask))
        {
            //FallOffLedge();
        }
    }

    void FallOffLedge()
    {
        
    }

    void LocatePlayerPosition(Collider boxSide)
    {
        if (hit.normal == Vector3.back) // From South
        {
            playerGrabSide = 1;
        }
        if (hit.normal == Vector3.forward) // From North
        {
            playerGrabSide = 2;
        }
        if (hit.normal == Vector3.left) // From West
        {
            playerGrabSide = 3;
        }
        if (hit.normal == Vector3.right) // From East
        {
            playerGrabSide = 4;
        }

        switch (playerGrabSide)
        {
            case 1:
                grabLedgePoint = new Vector3(transform.position.x, boxSide.bounds.max.y - 1.50f, boxSide.bounds.min.z - 0.45f);
                break;
            case 2:
                grabLedgePoint = new Vector3(transform.position.x, boxSide.bounds.max.y - 1.50f, boxSide.bounds.max.z + 0.45f);
                break;
            case 3:
                grabLedgePoint = new Vector3(boxSide.bounds.min.x - 0.45f, boxSide.bounds.max.y - 1.50f, transform.position.z);
                break;
            case 4:
                grabLedgePoint = new Vector3(boxSide.bounds.max.x + 0.45f, boxSide.bounds.max.y - 1.50f, transform.position.z);
                break;
        }

        
    }

    void GrabOntoLedge()
    {
        if (holding)
        {
            transform.position = Vector3.MoveTowards(transform.position, grabLedgePoint, 2f);
        }
    }

    void FaceWall()
    {
        transform.rotation = Quaternion.LookRotation(-hit.normal);
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
        direction = transform.TransformDirection(Vector3.forward * 3/4);
        Gizmos.DrawRay(origin, direction);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(hands.position + dir * 1.5f, -Vector3.up * 1.7f);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(hands.position + ledge_Check, Vector3.down * 1.35f);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up / 2, Vector3.down);

        if (collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(collider.bounds.center, Vector3.up * 1.75f);
        }
    }
}
