using UnityEngine;

public class Chicken_Move : MonoBehaviour
{
    [SerializeField]
    Transform player; // Player position

    public BoxCollider chickenBox; // Hitbox of chicken

    private Rigidbody rb; // Rigidbody of chicken

    private float distToGround; // Checking how far chicken is from ground for raycast to jump again
    private float distanceToPlayer; // How far is Player from chicken

    private bool groundCheck = true; // Is chicken grounded?

    private void Start()
    {

        rb = this.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("No Rigidbody");
        }

        distToGround = chickenBox.bounds.extents.y; // Distance of chicken to ground is casted off of bottom of hitbox 
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(player.position, rb.position);

        IsGrounded();

        if (distanceToPlayer < 15f) // If Player is within attack range then set destination to Player
        {
            FacePlayer();

            if (groundCheck) // Grounded?
            {
                Invoke("JumpChicken", 1);
            }
        }
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            groundCheck = true;
            return groundCheck;
        }
        else
        {
            groundCheck = false;
            return groundCheck;
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void JumpChicken()
    {
        rb.AddForce(transform.forward.x * 3, 9, transform.forward.z * 3);
    }
}
