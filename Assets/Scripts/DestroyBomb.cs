using UnityEngine;
using System.Linq;

public class DestroyBomb : MonoBehaviour
{
    public float radius; // Radius of bomb explosion
    public GameObject explosionEffectPrefab; // Prefab of bomb
    private GameObject explosionEffect; // Clone of prefab so we don't destroy prefab

    string[] bombTags = {"Bomb", "Bomb_Left", "Bomb_Right"};

    private void OnTriggerEnter(Collider other)
    {
        if (bombTags.Contains(other.gameObject.tag)) // If the collider that hit object is a bomb then run code
        {
            /*
            Collider[] colliders= Physics.OverlapSphere(other.transform.position, radius); // Destroy enemies in this radius, not yet working
            foreach (Collider nearbyObject in colliders)
            {

            }
            */

            Destroy(other.gameObject);
            explosionEffect = Instantiate(explosionEffectPrefab, other.transform.position, Quaternion.Euler(0, 0, 0));
            ParticleSystem parts = explosionEffect.GetComponent<ParticleSystem>();
            float totalDuration = parts.duration + parts.startLifetime;
            Destroy(explosionEffect, totalDuration);
        }
    }
}
