using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitros_Spell_Checker : MonoBehaviour
{
    Cube_Test cube_Script;
    BoxCollider boxCollider;

    void Start()
    {
        cube_Script = FindObjectOfType<Cube_Test>();
        boxCollider = cube_Script.boundry.GetComponent<BoxCollider>();
    }

    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(boxCollider.bounds.max.x + 2, 
            this.transform.position.y, this.transform.position.z), 10f * Time.deltaTime);

        if (this.transform.position == new Vector3(boxCollider.bounds.max.x + 2,
            this.transform.position.y, this.transform.position.z))
        {
            Destroy(this.gameObject);
        }
    }
}
