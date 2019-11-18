using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Nitros : MonoBehaviour
{
    public Transform player, nitros, anchorPoint, playerRotationPoint, nitrosRotationPoint;
    //public Vector3 offSet;
    Vector3 direction_Nitros, direction_Player, direction_AnchorPoint,camera_Pos, set_Pos, set_Anchor_Pos, anchorPos;
    [SerializeField]
    float distance, distanceAnchor, height, cameraRotation, cameraRotationSwivel;
    Quaternion lookRotation;

    private void LateUpdate()
    {
        playerRotationPoint.position = player.position + Vector3.up * 2;
        nitrosRotationPoint.position = nitros.position;

        // NITROS TO PLAYER POS
        direction_Nitros.x = (player.position.x - nitros.position.x); // get the direction
        direction_Nitros.z = (player.position.z - nitros.position.z); // get the direction
        direction_Nitros.Normalize();
        camera_Pos = player.position - (direction_Nitros * distance); // get the point a little past the player
        set_Pos.x = camera_Pos.x; // dont touch Y axis so camera doesnt go below certain point
        set_Pos.z = camera_Pos.z;

        // ANCHOR POS
        //direction_AnchorPoint.x = (anchorPoint.position.x - player.position.x);
        //direction_AnchorPoint.z = (anchorPoint.position.z - player.position.z);
        //direction_AnchorPoint.Normalize();
        anchorPos = player.position - (playerRotationPoint.forward * distance);
        set_Anchor_Pos.x = anchorPos.x;
        set_Anchor_Pos.z = anchorPos.z;

        // CAMERA POS TO ANCHOR POS
        /*
        direction_Player.x = (player.position.x - anchorPoint.position.x); // get the direction
        direction_Player.z = (player.position.z - anchorPoint.position.z);
        direction_Player.Normalize();
        camera_Pos = player.position + (direction_Player * distance);
        set_Pos.x = camera_Pos.x;
        set_Pos.z = camera_Pos.z;
        */

        // CAMERA POS TO ROTATION POS
        //direction_AnchorPoint = (player.position - transform.position);
        //camera_Pos = player.position + (direction_AnchorPoint * distance);
        //direction_AnchorPoint.Normalize();
        //set_Pos.x = camera_Pos.x;
        //set_Pos.z = camera_Pos.z;

        lookRotation = Quaternion.LookRotation(-direction_Nitros);

        transform.position = Vector3.Lerp(transform.position, set_Anchor_Pos + Vector3.up * height, Time.deltaTime * cameraRotation);
        anchorPoint.position = Vector3.Lerp(anchorPoint.position, set_Pos, Time.deltaTime * cameraRotation);
        nitrosRotationPoint.rotation = Quaternion.Slerp(nitrosRotationPoint.rotation, lookRotation, Time.deltaTime * cameraRotationSwivel);
        playerRotationPoint.rotation = Quaternion.Slerp(playerRotationPoint.rotation, nitrosRotationPoint.rotation, Time.deltaTime * cameraRotationSwivel);
        //transform.position = Vector3.Lerp(transform.position, direction_Nitros + Vector3.up * height, Time.deltaTime * cameraRotation);

        /*
        if (Vector3.Distance(player.transform.position, nitros.transform.position) > 7f)
        {
            //transform.position = Vector3.Lerp(transform.position, set_Pos + Vector3.up * height,
                //Time.deltaTime * Vector3.Distance(player.transform.position, nitros.transform.position)); // set camera at set_Pos position
            nitrosRotationPoint.rotation = Quaternion.Slerp(nitrosRotationPoint.rotation, lookRotation, Time.deltaTime * cameraRotationSwivel);
        }
        else
        {
            //transform.position = Vector3.Lerp(transform.position, set_Pos + Vector3.up * height, 
                //Time.deltaTime * Vector3.Distance(player.transform.position, nitros.transform.position));
            nitrosRotationPoint.rotation = Quaternion.Slerp(nitrosRotationPoint.rotation, lookRotation, Time.deltaTime * cameraRotation);
        }

        */
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotationPoint.rotation, Time.deltaTime * cameraRotationSwivel);
        //transform.LookAt(nitros.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(nitros.position, player.position + Vector3.up * 2);

        //Gizmos.DrawRay(nitrosRotationPoint.position + Vector3.up * 2f, nitrosRotationPoint.forward * 10f);
        //Gizmos.DrawLine(player.position + Vector3.up * 2, transform.position);
        Gizmos.DrawRay(playerRotationPoint.position, playerRotationPoint.forward * 10f);
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawLine(anchorPoint.position, new Vector3(camera_Pos.x, height, camera_Pos.z));
    }
}
