using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitros_Spell_Checker : MonoBehaviour
{
    Cube_Test cube_Script;
    BoxCollider boxCollider;
    Vector3 cast_Box_Pos_Cube;
    [HideInInspector]
    public int cardinalDirectionNum;

    void Start()
    {
        cube_Script = FindObjectOfType<Cube_Test>();
        cast_Box_Pos_Cube = cube_Script.cast_Box_Pos;
        boxCollider = cube_Script.boundry.GetComponent<BoxCollider>();
        if (this.transform.position.x > cast_Box_Pos_Cube.x)
        {
            cardinalDirectionNum = 1;        
        }
        if (this.transform.position.x < cast_Box_Pos_Cube.x)
        {
            cardinalDirectionNum = 2;
        }
        if (this.transform.position.z > cast_Box_Pos_Cube.z)
        {
            cardinalDirectionNum = 3;
            this.transform.Rotate(0f, 90f, 0f);
        }
        if (this.transform.position.z < cast_Box_Pos_Cube.z)
        {
            cardinalDirectionNum = 4;
            this.transform.Rotate(0f, 90f, 0f);
        }
    }

    void MoveDirection()
    {
        switch (cardinalDirectionNum)
        {
            case 1:
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                        new Vector3(boxCollider.bounds.max.x, this.transform.position.y, this.transform.position.z), 10f * Time.deltaTime);
                    return;
                }
            case 2:
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                        new Vector3(boxCollider.bounds.min.x, this.transform.position.y, this.transform.position.z), 10f * Time.deltaTime);
                    return;
                }
            case 3:
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                        new Vector3(this.transform.position.x, this.transform.position.y, boxCollider.bounds.max.z), 10f * Time.deltaTime);
                    return;
                }
            case 4:
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position,
                        new Vector3(this.transform.position.x, this.transform.position.y, boxCollider.bounds.min.z), 10f * Time.deltaTime);
                    return;
                }
        }
    }

    void Update()
    {
        MoveDirection();

        if (this.transform.position == new Vector3(boxCollider.bounds.max.x,
            this.transform.position.y, this.transform.position.z))
        {
            Destroy(this.gameObject);
        }
        if (this.transform.position == new Vector3(boxCollider.bounds.min.x,
            this.transform.position.y, this.transform.position.z))
        {
            Destroy(this.gameObject);
        }
        if (this.transform.position == new Vector3(this.transform.position.x,
            this.transform.position.y, boxCollider.bounds.max.z))
        {
            Destroy(this.gameObject);
        }
        if (this.transform.position == new Vector3(this.transform.position.x,
            this.transform.position.y, boxCollider.bounds.min.z))
        {
            Destroy(this.gameObject);
        }
    }
}
