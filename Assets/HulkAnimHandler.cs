using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HulkAnimHandler : EnemyAnimHandler
{


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();

        // Initialize the variables.
        isAttacking = false;
        isPatrolling = false;
        isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        SyncVariables();

        if (isPatrolling)
        {
            anim.SetBool("Detect", false);
        }
        if (isChasing)
        {
            anim.SetBool("Detect", true);
        }
    }


}
