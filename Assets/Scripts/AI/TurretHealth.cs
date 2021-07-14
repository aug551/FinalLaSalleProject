using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : EnemyHealth
{
    void Start()
    {
        MaxHealth = 100;
        currentHealth = MaxHealth;
        attack = 10;
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GenerateItem();
            Destroy(gameObject);
        }
    }
}
