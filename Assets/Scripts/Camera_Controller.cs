using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform player;
    public Vector3 offSet;
    float tilt = 25f;

    private void LateUpdate()
    {
        transform.position = player.position + offSet;
        transform.rotation = Quaternion.Euler(tilt, transform.rotation.y, transform.rotation.z);
    }
}
