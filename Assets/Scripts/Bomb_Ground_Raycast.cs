using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Ground_Raycast : MonoBehaviour
{
    private BoxCollider bomb;
    private Rigidbody rb;
    private float distToGround;

    bool groundCheck;

    private void Start()
    {
        bomb = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        distToGround = bomb.bounds.extents.y;

        IsGrounded();
        if (groundCheck)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            groundCheck = true;
            rb.useGravity = false;
            return groundCheck;
        }
        else
        {
            groundCheck = false;
            return groundCheck;
        }
    }
}
