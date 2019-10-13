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

    float timer;
    public float accel;

    Vector3 directionMove; // Input of X and Y axis
    Vector3 velocity, velocityXZ;
    Vector3 launchVector;

    Ledge_Checker ledgeGrab;

    public Animator anim; // Animate Player

    private float inputV;
    private float inputH;

    bool launched = false;
    //public bool testGrab = false;
    public bool stopControllerInput = false;
    public bool slowSpeedAfterThrow;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        ledgeGrab = this.GetComponent<Ledge_Checker>();
    }

    void Update()
    {
        DoMove();
        AnimatePlayer();
        Gravity();
        Jump();
        SlowSpeed();
        //if (launched)
        //{
        //    launchVector = new Vector3(1, -0.02f, 0);
        //    controller.Move(launchVector * Time.deltaTime * 20); // CHANGE TO IF COLLIDE THEN STOP LAUNCH
        //}
        if (!stopControllerInput) // testing grab, switch back to launched after TESTGRAB, lol also FIX LATER cause im making temp solutions....
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }

    // Allow Player to move
    public void DoMove()
    {       
        directionMove = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        directionMove = Vector3.ClampMagnitude(directionMove, 1);

        //float yStore = directionMove.y;
        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, directionMove * speed, accel * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
        //directionMove.y = yStore;

        if (directionMove != new Vector3(0, directionMove.y, 0) && !ledgeGrab.hanging) // So Player doesn't snap back to Z axis
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
            velocity.y = -0.5f; // To keep player grounded at all times by minor gravity
        }
        else
        {
            velocity.y += Physics.gravity.y * Time.deltaTime; // Slowly apply physics as player leaves ground
        }
        Mathf.Clamp(velocity.y, -10, 10);
    }

    void Jump()
    {
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }
    }

    void SlowSpeed()
    {
        if (slowSpeedAfterThrow)
        {
            timer += Time.deltaTime;
            if (timer <= 0.1f && speed >= 4f)
            {
                speed -= 1;
            }
            if (timer >= 0.2f)
            {
                speed += 1;
            }
            if (speed >= 9f)
            {
                speed = 9f;
                timer = 0;
                slowSpeedAfterThrow = false;
            }
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
