using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private RootMotionCharacterController rmcc;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && rmcc.IsAttacking)
        {
            // Do damage stuff here

            if (other.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy");
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rmcc = GetComponentInParent<RootMotionCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
