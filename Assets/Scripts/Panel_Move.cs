using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Move : MonoBehaviour
{
    [SerializeField]
    List<WayPoints> wayPoints;
    [SerializeField]
    Transform platform;
    int wayPointsIndex = 0;
    public float speed;
    private Transform playerSpeed;

    void Update()
    {
        
        if (platform.position == wayPoints[wayPointsIndex].transform.position)
        {

            wayPointsIndex++;
            if (wayPointsIndex == wayPoints.Count)
            {
                wayPointsIndex = 0;
            }
            
        }

        platform.position = Vector3.MoveTowards(platform.position, wayPoints[wayPointsIndex].transform.position, Time.deltaTime * speed);

    }
}
