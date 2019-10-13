using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_One : MonoBehaviour
{
    [SerializeField]
    Transform player;

    private void Update()
    {
        transform.LookAt(player.position);
    }
}
