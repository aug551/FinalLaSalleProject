using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : NPC
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    //Attack variables
    [Header("Attack variables")]
    [SerializeField] float aggroRadius;
    // [SerializeField] float SightRadius;
    [SerializeField] float attackdamage;
    [SerializeField] float attackRadius;
    [SerializeField] float attackInterval;
    [SerializeField] Collider atkCollider;

    [Header("Agent variables")]
    [SerializeField] float agentWalkSpeed;
    [SerializeField] float agentRunSpeed;

    [Header(" ")]
    //Distance Variables
    [SerializeField] Transform eyeTransfrom; //Where the monsters eyes are located
    float distanceFromPlayer; // distance between the enemie and the player
    bool playerSight; // if the player is in LOS (can hide from enemies with this)
    bool playerSeen; // if the player is in SightRadius and sight is not blocked (probably don't need actually)
    bool attackPlayer; // if the enemie will attack the player
    bool isDead; // might move to npc 
    bool alreadyattacked;

    //other
    Animator animator;
    RaycastHit hit;
    Vector3 rayDirection;
    [SerializeField] LayerMask playermask;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        //Animation variables
        animator.SetFloat("Speed", agent.velocity.magnitude);     
   

        // AI States
        distanceFromPlayer = Vector3.Distance(player.transform.position, agent.transform.position);

        //Watch // might use for something else (Scream, Charge ... )
        //if (distanceFromPlayer < SightRadius)
        //{
        //    playerSeen = true;
        //    transform.LookAt(player.transform);
        //    Debug.Log("Seen");
        //}
        //else { playerSeen = false; }

        //Patrolling
        if (distanceFromPlayer > aggroRadius || !playerSight)
        {
            base.Patrolling = true; agent.speed = agentWalkSpeed; 
        }
        else { base.Patrolling = false; agent.speed = agentRunSpeed; }
        //Aggro
        if (distanceFromPlayer < aggroRadius && playerSight)
        {
            PlayerAggro();
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            rayDirection = player.transform.GetChild(0).position - eyeTransfrom.position;
            Physics.Raycast(eyeTransfrom.position, rayDirection, out hit, 1000f);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    playerSight = true;
                }
                else
                {
                    playerSight = false;
                }
            }
        }
    }

    void PlayerAggro()
    {
        agent.SetDestination(player.transform.position);
        if (!alreadyattacked)
        {
            alreadyattacked = true;
            animator.SetTrigger("Attack");
            Invoke(nameof(AttackCD), attackInterval);
            Collider[] colliders = Physics.OverlapBox(atkCollider.bounds.center, atkCollider.bounds.extents, atkCollider.transform.rotation, playermask);   
            foreach (Collider col in colliders)
            {
                //if (col.gameObject.TryGetComponent<CharacterHealth>().takedamage()) //example implementation
            }     
        }
        
    }

    void AttackCD()
    {
        alreadyattacked = false;
    }
}
