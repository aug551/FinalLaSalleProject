using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth;
    public float currentHealth;
    public GameObject item;
    private Animator animator;

    public float attack = 10;

    void Start()
    {
        MaxHealth = 100;
        currentHealth = MaxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            int generateItem = Random.Range(1, 2);
            animator.Play("Z_FallingBack");
            if (generateItem == 1)
            {
                Instantiate(item, gameObject.transform.position, Quaternion.identity);

            }
            else
            {
                Instantiate(item, gameObject.transform.position, Quaternion.identity);
            }
            Invoke("DestroyEnemy", 1.0f);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
