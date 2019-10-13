using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBomb_Collide : MonoBehaviour
{
    public GameObject explosionPrefab, smokePrefab, shockWavePrefab;
    private GameObject explosion, smoke, shockWave;
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
            explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
            smoke = Instantiate(smokePrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
            shockWave = Instantiate(shockWavePrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
            //ParticleSystem parts = explosionEffect.GetComponent<ParticleSystem>();
            //float totalDuration = parts.duration + parts.startLifetime;
            Destroy(explosion, 1.5f);
            Destroy(smoke, 1.5f);
            Destroy(shockWave, 1.5f);
        }
    }
}
