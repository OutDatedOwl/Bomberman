using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newton_Bomb : MonoBehaviour
{
    Vector3 frontRay, backRay, direction;
    BombToss gameManager;
    Rigidbody body;
    Vector3 vFrom;
    RaycastHit hit;

    private void Start()
    {
        gameManager = FindObjectOfType<BombToss>();
        gameManager.bombAllowance.Add(this.gameObject);
        body = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Raycast for bomb behind this bomb, store back bomb velocity before collision
        if (Physics.Raycast(this.transform.position, -this.transform.forward, out hit, 1f) && hit.collider.tag == "Bomb_Foot")
        {
            vFrom = hit.collider.attachedRigidbody.velocity;
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.collider.tag == "Bomb_Foot") // If object collided is a bomb then transfer velocity from back bomb to this bomb
        {
            body.velocity = vFrom;
        }
    }

    void OnDrawGizmosSelected()
    {
        frontRay = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.color = Color.red;
        Vector3 directionFront = transform.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(frontRay, directionFront);

        backRay = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.color = Color.green;
        Vector3 directionBack = transform.TransformDirection(-Vector3.forward);
        Gizmos.DrawRay(frontRay, directionBack);
    }

}
