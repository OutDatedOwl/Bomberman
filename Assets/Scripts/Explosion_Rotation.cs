using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Rotation : MonoBehaviour
{
    int i;
    private Renderer mat;

    void Start()
    {
        mat = this.GetComponent<Renderer>();
    }

    void Update()
    {
        this.gameObject.transform.Rotate(this.transform.position.x, this.transform.position.y, this.transform.position.z, Space.World);
        if (i < 100)
        {
            i++;
            this.gameObject.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);

        }
        //Debug.Log(mat.material.color.a);
        FadeOut();
        //Debug.Log(newColor.a);
    }

    void FadeOut()
    {
        Color newColor = mat.material.color;
        if (newColor.a > 0f)
        {
            newColor.a -= 0.5f * Time.deltaTime;
            mat.material.color = newColor;
            gameObject.GetComponent<Renderer>().material.color = mat.material.color;
        }
    }
}
