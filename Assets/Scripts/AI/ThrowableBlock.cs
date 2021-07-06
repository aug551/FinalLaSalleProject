using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBlock : MonoBehaviour
{
    public TeleAttackDetect teleAtk;
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
                teleAtk.RemoveEnemyFromList(this.gameObject);
            }
            if (other.CompareTag("Player"))
            {
                return;
            }
            if (other.CompareTag("Wall"))
            {
                explode.Explode();
                teleAtk.RemoveEnemyFromList(this.gameObject);
            }
        }
    }

    private void Start()
    {
        teleAtk = GameObject.FindGameObjectWithTag("Player").GetComponent<TeleAttackDetect>();
        explode = GetComponent<CubeExplode>();
    }
}
