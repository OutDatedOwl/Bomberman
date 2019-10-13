using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    float timeUntilBoom;

    public GameObject explosionPrefab, smokePrefab, shockWavePrefab;
    private GameObject explosion, smoke, shockWave;

    BombToss removeBombFromList;

    private void Start()
    {
        removeBombFromList = FindObjectOfType<BombToss>();
    }

    private void Update()
    {
        timeUntilBoom += Time.deltaTime;
        if (this.gameObject != null && timeUntilBoom >= 3f)
        {
            timeUntilBoom = 0;
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
