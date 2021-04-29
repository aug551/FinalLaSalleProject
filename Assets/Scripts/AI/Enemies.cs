using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : NPC
{
    //=============================================================================
    // Author: Kevin Charron
    //=============================================================================

    //Attack variables
    [Header("Attack variables")]
    [SerializeField] float aggroRadius;
    [SerializeField] float SightRadius;
    [SerializeField] float attackdamage;
    [SerializeField] float attackRadius;
    [SerializeField] float attackInterval;
    [SerializeField] float attackCoolDown = 0f;

    //Distance Variables
    [SerializeField] Transform eyeTransfrom; //Where the monsters eyes are located
    float distanceFromPlayer; // distance between the enemie and the player
    bool playerSight; // if the player is in LOS (can hide from enemies with this)
    bool playerSeen; // if the player is in SightRadius and sight is not blocked (probably don't need actually)
    bool attackPlayer; // if the enemie will attack the player
    bool isDead; // might move to npc 

    //other
    Animator animator;
    RaycastHit hit;
    Vector3 rayDirection;

    private void Awake()
    {
        base.player = GameObject.Find("Player");
        base.agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        //Animation variables
        animator.SetFloat("Speed", agent.velocity.magnitude);     
   

        // AI States
        distanceFromPlayer = Vector3.Distance(player.transform.position, agent.transform.position);

        //Watch
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
            base.Patrolling = true; agent.speed = 1f; 
        }
        else { base.Patrolling = false; agent.speed = 3f; }
        //Aggro
        if (distanceFromPlayer < aggroRadius && playerSight)
        {
            PlayerAggro();
        }
        //Attack
        //if (distanceFromPlayer < attackRadius)
        //{
        //    attackPlayer = true;
        //}
        //else { attackPlayer = false; }
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
        transform.LookAt(player.transform);
        agent.SetDestination(player.transform.position);
        Debug.Log("Aggro");
    }

    void AttackPlayer()
    {
        //TBD
    }
}
