using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HulkAnimHandler : EnemyAnimHandler
{
    [SerializeField] private Vector3 pushForce = new Vector3(30f, 10f, 0);
    [SerializeField] private float pushDuration = 0.45f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponentInParent<RootMotionCharacterController>().ControlCharacter(
                new Vector3(this.transform.forward.x * pushForce.x, pushForce.y, 0), pushDuration);
        }
    }

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
