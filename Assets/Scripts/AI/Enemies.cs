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
    [SerializeField] float aggroRadius;
    [SerializeField] float SightRadius;
    [SerializeField] float attackdamage;
    [SerializeField] float attackRadius;
    [SerializeField] float attackInterval;
    [SerializeField] float attackCoolDown = 0f;

    //Distance Variables
    [SerializeField] Transform eyeTransfrom; //Where the monsters eyes are located
    float distanceFromPlayer; // distance between the enemie and the player
    bool playerSight; // if the player is in SightRadius
    bool playerSeen; // if the player is in SightRadius and sight is not blocked
    bool attackPlayer; // if the enemie will attack the player
    bool isDead = false; // might move to npc 

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

    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, agent.transform.position);

        if (distanceFromPlayer < SightRadius)
        {
            playerSeen = true;
            transform.LookAt(player.transform);
        }
        else { playerSeen = false; }
        if (distanceFromPlayer < aggroRadius && playerSight)
        {
            PlayerAggro();
        }
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
                if (hit.collider.gameObject.tag == "Player" && playerSeen)
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
        //animator.SetBool("move", true);
        agent.SetDestination(player.transform.position);
        Debug.Log("aggro");
    }

    void AttackPlayer()
    {
        //TBD
    }
}
