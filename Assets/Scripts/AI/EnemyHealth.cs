using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth;
    public float currentHealth;
    public GameObject item;
    private Animator animator;
    private Enemy enemy;

    public float attack = 10;

    void Start()
    {
        MaxHealth = 100;
        currentHealth = MaxHealth;
        animator = GetComponent<Animator>();
        enemy = GetComponentInParent<Enemy>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            
            //enemy.AgentRunSpeed = 0; enemy.AgentWalkSpeed = 0;
            animator.Play("Z_FallingBack");
            Instantiate(item, gameObject.transform.position, Quaternion.identity);
            Invoke("DestroyEnemy", 1.0f);
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
