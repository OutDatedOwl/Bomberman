using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Test : MonoBehaviour
{
    public int tileSizeX, tileSizeY;
    public float padding;
    public GameObject castPrefab, boundry;
    BoxCollider boxCollide;
    Renderer cube_Renderer;
    GameObject obj, cast, leftCrystal, rightCrystal, forwardCrystal, backCrystal;
    //bool outOfBoundsMaxX, outOfBoundsMaxZ, outOfBoundsMinX, outOfBoundsMinZ;
    //List<string> cube_Pos;

    void Start()
    {
        //cube_Pos = new List<string>();
        //Instantiate(boundry, new Vector3(-2f, 0, -2f), Quaternion.identity);
        boxCollide = boundry.GetComponent<BoxCollider>();
        for (int i = 0; i < tileSizeX; i++)
        {
            for (int j = 0; j < tileSizeY; j++)
            {
                if ((i == tileSizeX / 2 - 1 && j == tileSizeY / 2) || (i == tileSizeX / 2 && j == tileSizeY / 2 + 1))
                {
                    //obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    //obj.AddComponent<>();    
                    cast = Instantiate(castPrefab, obj.transform.position + Vector3.up, Quaternion.identity);
                    obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //cast = Instantiate(castPrefab, obj.transform.position + Vector3.up, Quaternion.identity);
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
                //print(cube_Pos[i] + "," + cube_Pos[j]);
            }  
        }
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
        Instantiate(cast_FBX, cast_Position + Vector3.right * padding,
       Quaternion.identity);

        /*
        rightCrystal = Instantiate(cast_FBX, cast_Position + Vector3.right * padding,
       Quaternion.Euler(-141.888f, -2.924988f, -45f));
        
        if (rightCrystal.transform.position == cast_Position + Vector3.up)
        {
            rightCrystal = Instantiate(cast_FBX, cast_Position + Vector3.right * padding,
       Quaternion.Euler(-141.888f, -2.924988f, -45f));

            rightCrystal.transform.position = Vector3.Lerp(rightCrystal.transform.position, Vector3.up, 3f * Time.deltaTime);
        }
        
        if (!outOfBoundsMaxX)
        {
            rightCrystal = Instantiate(cast_FBX, cast_Position + Vector3.up + Vector3.right * padding,
       Quaternion.Euler(-141.888f, -2.924988f, -45f));
        }
        
        if (rightCrystal.transform.position.x >= boxCollide.bounds.max.x - padding)
        {
            outOfBoundsMaxX = true;
        } */
    }
}
