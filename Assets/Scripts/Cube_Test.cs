using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Test : MonoBehaviour
{
    public int tileSizeX, tileSizeY;
    public float padding;
    public GameObject castPrefab, boundry, game_Manager;
    public Vector3 cast_Box_Pos;
    BoxCollider boxCollide;
    Renderer cube_Renderer;
    GameObject obj, cast;
    Nitros_Spell_Checker nitros_Spell_Check;

    void Start()
    {
        boxCollide = boundry.GetComponent<BoxCollider>();
        for (int i = 0; i < tileSizeX; i++)
        {
            for (int j = 0; j < tileSizeY; j++)
            {
                if ((i == tileSizeX / 2 - 1 && j == tileSizeY / 2) || (i == tileSizeX / 2 && j == tileSizeY / 2 + 1))
                { 
                    cast = Instantiate(castPrefab, obj.transform.position + Vector3.up, Quaternion.identity);
                    obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                }
                else if ((i == 0 && j == 0) || (i == tileSizeX - 1 && j == 0)
                    || (i == 0 && j == tileSizeY - 1) || (i == tileSizeX - 1 && j == tileSizeY - 1))
                {
                    obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                }
                else
                    obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube_Renderer = obj.GetComponent<Renderer>();
                Add_Cube_List(i, j);
                //cube_Renderer.material.mainTexture = (Texture)fireBlock;
                obj.transform.position = new Vector3((j - tileSizeX / 2) * padding, 0, (i - tileSizeY / 2) * padding);
                obj.transform.localScale = new Vector3(4f, 1f, 4f);
                if (j == i || (j % 2 == 0 && i % 2 == 0) || (j % 2 == 1 && i % 2 == 1))
                {
                    cube_Renderer.material.color = Color.red;
                }
                if ((j % 2 == 1 && i % 2 == 0) || (j % 2 == 0 && i % 2 == 1))
                {
                    cube_Renderer.material.color = Color.blue;
                }
                obj.transform.SetParent(this.transform);
            }  
        }
        Instantiate(game_Manager, Vector3.zero, Quaternion.identity); 
    }

    void Add_Cube_List(int cubeRow, int cubeColumn)
    {
        //cube_Pos.Add(cubeColumn.ToString());
        obj.name = cubeRow.ToString() + "," + cubeColumn.ToString();
        if ((cubeRow == tileSizeX / 2 - 1 && cubeColumn == tileSizeY / 2 - 1) || (cubeRow == tileSizeX / 2 && cubeColumn == tileSizeY / 2))
        {
            obj.name = "Cast_Cross";
        }
    }

    public void VerticalShoot(Vector3 cast_Position, GameObject cast_FBX)
    {
        cast_Box_Pos = cast_Position;

        Instantiate(cast_FBX, cast_Position + Vector3.right * padding,
       Quaternion.identity);
        nitros_Spell_Check = cast_FBX.GetComponent<Nitros_Spell_Checker>();

        Instantiate(cast_FBX, cast_Position + Vector3.left * padding,
        Quaternion.identity);
        nitros_Spell_Check = cast_FBX.GetComponent<Nitros_Spell_Checker>();

        Instantiate(cast_FBX, cast_Position + Vector3.forward * padding,
       Quaternion.identity);
       nitros_Spell_Check = cast_FBX.GetComponent<Nitros_Spell_Checker>();

        Instantiate(cast_FBX, cast_Position + Vector3.back * padding,
        Quaternion.identity);
        nitros_Spell_Check = cast_FBX.GetComponent<Nitros_Spell_Checker>();
    }
}
