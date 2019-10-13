using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitros_Find_Ticker : MonoBehaviour
{
    Nitros_Find nitros_Find;
    public float ticker;

    private void Start()
    {
        nitros_Find = this.GetComponent<Nitros_Find>();
    }

    void Update()
    {
        if (nitros_Find.distance <= nitros_Find.greenSize)
        {
            if (nitros_Find.transform.position == nitros_Find.chargeSpot)
            {
                ticker += Time.deltaTime;
                if (ticker >= 1f)
                {
                    nitros_Find.insideSlideZone = true;
                    //nitros_Find.Charge();
                }
            }
            if (nitros_Find.transform.position != nitros_Find.chargeSpot)
            {
                ticker = 0;
            }
        }
    }
}
