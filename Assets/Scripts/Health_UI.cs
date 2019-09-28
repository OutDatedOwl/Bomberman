using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_UI : MonoBehaviour
{
    Game_Manager HP;
    public int numOfHP;

    public Image[] HP_Array;
    public Sprite fullHP;

    private void Start()
    {
        HP = this.GetComponent<Game_Manager>();
    }

    void Update()
    {
        switch (HP.playerHP)
        {
            case 2:
                HP_Array[HP.playerHP].enabled = false;
                break;
            case 1:
                HP_Array[HP.playerHP].enabled = false;
                break;
            case 0:
                HP_Array[HP.playerHP].enabled = false;
                break;
        }
        
    }
}
