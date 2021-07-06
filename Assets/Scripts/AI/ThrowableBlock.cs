using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBlock : MonoBehaviour
{
    public CubeExplode explode;
    public bool fired = false;
    public Vector3 currentForce;

    private void OnTriggerEnter(Collider other)
    {
        if (fired)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyHealth>().TakeDamage(60);
                explode.Explode();
            }
            if (other.CompareTag("Player"))
            {
                return;
            }
            if (other.CompareTag("Wall"))
            {
                explode.Explode();
            }
        }
    }

    private void Start()
    {
        explode = GetComponent<CubeExplode>();
    }
}
