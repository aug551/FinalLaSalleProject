using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    public float arrowDamage = 1.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterHealth health = other.gameObject.GetComponentInParent<CharacterHealth>();
            health.TakeDamage(arrowDamage);
            Destroy(gameObject);
        }
    }
}
