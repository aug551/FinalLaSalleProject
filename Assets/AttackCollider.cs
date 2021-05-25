using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private RootMotionCharacterController rmcc;
    private CharacterStats characterStats;
    private EnemyHealth enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && rmcc.IsAttacking)
        {
            // Do damage stuff here

            if (other.CompareTag("Enemy"))
            {
                enemy = other.gameObject.GetComponentInParent<EnemyHealth>();
                Debug.Log(enemy.currentHealth);
                enemy.TakeDamage(characterStats.attack);
                Debug.Log("Hit enemy");
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rmcc = GetComponentInParent<RootMotionCharacterController>();
        characterStats = GetComponentInParent<CharacterStats>();
        Debug.Log(characterStats.attack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
