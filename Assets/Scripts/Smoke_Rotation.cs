using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_Rotation : MonoBehaviour
{
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.up, Time.deltaTime * 100);
    }
}
