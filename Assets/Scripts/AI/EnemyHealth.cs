using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;
    public GameObject item;
    public GameObject zombie;

    void Start()
    {
        MaxHealth = 100;
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Instantiate(item, zombie.transform.position, Quaternion.identity);
        }
    }
}
