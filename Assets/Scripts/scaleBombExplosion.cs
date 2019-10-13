using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleBombExplosion : MonoBehaviour
{
    int i;
    private Renderer mat;

    void Start()
    {
        mat = this.GetComponent<Renderer>();
    }

    void Update()
    {
        if (i < 80)
        {
            i++;
            this.gameObject.transform.localScale += new Vector3(0.2f, 0f, 0.2f);

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
            newColor.a -= 0.7f * Time.deltaTime;
            mat.material.color = newColor;
            gameObject.GetComponent<Renderer>().material.color = mat.material.color;
        }
    }
}
