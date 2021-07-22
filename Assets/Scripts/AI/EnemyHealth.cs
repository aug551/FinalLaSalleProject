using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth;
    public float currentHealth;
    public GameObject itemLeather;
    public GameObject itemString;
    private Animator animator;

    public float attack;
    public bool item = false;

    void Start()
    {
        MaxHealth = 100;
        currentHealth = MaxHealth;
        attack = 10;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GenerateItem();
            Destroy(gameObject);
        }
    }

    public void GenerateItem()
    {
        int generateItem = Random.Range(1, 3);
        if (generateItem == 1)
        {
            Instantiate(itemLeather, gameObject.transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(itemString, gameObject.transform.position, Quaternion.identity);
        }
    }

    public void GetStats()
    {
        
    }
}
