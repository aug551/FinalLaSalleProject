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

    public float attack = 10;
    public bool item = false;

    void Start()
    {
        MaxHealth = 100;
        currentHealth = MaxHealth;
        //animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            int generateItem = Random.Range(1, 3);
            Debug.Log(generateItem);
            if (generateItem == 1)
            {
                Instantiate(itemLeather, gameObject.transform.position, Quaternion.identity);

            }
            else
            {
                Instantiate(itemString, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
