using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitros_Cast : MonoBehaviour
{
    public GameObject cast_Diamond;

    Cube_Test nitros_Cast_Vertical;

    void Start()
    {
        nitros_Cast_Vertical = FindObjectOfType<Cube_Test>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Nitros")
        {
            Cast_Vetical();
        }
    }

    void Cast_Vetical()
    {
        nitros_Cast_Vertical.VerticalShoot(this.transform.position, cast_Diamond);
        //nitros_Cast_Vertical.VerticalShoot(this.transform.position, cast_Diamond);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawRay(this.transform.position, Vector3.up * 5f);
    }
}
