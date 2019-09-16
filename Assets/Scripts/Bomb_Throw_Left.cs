using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Throw_Left : MonoBehaviour
{
    // Bomb and the target to where the bomb is thrown
    public Rigidbody bomb;
    public GameObject target;

    bool bombLaunched;

    // Variables for bomb launch
    public float h;
    public float gravity = -18;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target_Left");
        //GameObject throwExtraBomb = GameObject.FindGameObjectWithTag("Bomb");
    }

    // Update is called once per frame
    void Update()
    {
        if (bombLaunched && bomb == null)
        {
            bombLaunched = false;
        }

        if (!bombLaunched)
        {
            Launch();
        }
        bombLaunched = true;
    }

    public void Launch()
    {
        bomb.velocity = CalculateLaunchVelocity();
    }

    Vector3 CalculateLaunchVelocity()
    {
        float displacementY = target.transform.position.y - bomb.position.y;
        Vector3 displacementXZ = new Vector3(target.transform.position.x - bomb.position.x, 0, target.transform.position.z - bomb.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt((-2 * displacementY) / gravity));

        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }
}
