using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newton_Bomb : MonoBehaviour
{
    Vector3 frontRay, backRay, rightRay, leftRay, direction;
    BombToss gameManager;
    Rigidbody body;
    Vector3 vFrom;
    RaycastHit hit;
    Collider boxCollider;
    CharacterController controller;
    Player player;
    GameObject playerController;

    private IEnumerator waitKickTime;

    private bool kickedBombStopTime;

    public float pushPower;

    private void Start()
    {
        gameManager = FindObjectOfType<BombToss>();
        gameManager.bombAllowance.Add(this.gameObject);
        body = this.GetComponent<Rigidbody>();
        boxCollider = this.GetComponent<Collider>();
        playerController = GameObject.FindGameObjectWithTag("Player");
        controller = playerController.GetComponent<CharacterController>();
        player = playerController.GetComponent<Player>();
    }

    private void Update()
    {
        // Raycast for bomb behind this bomb, store back bomb velocity before collision
        if (Physics.Raycast(this.transform.position, -this.transform.forward, out hit, 1f) && hit.collider.tag == "Bomb_Foot")
        {
            vFrom = hit.collider.attachedRigidbody.velocity;
        }

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 1f) && hit.collider.tag == "Bomb_Foot")
        {
            vFrom = hit.collider.attachedRigidbody.velocity;
        }

        if (Physics.Raycast(this.transform.position, -this.transform.right, out hit, 1f) && hit.collider.tag == "Bomb_Foot")
        {
            vFrom = hit.collider.attachedRigidbody.velocity;
        }

        if (Physics.Raycast(this.transform.position, this.transform.right, out hit, 1f) && hit.collider.tag == "Bomb_Foot")
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            boxCollider.isTrigger = false;
            gameManager.insideCollider = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.insideCollider = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            body.velocity = controller.transform.forward * pushPower;
            player.stopControllerInput = true;
            //controller.enabled = false;
            waitKickTime = PlayKickAnimation(0.2f);
            StartCoroutine(waitKickTime);
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

        rightRay = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.color = Color.yellow;
        Vector3 directionRight = transform.TransformDirection(Vector3.right);
        Gizmos.DrawRay(rightRay, directionRight);

        leftRay = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.color = Color.blue;
        Vector3 directionLeft = transform.TransformDirection(-Vector3.right);
        Gizmos.DrawRay(leftRay, directionLeft);
    }

    IEnumerator PlayKickAnimation(float time)
    {
        if (kickedBombStopTime)
            yield break;

        kickedBombStopTime = true;

        yield return new WaitForSeconds(time);
        //controller.enabled = true;
        player.stopControllerInput = false;
        kickedBombStopTime = false;
    }
}
