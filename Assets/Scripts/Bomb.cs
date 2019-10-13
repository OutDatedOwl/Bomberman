using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Bomb and the target to where the bomb is thrown
    public Rigidbody bomb;
    public GameObject target;

    // Position for holding bomb
    private GameObject holdBomb;
    private GameObject charController;
    private GameObject playerController;

    bool bombLaunched;

    private CharacterController controller;
    //private Player player;
    private textMoveChar player;

    // Variables for bomb launch
    public float h;
    public float gravity = -18;

    // Finding the game objects on Player
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");
        holdBomb = GameObject.FindGameObjectWithTag("Hands");
        charController = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player");
        controller = charController.GetComponent<CharacterController>();
        player = playerController.GetComponent<textMoveChar>();
    }

    // FIX THE THROWNBOMB BOOLEAN, TRY TO CREATE SECOND BOMB IN SAME POSITION BUT THROW THAT INSTEAD OF ONE BEING HELD
    private void LateUpdate()
    {
        if (bombLaunched && bomb == null) // When bomb is destroyed, can throw new bomb
        {
            bombLaunched = false;
        }

        if (Input.GetKey(KeyCode.R)) // Press and hold R to hold bomb
        {
            if (!bombLaunched)
            {
                IsHolding();
            }
            
        }
        if (Input.GetKeyUp(KeyCode.R)) // Release R to throw bomb
        {
            bomb.isKinematic = false; // Make bomb non-kinematic so it can be launched
            if (!bombLaunched)
            {
                Launch();
                player.slowSpeedAfterThrow = true;
                //player.stopControllerInput = true;
                //controller.enabled = false;
                //waitKickTime = PlayKickAnimation(0.2f);
                //StartCoroutine(waitKickTime);
            }  
            bombLaunched = true;
        }
    }

    void IsHolding() // Hold bomb method to keep bomb near Player when running or jumping
    {
        bomb.isKinematic = true;
        bomb.position = holdBomb.transform.position;
    }

    public void Launch() // Launch bomb using the parabola calculation
    {
        bomb.velocity = CalculateLaunchVelocity();
    }

    // Parabola calculations
    Vector3 CalculateLaunchVelocity()
    {
        float displacementY = target.transform.position.y - bomb.position.y;
        Vector3 displacementXZ = new Vector3(target.transform.position.x - bomb.position.x, 0, target.transform.position.z - bomb.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt((-2 * displacementY) / gravity));

        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }
    /*
    IEnumerator PlayKickAnimation(float time)
    {
        if (kickedBombStopTime)
            yield break;

        kickedBombStopTime = true;

        yield return new WaitForSeconds(time);
        player.speed += 5;
        //player.stopControllerInput = false;
        //controller.enabled = true;
        kickedBombStopTime = false;
    }
    */
}
