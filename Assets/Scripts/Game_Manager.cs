using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public int playerHP = 3; // Player HP
    public float resetTime;
    public Transform player;

    float timer, spell_Cast_Cooldown_Timer;

    GameObject enemyObject;

    public AudioClip chickenClip, bombermanHurt;
    Nitros_Find nitros_Movement;

    public bool spellCastCoolDown;

    public AudioSource chickenSource, bombermanSource;

    private void Start()
    {
        //chickenSource.clip = chickenClip;
        nitros_Movement = FindObjectOfType<Nitros_Find>();
    }

    private void Update()
    {
        //Debug.Log(spell_Cast_Cooldown_Timer);

        if (playerHP == 0 || player.position.y <= -8f)
        {
            Invoke("Restart", resetTime);
        }
        if (SceneManager.GetActiveScene().Equals("Nitros"))
        {
            if (nitros_Movement.hitCastBlock == true)
            {
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    nitros_Movement.hitCastBlock = false;
                    timer = 0;
                }
            }
            if (spellCastCoolDown)//nitros_Spell_Cooldown.nitros_In_Cast_Block == true)
            {
                Cooldown_Nitros_Spell_Cast();
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Cooldown_Nitros_Spell_Cast()
    {
        spell_Cast_Cooldown_Timer += Time.deltaTime;
        if (spell_Cast_Cooldown_Timer >= 10f)
        {
            spellCastCoolDown = false;
            spell_Cast_Cooldown_Timer = 0;
        }
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
