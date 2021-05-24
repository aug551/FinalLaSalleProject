using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth;
    public float currentHealth;
    public GameObject item;
    public GameObject zombie;

    public float attack = 10;

    void Start()
    {
        MaxHealth = 100;
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Instantiate(item, zombie.transform.position, Quaternion.identity);
        }
    }
}
