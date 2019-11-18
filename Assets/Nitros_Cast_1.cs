using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitros_Cast_1 : MonoBehaviour
{
    public GameObject cast_Diamond;
    Nitros_Find nitros_Movement;
    Cube_Test nitros_Cast_Vertical;
    Game_Manager game_Manage;
    //public bool spellCastCooldown, nitros_In_Cast_Block;
    public bool nitros_In_Cast_Block;

    void Start()
    {
        nitros_Cast_Vertical = FindObjectOfType<Cube_Test>();
        nitros_Movement = FindObjectOfType<Nitros_Find>();
        game_Manage = FindObjectOfType<Game_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(nitros_Movement.transform.position);
        if (other.tag == "Nitros" && !game_Manage.spellCastCoolDown)
        {
            nitros_In_Cast_Block = true;

            game_Manage.spellCastCoolDown = true;
            //spellCastCooldown = true;
            Cast_Vetical();
        }
    }

    void Cast_Vetical()
    {
        nitros_Movement.hitCastBlock = true;
        nitros_Cast_Vertical.VerticalShoot(this.transform.position, cast_Diamond);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawRay(this.transform.position, Vector3.up * 5f);
    }
}
