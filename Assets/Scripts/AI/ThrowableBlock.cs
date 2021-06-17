using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBlock : MonoBehaviour
{
    public TeleAttackDetect teleAtk;
    public bool fired = false;

    private void OnTriggerEnter(Collider other)
    {
        if (fired)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyHealth>().TakeDamage(60);
                Destroy(gameObject);
            }
            if (other.CompareTag("Player"))
            {
                return;
            }
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        teleAtk = GameObject.FindGameObjectWithTag("Player").GetComponent<TeleAttackDetect>();
    }
}
