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
    bool isChasing;
    bool isAttacking = false; // placeholder.. Change when there's a reply

    //other
    Animator animator;
    RaycastHit hit;
    Vector3 rayDirection;
    [SerializeField] LayerMask playermask;
    public EnemyHealth enemy;

    public float AgentWalkSpeed { get => agentWalkSpeed; set => agentWalkSpeed = value; }
    public float AgentRunSpeed { get => agentRunSpeed; set => agentRunSpeed = value; }

    public bool IsPatrolling { get => base.Patrolling; }
    public bool IsChasing { get => isChasing; }
    public bool IsAttacking { get => isAttacking; }

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        enemy = GetComponent<EnemyHealth>();
    }

    protected override void Update()
    {
        base.Update();
        //Animation variables
        // animator.SetFloat("Speed", agent.velocity.magnitude);     
   

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
        if (distanceFromPlayer > aggroRadius && !playerSight)
        {
            base.Patrolling = true; 
            agent.speed = agentWalkSpeed;
            isChasing = false;
        }
        else { base.Patrolling = false; agent.speed = agentRunSpeed; }
        //Aggro
        if (distanceFromPlayer < aggroRadius || playerSight)
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
            Debug.DrawRay(eyeTransfrom.position, rayDirection, Color.red);
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
        this.isChasing = true;

        // Check if player is nearby.
        if (!alreadyattacked)
        {
            alreadyattacked = true;
            // animator.SetTrigger("Attack");
            Invoke(nameof(AttackCD), attackInterval);
            Collider[] colliders = Physics.OverlapBox(atkCollider.bounds.center, atkCollider.bounds.extents, atkCollider.transform.rotation, playermask);   
            foreach (Collider col in colliders)
            {
                Debug.Log("hit");
                if (col.tag == "Player")
                {
                    if (col.gameObject.TryGetComponent<CharacterHealth>(out CharacterHealth playerhealth))
                    {
                        Debug.Log("hit");
                        playerhealth.TakeDamage(enemy.attack);
                    }
                }
            }
        }
    }

    void AttackCD()
    {
        alreadyattacked = false;
    }
}
