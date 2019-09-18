using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    float timeUntilBoom;

    public GameObject explosionEffectPrefab;
    private GameObject explosionEffect;

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
            explosionEffect = Instantiate(explosionEffectPrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
            ParticleSystem parts = explosionEffect.GetComponent<ParticleSystem>();
            float totalDuration = parts.duration + parts.startLifetime;
            Destroy(explosionEffect, totalDuration);
        }
    }
}
