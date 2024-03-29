﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Game_Manager game_Manager;

    private void Start()
    {
        GameObject manageGame = GameObject.FindGameObjectWithTag("Game_Manager");
        game_Manager = manageGame.GetComponent<Game_Manager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy") // If enemy hits player
        {
            game_Manager.HealthSystem(this.gameObject);
        }

        if (collision.collider.tag == "Bomb_Foot" && transform.tag != "Player") // If kickbomb hits enemy and isn't player
        {
            game_Manager.HealthSystem(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Explosion")
        {
            game_Manager.HealthSystem(this.gameObject);
        }
    }
}
