using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    int i;
    private Renderer mat;

    void Start()
    {
        mat = this.GetComponent<Renderer>();
    }

    void Update()
    {
        this.gameObject.transform.Rotate(0f, this.transform.position.y, 0f, Space.World);
        if (i < 25)
        {
            i++;
            this.gameObject.transform.localScale += new Vector3(0.12f, 0.12f, 0.12f);
            
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
            newColor.a -= 0.8f * Time.deltaTime;
            mat.material.color = newColor;
            gameObject.GetComponent<Renderer>().material.color = mat.material.color;
        }
    }
}
