using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public int playerHP = 3; // Player HP
    public float resetTime;

    GameObject enemyObject;

    public AudioClip chickenClip, bombermanHurt;

    public AudioSource chickenSource, bombermanSource;

    private void Start()
    {
        //chickenSource.clip = chickenClip;
    }

    private void Update()
    {
        if (playerHP == 0)
        {
            Invoke("Restart", resetTime);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HealthSystem(GameObject thingHit)
    {
        if (thingHit.tag == "Player" && playerHP != 0)
        {
            bombermanSource.Play();
            playerHP = playerHP - 1;
        }

        if (thingHit.tag == "Enemy")
        {
            chickenSource.Play();
            Invoke("EnemyDieSound", 1f);
            enemyObject = thingHit;
        }
    }

    void EnemyDieSound()
    {
        enemyObject.SetActive(false);
        Destroy(enemyObject);
    }

}
