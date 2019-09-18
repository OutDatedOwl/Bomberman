using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller; // Player controller

    public GameObject target; // Target where Player throws bomb

    public float speed; // Speed of Player
    public float jumpForce; // How high Player jumps
    Quaternion rot; // Allow Player to rotate in which direction they face

    Vector3 directionMove; // Input of X and Y axis
    Vector3 launchVector;

    public Animator anim; // Animate Player

    private float inputV;
    private float inputH;

    bool launched = false;
    public bool testGrab = false;
    public bool stopControllerInput = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        DoMove();
        AnimatePlayer();
        Gravity();
        Jump();
        if (launched)
        {
            launchVector = new Vector3(1, -0.02f, 0);
            controller.Move(launchVector * Time.deltaTime * 20); // CHANGE TO IF COLLIDE THEN STOP LAUNCH
        }
        if (!stopControllerInput) // testing grab, switch back to launched after TESTGRAB, lol also FIX LATER cause im making temp solutions....
        {
            controller.Move(directionMove * Time.deltaTime);
        }
    }
    
    // Allow Player to move
    public void DoMove()
    {       
        directionMove = new Vector3(Input.GetAxis("Horizontal") * speed, directionMove.y, Input.GetAxis("Vertical") * speed);
        
        float yStore = directionMove.y;
        directionMove = directionMove.normalized * speed;
        directionMove.y = yStore;

        if (directionMove != new Vector3(0, directionMove.y, 0)) // So Player doesn't snap back to Z axis
        {
            transform.eulerAngles = Vector3.up * Mathf.Atan2(directionMove.x, directionMove.z) * Mathf.Rad2Deg;
        }
    }

    void AnimatePlayer()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

        anim.SetFloat("InputV", inputV);
        anim.SetFloat("InputH", inputH);
    }

    // Apply gravity because we used character controller
    public void Gravity()
    {
        if (controller.isGrounded)
        {
            directionMove.y = -0.5f; // To keep player grounded at all times by minor gravity
        }
        else
        {
            directionMove.y += Physics.gravity.y * Time.deltaTime; // Slowly apply physics as player leaves ground
        }
        Mathf.Clamp(directionMove.y, -10, 10);
    }

    void Jump()
    {
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            directionMove.y = jumpForce;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Launcher")
        {
            launched = true;
        }
        if (col.tag == "Launcher_End")
        {
            launched = false;
        }
    }

}
