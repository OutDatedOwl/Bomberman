using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBomb_Collide : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    private GameObject explosionEffect;
    BombToss removeBombFromList;

    private void Start()
    {
        removeBombFromList = FindObjectOfType<BombToss>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall") // If bomb collides with wall then explode
        {
            // NEED TO CALL TO BOMBTOSS AND DESTROY THE FIRST IN INDEX
            Destroy(this.gameObject);
            removeBombFromList.bombAllowance.Remove(this.gameObject);
            explosionEffect = Instantiate(explosionEffectPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
            ParticleSystem parts = explosionEffect.GetComponent<ParticleSystem>();
            float totalDuration = parts.duration + parts.startLifetime;
            Destroy(explosionEffect, totalDuration);
        }
    }
}
