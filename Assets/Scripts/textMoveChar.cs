using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textMoveChar : MonoBehaviour
{
    public CharacterController controller; // Player controller
    public Transform camera_Direction, feet;
    //public GameObject running_Dust_Particles_Prefab;

    public float speed; // Speed of Player
    public float jumpForce; // How high Player jumps
    public float accel;

    float timer;

    Vector3 directionMove; // Input of X and Y axis
    Vector3 velocity, velocityXZ;
    Vector3 launchVector;

    public Animator anim; // Animate Player

    private float inputV;
    private float inputH;

    //private GameObject running_Dust_Particles;

    [HideInInspector]
    public bool stopControllerInput = false;
    [HideInInspector]
    public bool slowSpeedAfterThrow;

    bool jumpedUp = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        //running_Dust_Particles = Instantiate(running_Dust_Particles_Prefab, feet.transform.position + Vector3.back, Quaternion.identity);
        //running_Dust_Particles.transform.SetParent(feet.transform);
    }

    // Update is called once per frame
    void Update()
    {
        DoMove();
        AnimatePlayer();
        Gravity();
        Jump();
        SlowSpeed();

        if (!stopControllerInput) // testing grab, switch back to launched after TESTGRAB, lol also FIX LATER cause im making temp solutions....
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void DoMove()
    {
        directionMove = (camera_Direction.right * Input.GetAxis("Horizontal")) + (camera_Direction.forward * Input.GetAxis("Vertical")); 
        //new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        directionMove = Vector3.ClampMagnitude(directionMove, 1);

        //float yStore = directionMove.y;
        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, directionMove * speed, accel * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);
        //directionMove.y = yStore;

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
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Jump", true);
            velocity.y = jumpForce;
            jumpedUp = true;
        }
        if (anim.GetBool("Jump"))
        {
            anim.SetBool("Jumping", true);
        }
        if (controller.isGrounded && anim.GetBool("Jumping"))
        {
            anim.SetBool("Jumping", false);
            jumpedUp = false;
        }
        else
            anim.SetBool("Jump", false);
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

}
