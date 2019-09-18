using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot_Bomb : MonoBehaviour
{
    Vector3 pushForce;
    Rigidbody body;
    public float pushPower;
    private CharacterController controller;
    Player player;

    private bool kickedBombStopTime;

    private IEnumerator waitKickTime;

    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
        player = this.GetComponent<Player>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3) // So we don't hit bomb down
        {
            return;
        }

        if (hit.collider.tag == "Bomb_Foot")
        {
            pushForce = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            body.velocity = pushForce * pushPower;
            player.stopControllerInput = true;
            controller.enabled = false;
            waitKickTime = PlayKickAnimation(0.2f);
            StartCoroutine(waitKickTime);
            //pushForce = hit.controller.velocity.normalized * pushPower; // Hit bomb according to Player speed and public float of pushPower
            //pushForce = pushForce.normalized * pushPower;
        }
        //body.velocity = pushForce * pushPower;
        //body.AddForceAtPosition(pushForce, hit.point);
        //print(body.velocity.normalized);
    }

    IEnumerator PlayKickAnimation(float time)
    {
        if (kickedBombStopTime)
            yield break;

        kickedBombStopTime = true;

        yield return new WaitForSeconds(time);
        controller.enabled = true;
        player.stopControllerInput = false;
        kickedBombStopTime = false;
    }
}
