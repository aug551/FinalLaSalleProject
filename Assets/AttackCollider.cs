using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private RootMotionCharacterController rmcc;
    private CharacterStats characterStats;
    private List<EnemyHealth> enemy = new List<EnemyHealth>();

    public List<EnemyHealth> Enemy { get => enemy; set => enemy = value; }
    public CharacterStats CharacterStats { get => characterStats; set => characterStats = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && other != null)
        {
            // Do damage stuff here

            if (other.CompareTag("Enemy"))
            {
                if (!enemy.Contains(other.gameObject.GetComponentInParent<EnemyHealth>()))
                {
                    enemy.Add(other.gameObject.GetComponentInParent<EnemyHealth>());

                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rmcc = GetComponentInParent<RootMotionCharacterController>();
        characterStats = GetComponentInParent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
